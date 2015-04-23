using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour 
{
    public MovementHandler movementHandler;
    public PlayerHandler playerHandler;

    private Text lifeText;
    private Image healthBar;
    //private Slider healthBar;
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
        lifeText = GameObject.Find("HpBarText").GetComponent<Text>();
        healthBar = GameObject.Find("HpOverlayBar").GetComponent<Image>();
		playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();

        currentLife = maxLife = playerHandler.GetPlayerMaxLife();

        //Setup the HP bar.
        //healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
        //healthBar.maxValue = maxLife;
        //healthBar.value = maxLife;
        

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
            //healthBar.value = CurrentLife;
            ModifyHpBar();
            SetLifeText(CurrentLife, MaxLife);
            Debug.Log("Player took " + damageToTake + "damage.");
            movementHandler.KnockbackOnHit(transform.position.x, collision.transform.position.x);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void ModifyHpBar()
    {
        healthBar.fillAmount = currentLife / maxLife;
    }

    void SetLifeText(float currentLife, float maxLife)
    {
        lifeText.text = currentLife + " / " + maxLife;
    }
}
