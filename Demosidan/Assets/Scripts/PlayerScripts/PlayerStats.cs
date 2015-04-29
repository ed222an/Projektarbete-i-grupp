using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float baseLife = 5;

    //Str stat weights.
    public float lifePerStr = 3;
    public float strAtkWeight = 0.35f;

    //Dex stat weights.
    public float dexAtkSpdWeightPercent = 0.015f; //Percent attack speed increase.

    private float strength = 5;//Life, damage
    private float dexterity = 3;// Atk speed, crit chance
    private float intelligence = 3;//Magic damage, mana

    #region get/set

    public float Strength
    {
        get { return strength; }
    }

    public float Dexterity
    {
        get { return dexterity; }
    }

    public float Intelligence
    {
        get { return intelligence; }
    }
    #endregion

    public float CalculateAttackSpeed(Weapon currentWeapon)
    {
        float attackSpeed = currentWeapon.attackSpeed;

        attackSpeed -= (attackSpeed * (dexAtkSpdWeightPercent * dexterity));

        return attackSpeed;
    }

    public float CalculateAttackDamage(Weapon currentWeapon)
    {
        float attackDamage = currentWeapon.GetDamage();

        attackDamage += strAtkWeight * strength;

        return attackDamage;
    }

    public float CalculateMaxLife()
    {
        float life = baseLife + lifePerStr * strength;

        return life;
    }
}
