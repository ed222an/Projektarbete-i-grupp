using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
    public float weaponDamage;
    public float attackSpeed;

    BoxCollider2D weaponCollider;

	// Use this for initialization
	void Start()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
	}
}
