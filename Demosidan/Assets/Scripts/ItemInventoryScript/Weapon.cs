using UnityEngine;
using System.Collections;

public class Weapon : Item 
{
    public float attackSpeed;

    internal float GetDamage()
    {
        return base.damage;
    }
}
