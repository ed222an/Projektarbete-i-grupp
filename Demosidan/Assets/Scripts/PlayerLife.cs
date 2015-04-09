using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour 
{
    public float maxLife;
    public MovementHandler movementHandler;

    private float currentLife;
    private bool isAlive;

    #region Get/Set
    public float MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public float CurrentLife
    {
        get { return currentLife; }
    }
    #endregion

    // Use this for initialization
	void Start()
    {
        currentLife = maxLife;

        isAlive = true;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (currentLife <= 0 && isAlive)
        {
            isAlive = false;

            KillPlayer();
        }
	}

    void KillPlayer()
    {
        DestroyObject(transform.gameObject);
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentLife -= 1; //TODO: This should be the enemies damage.
            Debug.Log("Player took " + currentLife + "damage.");
            movementHandler.KnockbackOnHit(transform.position.x, collision.transform.position.x);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
