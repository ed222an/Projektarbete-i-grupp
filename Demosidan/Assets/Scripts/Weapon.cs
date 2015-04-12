using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
    public float weaponDamage;

    BoxCollider2D collider;

	// Use this for initialization
	void Start()
    {
        collider = GetComponent<BoxCollider2D>();
	}
}
