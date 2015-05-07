using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
	public GameObject icon;
	public float waitTime = 0.1f;

	private Animator anim;
	private bool canFlip = true;

	void Start() 
	{
		anim = gameObject.GetComponent<Animator> ();
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		{            
			if (Input.GetKeyUp(KeyCode.E))
			{
				if(canFlip)
				{
					canFlip = false;
					StartCoroutine(FlipSwitch());
				}
			}
		}
	}
	
	IEnumerator FlipSwitch()
	{
		anim.SetTrigger ("Flip");
		yield return new WaitForSeconds (waitTime);
		canFlip = true;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			icon.SetActive(true);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			icon.SetActive(false);
		}
	}
}
