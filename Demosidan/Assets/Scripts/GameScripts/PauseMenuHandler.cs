using UnityEngine;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseObject;

    private WWWPostPlayerData poster;

    void Awake()
    {
        Time.timeScale = 0.0f;
        poster = GameObject.FindWithTag("GameController").GetComponent<WWWPostPlayerData>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //Handle a second press, this should close the window
        if (Input.GetKeyDown(KeyCode.Escape))
            ClosePausMenu();
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

    public void ClosePausMenu()
    {
        Time.timeScale = 1f;
        Destroy(pauseObject);
    }
}
