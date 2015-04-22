using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
    public float weaponDamage;
    public float attackSpeed;

    BoxCollider2D weaponCollider;

    void Awake()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start()
    {
        
	}
}
