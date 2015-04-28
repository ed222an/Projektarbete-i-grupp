using UnityEngine;
using System.Collections;

public class ShowSignMessage : MonoBehaviour 
{
	
	// Update is called once per frame
	void Update() 
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter " + this.GetType().Name + " on object " + gameObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("exit " + this.GetType().Name + " on object " + gameObject.name);
        }
    }
}
