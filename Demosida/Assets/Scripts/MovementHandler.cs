using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour 
{
    public float speed = 30;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    private Rigidbody2D rBody;
    private bool grounded = false;
    private float groundRadius = 0.02f;
    
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = Input.GetAxis("Horizontal");

        rBody.velocity = new Vector2(moveHorizontal * speed, rBody.velocity.y);  
    }

	void Start() 
    {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (grounded && Input.GetKeyDown("space"))
            rBody.AddForce(new Vector2(0f, 200f));
	}
}
