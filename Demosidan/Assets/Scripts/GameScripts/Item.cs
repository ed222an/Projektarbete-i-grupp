using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemTypes
{
    Weapon = 0,
    Helm,
    BodyArmor,
    Gloves,
    Pants,
    Boots
}

public class Item : MonoBehaviour 
{
    private Dictionary<ItemTypes, string> itemTypeNames = new Dictionary<ItemTypes, string>();

    public string itemName;

    public ItemTypes itemType;

    public float damage;
    
    public float strAmount;
    public float dexAmount;
    public float intAmount;

    //Should be defined as 0.01 = 1% increase.
    public float atkSpdBonusPercent;
    public float bonusLife;

    void Awake()
    {
        itemTypeNames.Add(ItemTypes.Weapon, "Weapon");
        itemTypeNames.Add(ItemTypes.Helm, "Helm");
        itemTypeNames.Add(ItemTypes.BodyArmor, "BodyArmor");
        itemTypeNames.Add(ItemTypes.Gloves, "Gloves");
        itemTypeNames.Add(ItemTypes.Pants, "Pants");
        itemTypeNames.Add(ItemTypes.Boots, "Boots");
    }

	// Use this for initialization
	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public string GetTypeNamesString(ItemTypes type)
    {
        return itemTypeNames[type];
    }
}
