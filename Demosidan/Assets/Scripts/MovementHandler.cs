using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour 
{
    public float speed = 30;
	public Vector2 velocity;
    public LayerMask whatIsGround;
    public Transform groundCheck;
	public bool facingRight = true;

    private Rigidbody2D rBody;
    private bool grounded = false;
    private float groundRadius = 0.02f;

	private Animator anim;
    
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = Input.GetAxis("Horizontal");

		velocity = new Vector2(moveHorizontal * speed, rBody.velocity.y);  
		rBody.velocity = velocity;

		// Flip the player
		if (moveHorizontal > 0 && !facingRight)
		{
			Flip();
		}
		else if(moveHorizontal < 0 && facingRight)
		{
			Flip();
		}
    }

	void Start() 
    {
        rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update() 
    {
		// Jumping
        if (grounded && Input.GetKeyDown ("space"))
			rBody.AddForce (new Vector2 (0f, 200f));

		// Jumping animation
		if (grounded)
			anim.SetBool ("Grounded", true);
		else
			anim.SetBool ("Grounded", false);

		if (velocity.x != 0)
			anim.SetBool ("Running",true);
		else
			anim.SetBool ("Running",false);

		// Attack animation test
		if (Input.GetMouseButtonDown (0)) 
        {
			anim.SetBool ("Attacking", true);
		} 
        else
		{
			anim.SetBool("Attacking", false);
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
            rBody.AddForce(new Vector2(-50f, 20f));
        else
            rBody.AddForce(new Vector2(50f, 20f));
    }
}
