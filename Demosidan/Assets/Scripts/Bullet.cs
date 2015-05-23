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

	// Destroy the object.
	private IEnumerator Despawn()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
