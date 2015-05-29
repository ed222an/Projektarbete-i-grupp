using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public float hSpeed = 100;
	public float lifeTime = 0.5f;

	private GameObject player;
	private Rigidbody2D rb;
	private bool addSpeed = true;
	
	void Start ()
	{
		// Finds the player object and gets its rigidbody component.
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(17, 13);
        Physics2D.IgnoreLayerCollision(17, 8);
        Physics2D.IgnoreLayerCollision(17, 15);
        Physics2D.IgnoreLayerCollision(17, 18);
	}

	void FixedUpdate ()
	{
		// If speed should be added...
		if(addSpeed)
		{
			addSpeed = false;

			// Check where the player and add force relative to its position.
			if (player.transform.position.x <= transform.position.x)
			{
				rb.AddForce(transform.right * hSpeed);
			}
			else
			{
				rb.AddForce(transform.right * -hSpeed);
			}

			StartCoroutine(Despawn());
		}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if it's a wall etc, remove it, if it's the player, you know, do stuff
        if (coll != null)
        {
            if (coll.gameObject.layer != 12)
                DestroyObject(this.gameObject);
        }
    }

	// Destroy the object.
	private IEnumerator Despawn()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
