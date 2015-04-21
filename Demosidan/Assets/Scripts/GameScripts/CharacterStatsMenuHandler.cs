using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatsMenuHandler : MonoBehaviour 
{
    public GameObject statsMenuObject;

    private Text strValue;
    private Text dexValue;
    private Text intValue;
    private PlayerStats playerStats;

	// Use this for initialization
	void Start()
    {
        strValue = GameObject.Find("StrValue").GetComponent<Text>();
        dexValue = GameObject.Find("DexValue").GetComponent<Text>();
        intValue = GameObject.Find("IntValue").GetComponent<Text>();

        //Tried this with FindGameObjectWithTag, but that's not working for some reason.
        playerStats = GameObject.Find("Dwarf_1").GetComponent<PlayerStats>();

	}
	
	// Update is called once per frame
	void Update()
    {
        if (playerStats == null)
        {
            Debug.Log("Unable to find PlayerStats on Dwarf_1");
            return;
        }
        //Update values
        strValue.text = "" + playerStats.Strength; //Get the values.
        dexValue.text = "" + playerStats.Dexterity; //Get the values.
        intValue.text = "" + playerStats.Intelligence; //Get the values.
	}

    public void CloseStatsMenu()
    {
        Destroy(statsMenuObject);
        GameController.characterStatsMenuActive = false;
    }
}
