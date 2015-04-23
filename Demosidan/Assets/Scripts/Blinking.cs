using UnityEngine;
using System.Collections;

public class Blinking : MonoBehaviour
{
	public float blinkDelay;

	private SpriteRenderer objRenderer;

	// Use this for initialization
	void Start ()
	{
		objRenderer = gameObject.GetComponent<SpriteRenderer>();
		StartCoroutine(Blink());
	}

	private IEnumerator Blink()
	{
		while(true)
		{
			objRenderer.enabled = false;
			yield return new WaitForSeconds(blinkDelay);

			objRenderer.enabled = true;
			yield return new WaitForSeconds(blinkDelay);
		}
	}
}
