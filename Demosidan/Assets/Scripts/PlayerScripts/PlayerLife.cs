using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour 
{
    public MovementHandler movementHandler;
    public PlayerHandler playerHandler;

    private GUIText lifeText;
    private float maxLife;
    private float currentLife;
    private bool isAlive;

    #region Get/Set
    public float MaxLife
    {
        get { return maxLife; }
        set 
        {
            if (value < 0)
                value = 0;
            maxLife = value; 
        }
    }

    public float CurrentLife
    {
        get { return currentLife; }
        set
        {
            if (value < 0)
                value = 0;
            currentLife = value;
        }
    }
    #endregion

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<GUIText>();
        playerHandler = GameObject.Find("Dwarf_1").GetComponent<PlayerHandler>();//TODO: Still don't want to get the specific dwarf, but w/e right now.
        currentLife = maxLife = playerHandler.GetPlayerMaxLife();

        isAlive = true;
    }

    // Use this for initialization
	void Start()
    {
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
            CurrentLife -= damageToTake;
            SetLifeText(CurrentLife, MaxLife);
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
