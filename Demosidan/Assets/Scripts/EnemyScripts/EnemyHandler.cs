﻿using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour 
{
    public float followRangeRadiusX = 3;
    public float followRangeRadiusY = 1;
    public Transform target;
    public float speed;
    public LayerMask playerLayer;

    private Rigidbody2D rBody;

    private EnemyStats enemyStats;
	// Use this for initialization
	void Start() 
    {
        rBody = GetComponent<Rigidbody2D>();
        enemyStats = GetComponent<EnemyStats>();
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        //bool isInRange = Physics2D.OverlapCircle(transform.position, followRange, playerLayer);
        bool isInRange = Physics2D.OverlapArea(new Vector2(transform.position.x - followRangeRadiusX, transform.position.y - followRangeRadiusY),
                                                new Vector2(transform.position.x + followRangeRadiusX, transform.position.y + followRangeRadiusY), playerLayer);

        if (target != null && isInRange)
            FollowTarget();
    }

    void FollowTarget()
    {
        if (transform.position.x < target.position.x)
            rBody.velocity = new Vector2(1f * speed, rBody.velocity.y);
        else
            rBody.velocity = new Vector2(-1f * speed, rBody.velocity.y);
    }

    public void KnockbackOnHit(float playerPosX, float enemyPosX)
    {
        if (playerPosX > enemyPosX)
        {
            rBody.AddForce(new Vector2(1200f, 0f));
        }
        else
        {
            rBody.AddForce(new Vector2(-1200f, 0f));
        }
    }

    //The idea is that this function gets all stats, flat damage and combines it to a single value, then is returned to the caller.
    public float GetTotalAttack()
    {
        return enemyStats.damage;
    }
}