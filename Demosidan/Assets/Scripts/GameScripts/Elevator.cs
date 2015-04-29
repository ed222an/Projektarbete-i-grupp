using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;

    private Transform playerTransform;

	public float moveSpeed;

	// Use this for initialization
	void Awake()
	{
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementHandler>().SetCanControlCharacter(false);
	}
	
	// Update is called once per frame
	void Update()
	{
        if (transform.position.y <= endTransform.position.y)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementHandler>().SetCanControlCharacter(true);
            return;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);

        playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - moveSpeed * Time.deltaTime, playerTransform.position.z);

        
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