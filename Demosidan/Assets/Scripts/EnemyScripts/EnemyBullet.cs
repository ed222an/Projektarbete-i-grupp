using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 1000;

    GameObject origin;
    Rigidbody2D rBody;

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if it's a wall etc, remove it, if it's the player, you know, do stuff
        if (coll != null)
        {
            if (coll.gameObject.tag == "Player")
            {
                //Deal damage
                coll.gameObject.GetComponent<PlayerLife>().DealDamageToPlayer(origin.gameObject.GetComponent<EnemyStats>().damage);
            }

            DestroyObject(this.gameObject);
        }
    }

    public void Instantiate(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Null was passed to EnemyBullet::SetOrigin");
            DestroyObject(this.gameObject);
        }

        origin = obj;
        rBody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(18, 12);
        Physics2D.IgnoreLayerCollision(18, 18);

        if (origin.transform.position.x <= transform.position.x)
        {
            rBody.AddForce(transform.right * speed);
        }
        else
        {
            rBody.AddForce(transform.right * -speed);
        }
    }
}
