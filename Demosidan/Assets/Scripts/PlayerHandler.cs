using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour 
{

    private Weapon currentWeapon;

	// Use this for initialization
	void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
	}

    public float GetTotalPlayerAttack()
    {
        return currentWeapon.weaponDamage;
    }
}
