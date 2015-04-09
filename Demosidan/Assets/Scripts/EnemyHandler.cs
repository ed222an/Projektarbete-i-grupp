using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour 
{
    public float followRange;
    public Transform target;
    public float speed;

    private Rigidbody2D rBody;
	// Use this for initialization
	void Start() 
    {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        if (target != null && ((transform.position.x + followRange > target.position.x && transform.position.x < target.position.x) 
            || (transform.position.x - followRange < target.position.x && transform.position.x > target.position.x)))
            FollowTarget();
	}

    //TODO: Use the vector to check if target is close by, right now it's only x, needs to be Y aswell.
    void FollowTarget()
    {
        if (transform.position.x < target.position.x)
            rBody.velocity = new Vector2(1f * speed, rBody.velocity.y);
        else
            rBody.velocity = new Vector2(-1f * speed, rBody.velocity.y);
    }

    public void KnockbackOnHit(float playerPosX, float enemyPosX)
    {
        if (playerPosX > enemyPosX)
        {
            rBody.AddRelativeForce(new Vector2(1200f, 150f), ForceMode2D.Force);
        }
        else
        {
            rBody.AddRelativeForce(new Vector2(-1200f, 150f), ForceMode2D.Force);
        }
    }
}
