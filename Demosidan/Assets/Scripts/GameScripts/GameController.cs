using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static GameController controller;
    public static int[] nonGameLevels = new int[] { 0 };

    public GUIText restartText;
    public GameObject gameController;
    public GameObject inventory;

    //TODO: Might not want to be lazy and use a static variable here.
    public static bool characterStatsMenuActive = false;
    public static bool inventoryActive = false;
    public static bool achievementActive = false;

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        restartText = GameObject.Find("RestartText").GetComponent<GUIText>();
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
                //TODO: Compile stats and achievements in one scene to eliminate the issue with closing and opening a new scene (looks like flickering)
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
                //TODO: Compile stats and achievements in one scene to eliminate the issue with closing and opening a new scene (looks like flickering)
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            restartText.enabled = false;
            return;
        }

        restartText.enabled = true;
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartText.enabled = false;
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
