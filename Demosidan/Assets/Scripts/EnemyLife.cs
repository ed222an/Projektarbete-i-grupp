using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour 
{
    public float maxLife;

    private float currentLife;
    private bool isAlive;

#region Get/Set
    public float MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public float CurrentLife
    {
        get { return currentLife; }
    }
#endregion

    // Use this for initialization
	void Start()
    {
        currentLife = maxLife;

        isAlive = true;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (currentLife <= 0 && isAlive)
        {
            isAlive = false;

            DestroyEnemy();
        }
	}

    ////Take damage
    //IEnumerator OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.transform.tag == "Weapon")
    //        return;
    //    return;
    //}

    void DestroyEnemy()
    {


        DestroyObject(transform.gameObject);
    }
}
