using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour 
{
    public float followRangeRadiusX = 3;
    public float followRangeRadiusY = 1;
    public float speed;
    public LayerMask playerLayer;
    public LayerMask rayCastLayers;
    public LayerMask whatIsGround;
	public bool facingRight = true;
    public bool patrol;
    public Transform edgeCheck;
    public Transform edgeCheck2;
    public bool platformSticky;
	public bool isRanged;

    private float moveDirection;
    private bool changeDirection;
    private float edgeCheckRadius = 0.7f;
    private float startPatrolTimer = 1f;
    private bool canPatrol;
    
    private Rigidbody2D rBody;
	private Animator anim;
    private EnemyStats enemyStats;
	private Transform target;
    private bool isInAttackRange = false;
    private bool canAttack = false;
    private bool isInRange = false;
	private bool isAttacking = false;
    private float attackTimer = 0.0f;
    private GameObject playerObject;

    private RaycastHit2D hit;

    //This is the HP bar ontop of the enemy
    private RectTransform hpBarRect;

	// Audio
	public AudioClip[] movingSounds;
	public AudioClip[] attackSounds;
	
	private bool isPlaying = false;
	private AudioSource audio;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerObject = target.gameObject;
        hpBarRect = GetComponentInChildren<RectTransform>();
        speed = Random.Range(speed - 0.5f, speed + 0.5f);
        moveDirection = Random.Range(0, 2);
        Physics2D.IgnoreLayerCollision(12, 12);
        Physics2D.IgnoreLayerCollision(12, 16);
		audio = gameObject.GetComponent<AudioSource>();

        if (moveDirection == 0)
            moveDirection = -1;
    }

	// Update is called once per frame
	void FixedUpdate() 
    {
        if (target != null)
        {
            //If player is in scene, cast a line to the player to determine if the enemy can "see" the player or if there are other objects in the way
            hit = Physics2D.Linecast(rBody.position + new Vector2(0.0f, 0.5f), target.position, rayCastLayers);
            //Debug.DrawLine(rBody.position + new Vector2(0.0f, 0.5f), target.position);
        }

        //if the player is not visibly blocked by other objects from the enemys, then we can start checking if he is in the enemys vision range
        if (target != null && hit.collider.gameObject.tag == "Player")
        {
            isInRange = Physics2D.OverlapArea(new Vector2(transform.position.x - followRangeRadiusX, transform.position.y - followRangeRadiusY),
                                                    new Vector2(transform.position.x + followRangeRadiusX, transform.position.y + followRangeRadiusY), playerLayer);
        }
        else
        {
            isInRange = false;
        }

		if (!isRanged)
		{
			if (target != null && isInRange) 
            {
				canPatrol = false;
				startPatrolTimer = 1f;
				FollowTarget ();

				if (platformSticky && EdgeCheck ()) 
                {
					StopMovement ();
				}
			} 
            else if (patrol && canPatrol && startPatrolTimer < 0) 
            {
				EdgeCheck ();
				Patrol ();
			} 
            else 
            {
				StopMovement ();
			}
		}
    }

    void StopMovement()
    {
        rBody.velocity = new Vector2(0.0f, rBody.velocity.y);
        anim.SetBool("Moving", false);
    }

    void Update()
    {
        if (startPatrolTimer > 0)
        {
            startPatrolTimer -= Time.deltaTime;
        }
        else if (startPatrolTimer <= 0)
        {
            canPatrol = true;
        }

        //Lower the time to the next attack by passed time.
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else if (attackTimer <= 0)
            canAttack = true;

        //Make sure the player exists before asking anything about the player.
        if (playerObject != null)
        {
            if (canAttack && isInAttackRange)
            {
                playerObject.gameObject.GetComponentInParent<PlayerLife>().DealDamageToPlayer(enemyStats.damage);
                canAttack = false;
                attackTimer = enemyStats.attackSpeed;
				StartCoroutine(PlayAttackSound());
				anim.SetBool("Attacking",true);
				isAttacking = true;
            }
			else
			{
				StartCoroutine(StopAttacking());
			}
        }
    }

	// Stops the attack animation
	private IEnumerator StopAttacking()
	{
		if(isAttacking)
		{
			isAttacking = false;
			yield return new WaitForSeconds(0.3f);
			anim.SetBool("Attacking",false);
		}
	}

    bool EdgeCheck()
    {
        if (!Physics2D.OverlapCircle(edgeCheck2.position, edgeCheckRadius, whatIsGround))
        {
            changeDirection = true;
            return true;
        }

        return false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            changeDirection = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInAttackRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInAttackRange = false;
        }
    }

    void Patrol()
    {
        if (changeDirection)
        {
            moveDirection *= -1;
            changeDirection = false;
        }
        if (moveDirection == 1)
        {
            if (!facingRight)
            {
                Flip();
            }
            rBody.velocity = new Vector2(moveDirection * speed, rBody.velocity.y);
        }
        else
        {
            if (facingRight)
            {
                Flip();
            }
            rBody.velocity = new Vector2(moveDirection * speed, rBody.velocity.y);
        }

        anim.SetBool("Moving", true);
    }

    void FollowTarget()
    {
        //Move right
        if (transform.position.x < target.position.x)
		{
			if(!facingRight)
			{
				Flip();
			}
            moveDirection = 1;
            rBody.velocity = new Vector2(1f * speed, rBody.velocity.y);
		}
        //Move left
        else
		{
			if(facingRight)
			{
				Flip();
			}
            moveDirection = -1;
            rBody.velocity = new Vector2(-1f * speed, rBody.velocity.y);
		}

		anim.SetBool("Moving", true);
		StartCoroutine(PlayMovingSound());
    }

	// Flips the world around the enemy, allowing us to only use 1 set of animations.
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

        //Flip the HP bar aswell.
        Vector3 hpBarScale = hpBarRect.localScale;
        hpBarScale *= -1;
        hpBarRect.localScale = hpBarScale;
	}

    public void KnockbackOnHit(float playerPosX, float enemyPosX)
    {
        if (playerPosX > enemyPosX)
        {
            rBody.AddForce(new Vector2(1200f, 0f));
        }
        else
        {
            rBody.AddForce(new Vector2(-1200f, 0f));
        }
    }

    //The idea is that this function gets all stats, flat damage and combines it to a single value, then is returned to the caller.
    public float GetTotalAttack()
    {
        return enemyStats.damage;
    }

	// Plays the moving sound
	private IEnumerator PlayMovingSound()
	{
		if (!isPlaying)
		{
			isPlaying = true;
			
			int random = Random.Range(0,movingSounds.Length);
			audio.PlayOneShot(movingSounds[random]);
			
			yield return new WaitForSeconds(movingSounds[random].length);
			
			isPlaying = false;
		}
	}

	// Plays the attack sound
	public IEnumerator PlayAttackSound()
	{
		if (!isPlaying)
		{
			isPlaying = true;
			
			int random = Random.Range(0,attackSounds.Length);
			audio.PlayOneShot(attackSounds[random]);
			
			yield return new WaitForSeconds(attackSounds[random].length);
			
			isPlaying = false;
		}
	}
}
