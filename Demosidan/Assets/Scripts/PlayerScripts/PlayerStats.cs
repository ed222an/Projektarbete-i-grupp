using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    //Set this to false to reflect stat changes, like when a gear part has been upgraded, removed or added, anything that changes the current stats.
    public static bool baseStatHasChanged = true;

    public float baseLife = 5;

    //Str stat weights.
    public float lifePerStr = 3;
    public float strAtkWeight = 0.35f;

    //Dex stat weights.
    public float dexAtkSpdWeightPercent = 0.015f; //Percent attack speed increase.

    private float strength;//Life, damage
    private float dexterity;// Atk speed, crit chance
    private float intelligence;//Magic damage, mana

    //TODO: Static values right now, might want to have these differ depending on character.
    private float baseStrength = 5;
    private float baseDexterity = 3;
    private float baseIntelligence = 3;

    private PlayerHandler playerHandler;

    #region get/set

    public float Strength
    {
        get { return strength; }
        set 
        {
            if (value < 0)
                value = 0;
            strength = value; 
        }
    }

    public float Dexterity
    {
        get { return dexterity; }
        set 
        {
            if (value < 0)
                value = 0;
            dexterity = value; 
        }
    }

    public float Intelligence
    {
        get { return intelligence; }
        set 
        {
            if (value < 0)
                value = 0;
            intelligence = value; 
        }
    }
    #endregion

    void Awake()
    {
        playerHandler = GetComponent<PlayerHandler>();
        UpdateBaseStats();
    }

    void Update()
    {
        if (baseStatHasChanged)
        {
            UpdateBaseStats();
        }
    }

    public float CalculateAttackSpeed(List<Item> items)
    {
        Weapon weapon = items.Find(item => item.itemType == ItemTypes.Weapon) as Weapon;

        float attackSpeed;

        if (weapon != null)
             attackSpeed = weapon.attackSpeed;
        else
            attackSpeed = 0.5f;

        float atkSpdPercentIncrease = 0.0f;

        foreach (Item item in items)
        {
            atkSpdPercentIncrease += item.atkSpdBonusPercent;
        }

        //Old way without any kind of diminishing.
        //attackSpeed -= (attackSpeed * (dexAtkSpdWeightPercent * dex + atkSpdPercentIncrease));
        
        //Debug.Log("Actual % increase : " + ((atkSpdPercentIncrease + dex * dexAtkSpdWeightPercent) * 100));
        //Debug.Log("After dimnish : " + ((0.7f * (1 - Mathf.Pow(1 + (dexAtkSpdWeightPercent * dex + atkSpdPercentIncrease), -1f))) * 100));

        //Can't say if this is even 90% Correct, but this will make sure the bonus attackspeed % can never go over 70%
        //TODO: Check back on this formula.
        attackSpeed -= (attackSpeed * (0.7f * (1 - Mathf.Pow(1 + (dexAtkSpdWeightPercent * Dexterity + atkSpdPercentIncrease), -1f))));
        
        return attackSpeed;
    }

    public float CalculateAttackDamage(List<Item> items)
    {
        float itemDamage = 0;

        foreach (Item item in items)
        {
            itemDamage += item.damage;
        }

        float totalDamage = itemDamage + strAtkWeight * Strength;

        return totalDamage;
    }

    public float CalculateMaxLife(List<Item> items)
    {
        float lifeFromItems = 0;

        foreach (Item item in items)
        {
            lifeFromItems += item.bonusLife;

            //Would be pretty stupid if negative bonus life could kill you
            if (lifeFromItems < -baseLife + 1)
                lifeFromItems = -baseLife + 1;
        }

        float totalLife = baseLife + lifeFromItems + lifePerStr * Strength;

        return totalLife;
    }

    public void UpdateBaseStats()
    {
        Strength = baseStrength;
        Dexterity = baseDexterity;
        Intelligence = baseIntelligence;

        foreach (Item item in playerHandler.GetEquippedItems())
        {
            Strength += item.strAmount;
            Dexterity += item.dexAmount;
            Intelligence += item.intAmount;
        }
        //TODO: Uncomment this when testing stats are finished. Not even sure it's needed, performance?
        //baseStatHasChanged = false;
    }
}
