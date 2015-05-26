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
	private Animator anim;

    #region Get/Set
    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }

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
        statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();
		playerHandler = gameObject.GetComponent<PlayerHandler>();
		anim = gameObject.GetComponent<Animator>();
        IsAlive = true;
    }

    // Use this for initialization
	void Start()
    {
        ResetLife();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (lifeText == null)
            lifeText = GameObject.Find("HpBarText").GetComponent<Text>();
        if (healthBar == null)
            healthBar = GameObject.Find("HpOverlayBar").GetComponent<Image>();

        if (currentLife <= 0 && IsAlive)
        {
            IsAlive = false;

            StartCoroutine(KillPlayer());
        }

        UpdateLife();
	}

    public void ResetLife()
    {
        currentLife = maxLife = playerHandler.GetPlayerMaxLife();
        SetLifeText();
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

    public void RevivePlayer()
    {
        anim.SetBool("Dying", false);
        anim.ResetTrigger("Die");
        IsAlive = true;
        ResetLife();
    }

    private IEnumerator KillPlayer()
    {
        playerHandler.Kill();
        statManager.AddDeath();

		anim.SetBool("Dying", true);
		anim.SetTrigger("Die");

		yield return new WaitForSeconds (3.0f); // Testing death animation.
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
