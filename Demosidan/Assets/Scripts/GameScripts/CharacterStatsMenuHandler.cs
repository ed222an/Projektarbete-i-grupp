﻿using UnityEngine;
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

    void Awake()
    {
        strValue = GameObject.Find("StrValue").GetComponent<Text>();
        dexValue = GameObject.Find("DexValue").GetComponent<Text>();
        intValue = GameObject.Find("IntValue").GetComponent<Text>();
        atkDmgValue = GameObject.Find("AttackDamageValue").GetComponent<Text>();
        atkSpdValue = GameObject.Find("AttackSpeedValue").GetComponent<Text>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }
	
	void Update()
    {
        //Update values
        strValue.text = "" + playerStats.Strength; //Get the values.
        dexValue.text = "" + playerStats.Dexterity; //Get the values.
        intValue.text = "" + playerStats.Intelligence; //Get the values.

        atkDmgValue.text = "" + playerHandler.GetTotalPlayerAttack().ToString("F2");
        atkSpdValue.text = "" + playerHandler.GetPlayerAttackSpeed().ToString("F2");

        //Handle a second I press, this should close the window
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape))
            CloseStatsMenu();
	}

    public void CloseStatsMenu()
    {
        Destroy(statsMenuObject);
    }

    void OnDestroy()
    {
        GameController.characterStatsMenuActive = false;
    }
}
