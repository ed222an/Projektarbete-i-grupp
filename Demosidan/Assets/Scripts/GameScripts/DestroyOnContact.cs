using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;

        Destroy(other.gameObject);
    }
}
