using UnityEngine;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseObject;

    private WWWPostPlayerData poster;

    void Start()
    {
        Time.timeScale = 0.0f;
        poster = GameObject.Find("GameController").GetComponent<WWWPostPlayerData>();
    }

    //Continue the game.
    public void Continue()
    {
        Time.timeScale = 1f;
        Destroy(pauseObject);
    }

    public void PostKillCount()
    {
        poster.PostPlayerKillData();
    }

    public void GoToMainMenu()
    {
        Application.LoadLevel("mainmenu");
        Time.timeScale = 1f;
        Destroy(pauseObject);
    }
}
