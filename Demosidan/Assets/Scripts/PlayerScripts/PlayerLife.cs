using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour 
{
    public float maxLife;
    public MovementHandler movementHandler;

    private GUIText lifeText;
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
        lifeText = GameObject.Find("LifeText").GetComponent<GUIText>();
        currentLife = maxLife;

        isAlive = true;

        SetLifeText(currentLife, maxLife);
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
            float damageToTake = collision.gameObject.GetComponent<EnemyHandler>().GetTotalAttack();
            currentLife -= damageToTake;
            SetLifeText(currentLife, maxLife);
            Debug.Log("Player took " + damageToTake + "damage.");
            movementHandler.KnockbackOnHit(transform.position.x, collision.transform.position.x);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void SetLifeText(float currentLife, float maxLife)
    {
        lifeText.text = "Life: " + currentLife + " / " + maxLife;
    }
}
