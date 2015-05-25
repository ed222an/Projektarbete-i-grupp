using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHandler : MonoBehaviour 
{
    public PlayerStats playerStats;
    public AchievementHandler achHandler;
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
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        achHandler = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();
    }
	// Use this for initialization
	void Start()
    {
        
	}

    void Update()
    {
        if (achHandler == null)
            achHandler = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();

        //Achievements related to the player
        achHandler.SetAchievementProgressByType(AchType.totalDamage, (int)GetTotalPlayerAttack());
    }

    public void AddGold(int amount)
    {
        goldCoins += amount;
        achHandler.AddAchievementProgressByType(AchType.gold, amount);
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
}
