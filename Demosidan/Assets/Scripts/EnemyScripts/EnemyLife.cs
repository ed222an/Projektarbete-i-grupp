using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour 
{
    public static event EnemyDeathEventManager.DeathAction WorkerBotDeath;

    public float maxLife;
	public float invTime = 0.1f;
    public EnemyHandler enemyHandler;

    private Image healthBar;
    private Canvas hpBarCanvas;
    private float currentLife;
    private bool isAlive;
	private bool canTakeDamage = true;
	private PlayerHandler ph;
    private StatManager statManager;

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
        statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();

        isAlive = true;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (currentLife <= 0 && isAlive)
        {
            isAlive = false;

            statManager.AddKill();
            if (WorkerBotDeath != null)
                WorkerBotDeath();
            
            DestroyEnemy();
        }
	}

    //Take damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Weapon")
        {
			if(canTakeDamage)
			{
				canTakeDamage = false;

	            if (!hpBarCanvas.enabled)
				{
	                hpBarCanvas.enabled = true;
				}

                if (ph == null)
                    ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();

				float damage = ph.GetTotalPlayerAttack();
	            currentLife -= damage;
	            UpdateHealthBar();

	            Debug.Log("Enemy took " + damage + " damage.");

				if(enemyHandler != null)
				{
	            	enemyHandler.KnockbackOnHit(transform.position.x, other.transform.position.x);
				}

				StartCoroutine(ChangeDamageStatus());
			}
        }
    }

	// Make the enemy invulnerable for a set amount of time
	private IEnumerator ChangeDamageStatus()
	{
		yield return new WaitForSeconds(invTime);
		canTakeDamage = true;
	}

	// Destroy the enemy gameobject
    void DestroyEnemy()
    {
        DestroyObject(transform.gameObject);
    }

	// Update the healthbar symbol
    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentLife / maxLife;
    }
}
