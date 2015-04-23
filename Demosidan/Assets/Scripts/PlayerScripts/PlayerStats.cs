using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float baseLife = 5;

    //Str stat weights.
    public float lifePerStr = 3;
    public float strAtkWeight = 0.35f;

    //Dex stat weights.
    public float dexAtkSpdWeight = 0.03f;

    private int strength = 50000;//Life, damage
    private int dexterity = 3;// Atk speed, crit chance
    private int intelligence = 3;//Magic damage, mana

    #region get/set

    public int Strength
    {
        get { return strength; }
    }

    public int Dexterity
    {
        get { return dexterity; }
    }

    public int Intelligence
    {
        get { return intelligence; }
    }
    #endregion

    public float CalculateAttackSpeed(Weapon currentWeapon)
    {
        float attackSpeed = currentWeapon.attackSpeed;

        attackSpeed -= dexAtkSpdWeight * dexterity;

        return attackSpeed;
    }

    public float CalculateAttackDamage(Weapon currentWeapon)
    {
        float attackDamage = currentWeapon.weaponDamage;

        attackDamage += strAtkWeight * strength;

        return attackDamage;
    }

    public float CalculateMaxLife()
    {
        float life = baseLife + lifePerStr * strength;

        return life;
    }
}
