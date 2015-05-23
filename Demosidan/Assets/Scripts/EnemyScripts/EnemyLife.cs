using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour 
{
    public static event EnemyDeathEventManager.DeathAction WorkerBotDeath;

    public float maxLife;
    public EnemyHandler enemyHandler;

    private Image healthBar;
    private Canvas hpBarCanvas;
    private float currentLife;
    private bool isAlive;
	private PlayerHandler ph;

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
        healthBar = GetComponentInChildren<Image>();
        hpBarCanvas = GetComponentInChildren<Canvas>();
        currentLife = maxLife;

		ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();

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

            StatManager.AddKill();
            if (WorkerBotDeath != null)
                WorkerBotDeath();
            
            DestroyEnemy();
        }
	}

    //Take damage
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Rewrite the player damage functionality to look more like the enemy version.
        if (other.transform.tag == "Weapon")
        {
            if (!hpBarCanvas.enabled)
                hpBarCanvas.enabled = true;
			float damage = ph.GetTotalPlayerAttack();
            currentLife -= damage;
            UpdateHealthBar();
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

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentLife / maxLife;
    }
}
