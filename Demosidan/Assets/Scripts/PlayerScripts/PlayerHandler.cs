using UnityEngine;
using System.Collections;

//TODO: Figure out exactly what this class should handle.
public class PlayerHandler : MonoBehaviour 
{
    public PlayerStats playerStats;

    private Weapon currentWeapon;

	// Use this for initialization
	void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        playerStats = GameObject.Find("Dwarf_1").GetComponent<PlayerStats>();
	}

    public float GetTotalPlayerAttack()
    {
        return playerStats.CalculateAttackDamage(currentWeapon);
    }

    public float GetAttackSpeed()
    {
        return playerStats.CalculateAttackSpeed(currentWeapon);
    }
}
