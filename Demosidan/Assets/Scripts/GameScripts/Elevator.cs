using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
	public Transform startPos;
	public Transform endPos;

	public float moveSpeed = 2.0f;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		/*
		transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

		if(transform.position.y >= startPos.position.y)
		{
			Debug.Log ("down");
			moveSpeed = moveSpeed * -1;
		}

		if (transform.position.y <= endPos.position.y)
		{
			Debug.Log ("up");
			moveSpeed = moveSpeed * -1;
		}

		*/
	}
}