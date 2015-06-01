using UnityEngine;
using System.Collections;

public class EnemyLoot : MonoBehaviour 
{
    public GameObject goldObj;
    public int goldCoinAmount = 0;

    void Start()
    {
        if (goldCoinAmount == 0)
        goldCoinAmount = Random.Range(0, 3) + 1;
    }

    public void DropGold(int amount = 1)
    {
        if (goldObj != null)
        {
            for(int i = 0; i < goldCoinAmount; i++)
                Instantiate(goldObj, transform.position, transform.rotation);
        }
    }
}
