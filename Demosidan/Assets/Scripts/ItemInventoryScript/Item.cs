using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemTypes
{
    None = 0,
    Weapon,
    Helm,
    BodyArmor,
    Gloves,
    Pants,
    Boots,
    End
}

public class Item : MonoBehaviour 
{
    private Dictionary<ItemTypes, string> itemTypeName = new Dictionary<ItemTypes, string>();

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
        itemTypeName.Add(ItemTypes.Weapon, "Weapon");
        itemTypeName.Add(ItemTypes.Helm, "Helmet");
        itemTypeName.Add(ItemTypes.BodyArmor, "Body Armor");
        itemTypeName.Add(ItemTypes.Gloves, "Gloves");
        itemTypeName.Add(ItemTypes.Pants, "Pants");
        itemTypeName.Add(ItemTypes.Boots, "Boots");
    }

    public string GetTypeNameString(ItemTypes type)
    {
        if (type >= ItemTypes.End)
            return "ERROR: Unknown item type, too big.";
        else if (type <= ItemTypes.None)
            return "ERROR: Unknown item type, too small.";

        string name;
        if (itemTypeName.TryGetValue(type, out name))
            return name;

        return "ERROR: Unknown item type.";
    }
}
