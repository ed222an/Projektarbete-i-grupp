using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatsMenuHandler : MonoBehaviour 
{
    public GameObject statsMenuObject;

    private Text strValue;
    private Text dexValue;
    private Text intValue;
    private Text atkDmgValue;
    private Text atkSpdValue;
    private PlayerStats playerStats;
    private PlayerHandler playerHandler;

	// Use this for initialization
	void Start()
    {
        strValue = GameObject.Find("StrValue").GetComponent<Text>();
        dexValue = GameObject.Find("DexValue").GetComponent<Text>();
        intValue = GameObject.Find("IntValue").GetComponent<Text>();
        atkDmgValue = GameObject.Find("AttackDamageValue").GetComponent<Text>();
        atkSpdValue = GameObject.Find("AttackSpeedValue").GetComponent<Text>();
        playerStats = PlayerStats.playerStats;

        //TODO: We want to get the player, not the specific dwarf.
        playerHandler = GameObject.Find("Dwarf_1").GetComponent<PlayerHandler>();
	}
	
	// Update is called once per frame
	void Update()
    {
        //Update values
        strValue.text = "" + playerStats.Strength; //Get the values.
        dexValue.text = "" + playerStats.Dexterity; //Get the values.
        intValue.text = "" + playerStats.Intelligence; //Get the values.

        atkDmgValue.text = "" + playerHandler.GetTotalPlayerAttack();
        atkSpdValue.text = "" + playerHandler.GetPlayerAttackSpeed();

        //Handle a second I press, this should close the window
        if (Input.GetKeyDown(KeyCode.C))
            CloseStatsMenu();
	}

    public void CloseStatsMenu()
    {
        Destroy(statsMenuObject);
        GameController.characterStatsMenuActive = false;
    }
}
