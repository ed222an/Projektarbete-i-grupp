using UnityEngine;
using System.Collections;

public class RangedEnemyScript : MonoBehaviour 
{
    public GameObject bullet;
    public LayerMask rayCastLayers;
	public float searchLazerLength;
    public float attackIntervalTime = 1f;
	public float timeBetweenAttacks = 0.45f;
	public bool facingRight = true;
    public bool turretMode = true;

    private bool lazerActive = true;

	private float attackInterval = 0.2f;

    private RaycastHit2D hit;
	private Animator anim;
	private RectTransform hpBarRect;
	private GameObject player;

	// Audio
	public AudioClip[] movingSounds;
	public AudioClip[] attackSounds;
	
	private bool isPlaying = false;
	private AudioSource audio;

	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
		hpBarRect = GetComponentInChildren<RectTransform>();
		player = GameObject.FindGameObjectWithTag("Player");
		audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
    {
		if (transform.position.x < player.transform.position.x)
		{
			if(!facingRight)
			{
				Flip();
				StartCoroutine(PlayMovingSound());
			}
		}
		else
		{
			if(facingRight)
			{
				Flip();
				StartCoroutine(PlayMovingSound());
			}
		}

        if (turretMode)
        {
            //ShowLazer();

            if (UpdateAttackInterval())
            {
                StartCoroutine(Shoot());
                Shoot();
            }
        }
        else
        {
            if (hit != null && hit.transform.gameObject.tag != "Player")
            {
                UpdateLazerPosition();
            }
            else if (hit != null && hit.transform.gameObject.tag == "Player")
            {
                Shoot();
            }
        }
	}

    private void UpdateLazerPosition()
    {
        throw new System.NotImplementedException();
    }

    private bool UpdateAttackInterval()
    {
        if (attackInterval > 0)
        {
            attackInterval -= Time.deltaTime;
            return false;
        }
        else
        {
            attackInterval = attackIntervalTime;
            return true;
        }
    }

    IEnumerator Shoot()
    {
		anim.SetBool("Attacking", true);
        CreateBullet();
		StartCoroutine(PlayAttackSound());
		yield return new WaitForSeconds(timeBetweenAttacks);
        CreateBullet();
		StartCoroutine(PlayAttackSound());
		yield return new WaitForSeconds(timeBetweenAttacks);
        CreateBullet();
		StartCoroutine(PlayAttackSound());
		yield return new WaitForSeconds(timeBetweenAttacks);
		anim.SetBool("Attacking", false);
    }

    void CreateBullet()
    {
        GameObject bulletObj = Instantiate(bullet, transform.position + new Vector3(0.2f, 0.2f, 0f), Quaternion.identity) as GameObject;
        EnemyBullet bulletScript = bulletObj.GetComponent<EnemyBullet>();
        bulletScript.Instantiate(this.gameObject, rayCastLayers);
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

    //TODO: Create a lazer
    public void ShowLazer()
    {
        hit = Physics2D.Linecast(transform.position + new Vector3(0f, 0.2f, 0f), transform.position + new Vector3(searchLazerLength, 0.2f, 0f), rayCastLayers);
        Debug.DrawLine(transform.position, transform.position + new Vector3(searchLazerLength, 0f, 0f));
    }
}
