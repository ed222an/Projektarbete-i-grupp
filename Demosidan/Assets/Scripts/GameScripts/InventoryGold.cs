using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryGold : MonoBehaviour 
{
    public Text gold;

    private PlayerHandler ph;

    void Update()
    {
        if (ph != null)
        {
            gold.text = ph.GoldCoins.ToString();
        }
        else
        {
            if (ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>())
                gold.text = ph.GoldCoins.ToString();   
        }
    }
}
