using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour 
{
    private int goldAmount = 1;

    private float pickupTimer;

    void Awake()
    {
        pickupTimer = 1;

        Physics2D.IgnoreLayerCollision(19, 8);
        Physics2D.IgnoreLayerCollision(19, 12);
        Physics2D.IgnoreLayerCollision(19, 17);
        Physics2D.IgnoreLayerCollision(19, 18);
        Physics2D.IgnoreLayerCollision(19, 19);
    }

    void Update()
    {
        if (pickupTimer > 0)
        {
            pickupTimer -= Time.deltaTime; 
        }
    }

    void Start()
    {
        float xForce = Random.Range(-2f, 2f);
        float yForce = Random.Range(1f, 5f) + 1;

        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse); //Add random force
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll != null && coll.gameObject.tag == "Player")
        {
            if (pickupTimer <= 0)
            { 
                coll.gameObject.GetComponent<PlayerHandler>().AddGold(goldAmount);
                DestroyObject(this.transform.parent.gameObject);
            }
        }
    }

    public void SetGoldAmount(int amount)
    {
        goldAmount = amount;
    }
}
