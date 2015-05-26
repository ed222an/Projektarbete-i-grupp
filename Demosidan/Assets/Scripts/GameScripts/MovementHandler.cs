using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour 
{
    public float speed;
    public float jumpForce;
	public Vector2 velocity;
    public LayerMask whatIsGround;
    public Transform groundCheck;
	public bool facingRight = true;

	// Engineer related
	public bool isEngineer = false;
	public GameObject cannonball;
	public GameObject bulletSpawnpoint;

    private Rigidbody2D rBody;
    private bool grounded = false;
    private bool canAttack = true;
    private bool controllable = true;
    private float groundRadiusX = 0.4f;
    private float groundRadiusY = 0.05f;
    private float attackTimer = 0.0f;

	private Animator anim;

    private PlayerHandler playerHandler;
    private StatManager statManager;

	// Audio
	public AudioClip[] jumpSounds;
	public AudioClip[] attackSounds;
	
	private bool isPlaying = false;
	private AudioSource audio;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(8, 14);
        playerHandler = GetComponent<PlayerHandler>();
		audio = gameObject.GetComponent<AudioSource>();
        statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();
    }

    void FixedUpdate()
    {
        if (!controllable)
            return;

        grounded = Physics2D.OverlapArea(new Vector2(groundCheck.position.x - groundRadiusX, groundCheck.position.y - groundRadiusY),
                                            new Vector2(groundCheck.position.x + groundRadiusX, groundCheck.position.y + groundRadiusY), whatIsGround);

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        velocity = new Vector2(moveHorizontal * speed, rBody.velocity.y);

        rBody.velocity = velocity;

		// Flip the player
        if (moveHorizontal > 0 && !facingRight)
		{
			Flip();
		}
        else if (moveHorizontal < 0 && facingRight)
		{
			Flip();
		}
    }

	// Update is called once per frame
	void Update() 
    {
        if (Time.timeScale == 0 || !controllable)
            return;

		// Jumping
        if (grounded && Input.GetKeyDown("space"))
        {
            rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            statManager.AddJump();

			// Jump sound
			if(!isPlaying)
			{
				StartCoroutine(PlayJumpSound());
			}
        }

		// Jumping animation
		if (grounded)
			anim.SetBool("Grounded", true);
		else
		{
			anim.SetBool("Grounded", false);
		}

		if (velocity.x != 0)
			anim.SetBool("Running", true);
		else
			anim.SetBool("Running", false);

		// Attack animation test
		if (Input.GetMouseButton(0) && canAttack) 
        {
			anim.SetBool("Attacking", true);
            canAttack = false;
            
            attackTimer = playerHandler.GetPlayerAttackSpeed();

			// If the player is playing the Engineer...
			if(isEngineer)
			{
				// Spawn a bullet.
				GameObject newBullet = Instantiate(cannonball, bulletSpawnpoint.transform.position, Quaternion.identity) as GameObject;
			}

			// Attack sound
			if(!isPlaying)
			{
				StartCoroutine(PlayAttackSound());
			}
		} 
        else
		{
			anim.SetBool("Attacking", false);
		}

        //Attack timer
        if (attackTimer <= 0)
        {
            canAttack = true;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
	}

	// Plays the jump sound
	private IEnumerator PlayJumpSound()
	{
		if (!isPlaying)
		{
			isPlaying = true;
			
			int random = Random.Range(0,jumpSounds.Length);
			audio.PlayOneShot(jumpSounds[random]);
			
			yield return new WaitForSeconds(jumpSounds[random].length);
			
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

	// Flips the world around the player, allowing us to only use 1 set of animations.
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void KnockbackOnHit(float playerPosX, float enemyPosX)
    {
        if (playerPosX > enemyPosX)
        {
            rBody.AddForce(new Vector2(1500f, 100f));
        }
        else
        {
            rBody.AddForce(new Vector2(-1500f, 100f));
        }
    }

    public void SetCanControlCharacter(bool status)
    {
        controllable = status;
    }
}
