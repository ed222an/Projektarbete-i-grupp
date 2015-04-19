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
	}

    public float GetTotalPlayerAttack()
    {
        return playerStats.CalculateAttackDamage(currentWeapon);
    }

    //TODO: Temp hard coded value for testing.
    public float GetAttackSpeed()
    {
        return playerStats.CalculateAttackSpeed(currentWeapon);
    }
}
