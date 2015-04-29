using UnityEngine;
using System.Collections;

public class Weapon : Item 
{
    public float attackSpeed;

    void Awake()
    {

    }

	// Use this for initialization
	void Start()
    {
        
	}

    internal float GetDamage()
    {
        return base.damage;
    }
}
