using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;

    private int strength = 10;//Life, damage
    private int dexterity = 5;// Atk speed, crit chance
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

    //Str stat weights.
    private float strAtkWeight = 0.35f;

    //Dex stat weights.
    private float dexAtkSpdWeight = 0.03f;

    void Awake()
    {
        if (playerStats == null)
        {
            DontDestroyOnLoad(gameObject);
            playerStats = this;
        }
        else if (playerStats != this)
            Destroy(gameObject);

    }

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
}
