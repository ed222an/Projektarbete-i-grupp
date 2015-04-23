using UnityEngine;
using System.Collections;

//TODO: Figure out exactly what this class should handle.
public class PlayerHandler : MonoBehaviour 
{
    public PlayerStats playerStats;

    private Weapon currentWeapon;

    void Awake()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
	// Use this for initialization
	void Start()
    {
        
	}

    public float GetTotalPlayerAttack()
    {
        return playerStats.CalculateAttackDamage(currentWeapon);
    }

    public float GetPlayerAttackSpeed()
    {
        return playerStats.CalculateAttackSpeed(currentWeapon);
    }

    public float GetPlayerMaxLife()
    {
        return playerStats.CalculateMaxLife();
    }
}
