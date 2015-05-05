using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class ItemType
{
    public List<string> typeNames = new List<string>();

    public ItemType()
    {
        typeNames.Add("Weapon");
        typeNames.Add("Helmet");
        typeNames.Add("Body Armor");
        typeNames.Add("Gloves");
        typeNames.Add("Pants");
        typeNames.Add("Boots");
    }

    public string GetTypeName(ItemTypes index)
    {
        return typeNames[(int)index];
    }
}

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
    private ItemType itemTypeNames = new ItemType();

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

    }

	// Use this for initialization
	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public string GetTypeNameString(ItemTypes type)
    {
        return itemTypeNames.GetTypeName(type);
    }
}
