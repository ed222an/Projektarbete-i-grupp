using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();
            playerLife.DealDamageToPlayer(playerLife.MaxLife);
        }
        else if (other.tag == "Enemy")
            DestroyObject(other.gameObject);
    }
}
