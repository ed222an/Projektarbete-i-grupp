using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public float speed = 100;

	private GameObject player;
	private Rigidbody2D rb;
	private bool addSpeed = true;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
	{
		if(addSpeed)
		{
			addSpeed = false;

			if (player.transform.position.x <= transform.position.x)
			{
				rb.AddForce(transform.right * speed);
				Debug.Log ("Shoot right!");
			}
			else
			{
				rb.AddForce(transform.right * -speed);
				Debug.Log("Shoot left!");
			}

			StartCoroutine(Despawn());
		}
	}

	private IEnumerator Despawn()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
}
