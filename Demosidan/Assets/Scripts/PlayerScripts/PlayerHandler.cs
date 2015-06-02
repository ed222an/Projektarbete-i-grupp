using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHandler : MonoBehaviour 
{
    public PlayerStats playerStats;
    public AchievementHandler achHandler;
    private StatManager statManager;
    private PlayerLife playerLife;
    private float goldCoins;

    public float GoldCoins
    {
        get { return goldCoins; }
        set { goldCoins = value; }
    }

    private List<Item> items = new List<Item>();

    private GameObject inventory;

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        foreach (Item item in inventory.GetComponentsInChildren<Item>())
            items.Add(item);
		playerStats = GetComponent<PlayerStats>();
        achHandler = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();
        statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();
        playerLife = GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (achHandler == null)
            achHandler = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();
        if (statManager == null)
            statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();

        //Achievements related to the player
        achHandler.SetAchievementProgressByType(AchType.totalDamage, (int)GetTotalPlayerAttack());
    }

    public void AddGold(int amount)
    {
        goldCoins += amount;
        statManager.AddGold(amount);
    }

    public float GetTotalPlayerAttack()
    {
        float achDamageBonus = achHandler.GetActiveBonusByType(RewardType.damage);
        return playerStats.CalculateAttackDamage(items, achDamageBonus);
    }

    public float GetPlayerAttackSpeed()
    {
        float achAttackSpeedBonus = achHandler.GetActiveBonusByType(RewardType.atkSpd);
        return playerStats.CalculateAttackSpeed(items, achAttackSpeedBonus);
    }

    public float GetPlayerMaxLife()
    {
        float achLifeBonus = achHandler.GetActiveBonusByType(RewardType.life);
        return playerStats.CalculateMaxLife(items, achLifeBonus);
    }

    public List<Item> GetEquippedItems()
    {
        return items;
    }

    public IList<Achievement> GetFinishedAchievements()
    {
        return achHandler.GetAllCompletedAchievements();
    }

    public void SetActiveOnAllMonoComponents(bool state)
    {
        MonoBehaviour[] components = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour comp in components)
            comp.enabled = state;
    }

    public bool IsAlive()
    {
        return playerLife.IsAlive;
    }

    public void Revive()
    {
        SetActiveOnAllMonoComponents(true);
        playerLife.RevivePlayer();
    }

    public void Kill()
    {
        SetActiveOnAllMonoComponents(false);
    }
}

