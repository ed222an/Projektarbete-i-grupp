using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GUIText restartText;
    public GameObject gameController;

    //TODO: Might not want to be lazy and use a static variable here.
    public static bool characterStatsMenuActive = false;
    public static bool inventoryActive = false;

    void Update()
    {
        CheckForRestart();

        //Pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0f)
            Application.LoadLevelAdditive("pausemenu");

        //Character stats menu
        if (Input.GetKeyDown(KeyCode.C) && !characterStatsMenuActive)
        {
            Application.LoadLevelAdditive("characterinformation");
            characterStatsMenuActive = true;
        }

        //Inventory
        if (Input.GetKeyDown(KeyCode.I) && !inventoryActive)
        {
            Application.LoadLevelAdditive("inventory");
            inventoryActive = true;
        }
    }

    void CheckForRestart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
            return;

        restartText.enabled = true;
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);
    }
}
