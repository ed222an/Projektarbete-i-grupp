using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHandler : MonoBehaviour 
{
    public PlayerStats playerStats;

    private List<Item> items = new List<Item>();

    private GameObject inventory;

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        foreach (Item item in inventory.GetComponentsInChildren<Item>())
            items.Add(item);
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
	// Use this for initialization
	void Start()
    {
        
	}

    public float GetTotalPlayerAttack()
    {
        return playerStats.CalculateAttackDamage(items);
    }

    public float GetPlayerAttackSpeed()
    {
        return playerStats.CalculateAttackSpeed(items);
    }

    public float GetPlayerMaxLife()
    {
        return playerStats.CalculateMaxLife(items);
    }

    public List<Item> GetEquippedItems()
    {
        return items;
    }
}
