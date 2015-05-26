using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour 
{
    public MovementHandler movementHandler;
    public PlayerHandler playerHandler;

    private StatManager statManager;
    private Text lifeText;
    private Image healthBar;
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
        statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();
        isAlive = true;
    }

    // Use this for initialization
	void Start()
    {
        currentLife = maxLife = playerHandler.GetPlayerMaxLife();
        SetLifeText();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (lifeText == null)
            lifeText = GameObject.Find("HpBarText").GetComponent<Text>();
        if (healthBar == null)
            healthBar = GameObject.Find("HpOverlayBar").GetComponent<Image>();

        if (currentLife <= 0 && isAlive)
        {
            isAlive = false;

            KillPlayer();
        }

        UpdateLife();
	}

    //Makes sure that the actual life and text is up to date based on stats.
    void UpdateLife()
    {
        float lifeDif = maxLife - currentLife;
        maxLife = playerHandler.GetPlayerMaxLife();
        currentLife = maxLife - lifeDif;
        SetLifeText();
        ModifyHpBar();
    }

    void KillPlayer()
    {
        DestroyObject(transform.gameObject);
        statManager.AddDeath();
    }

    public void DealDamageToPlayer(float damageToTake)
    {
        CurrentLife -= damageToTake;
        Debug.Log("Player took " + damageToTake + " damage.");
    }

    void ModifyHpBar()
    {
        healthBar.fillAmount = currentLife / maxLife;
    }

    void SetLifeText()
    {
        lifeText.text = currentLife + " / " + maxLife;
    }
}
