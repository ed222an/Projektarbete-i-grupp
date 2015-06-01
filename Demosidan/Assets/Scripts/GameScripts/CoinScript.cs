using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour 
{
    private int goldAmount = 1;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(19, 12);
        Physics2D.IgnoreLayerCollision(19, 17);
        Physics2D.IgnoreLayerCollision(19, 18);
        Physics2D.IgnoreLayerCollision(19, 19);
    }

    void Start()
    {
        float xForce = Random.Range(0f, 2f);
        float yForce = Random.Range(1f, 5f) + 1;
        if (xForce == 0)
            xForce = -1;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, 5f), ForceMode2D.Impulse); //Add random force
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll != null && coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHandler>().AddGold(goldAmount);
            DestroyObject(this.gameObject);
        }
    }

    public void SetGoldAmount(int amount)
    {
        goldAmount = amount;
    }
}
