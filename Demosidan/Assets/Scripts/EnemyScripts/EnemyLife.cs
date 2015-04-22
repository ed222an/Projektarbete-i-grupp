using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour 
{
    public float maxLife;
    public EnemyHandler enemyHandler;
    public GUIText killCount;

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

    void Awake()
    {
        currentLife = maxLife;

        isAlive = true;
    }

    // Use this for initialization
	void Start()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
        if (currentLife <= 0 && isAlive)
        {
            isAlive = false;

            KillCountManager.AddKill();

            DestroyEnemy();
        }
	}

    //Take damage
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Weapon")
        {
            float damage = other.gameObject.GetComponentInParent<PlayerHandler>().GetTotalPlayerAttack();
            currentLife -= damage;
            Debug.Log("Enemy took " + damage + " damage.");
            enemyHandler.KnockbackOnHit(transform.position.x, other.transform.position.x);

            yield return new WaitForSeconds(0.25f);
        }
    }

    void DestroyEnemy()
    {
        DestroyObject(transform.gameObject);
    }

    void OnDestroy()
    { 
        
    }
}
