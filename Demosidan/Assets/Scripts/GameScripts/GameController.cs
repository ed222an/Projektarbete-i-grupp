﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static GameController controller;

    private GUIText restartText;
    private GameObject inventory;
    private PlayerHandler playerHandler;

    public static bool characterStatsMenuActive = false;
    public static bool inventoryActive = false;
    public static bool achievementActive = false;

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        restartText = GameObject.Find("RestartText").GetComponent<GUIText>();
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    void Update()
    {      
        //Pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0f)
            Application.LoadLevelAdditive("pausemenu");

        if (Time.timeScale != 0f)
        {
            CheckForRestart();

            //Character stats menu
            if (Input.GetKeyDown(KeyCode.C) && !characterStatsMenuActive)
            {
                if (achievementActive)
                {
                    GameObject.Find("Achievementmenu").GetComponentInChildren<AchievementMenuHandler>().CloseAchievementMenu();
                }

                Application.LoadLevelAdditive("characterinformation");
                characterStatsMenuActive = true;
            }

            //Achievement menu
            if (Input.GetKeyDown(KeyCode.Y) && !achievementActive)
            {
                if (characterStatsMenuActive)
                {
                    GameObject.Find("CharacterInformation").GetComponentInChildren<CharacterStatsMenuHandler>().CloseStatsMenu();
                }

                Application.LoadLevelAdditive("achievementmenu");
                achievementActive = true;
            }

            //Inventory
            if (Input.GetKeyDown(KeyCode.I) && !inventoryActive)
            {
                inventory.GetComponentInChildren<Canvas>().enabled = true;
                inventoryActive = true;
            }
            else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)) && inventoryActive)
            {
                inventory.GetComponentInChildren<Canvas>().enabled = false;
                inventoryActive = false;
            }
        }   
    }

    void CheckForRestart()
    {
        if (playerHandler == null)
            playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();

        if (playerHandler.IsAlive())
        {
            restartText.enabled = false;
            return;
        }

        restartText.enabled = true;
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartText.enabled = false;
            playerHandler.Revive();
            SpawnPoint.spawnAt = 1;
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
