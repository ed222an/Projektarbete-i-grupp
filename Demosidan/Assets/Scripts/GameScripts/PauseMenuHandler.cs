using UnityEngine;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour 
{
    public GameObject pauseObject;

    void Start()
    {
        Time.timeScale = 0.0f;
    }

    //Continue the game.
    public void Continue()
    {
        Time.timeScale = 1f;
        Destroy(pauseObject);
    }

    public void PostKillCount()
    {
        WWWPostPlayerData poster = new WWWPostPlayerData();
        poster.PostPlayerKillData();
    }

    public void GoToMainMenu()
    {
        Application.LoadLevel("mainmenu");
        Time.timeScale = 1f;
        Destroy(pauseObject);
    }
}
