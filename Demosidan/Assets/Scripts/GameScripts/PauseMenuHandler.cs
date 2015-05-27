using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseObject;
    public GameObject confirmationWindow;
    public Button uploadButton;
    public Button downloadButton;

    private bool upload;

    void Awake()
    {
        Time.timeScale = 0.0f;

        if (!CommunityUser.IsLoggedIn)
        {
            uploadButton.interactable = false;
            downloadButton.interactable = false;
        }
        else
        {
            uploadButton.interactable = true;
            downloadButton.interactable = true;
        }
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

    public void GoToMainMenu()
    {
        Application.LoadLevel("mainmenu");
        Time.timeScale = 1f;
        SpawnPoint.spawnAt = 1;
        Destroy(pauseObject);
    }

    public void ClosePausMenu()
    {
        Time.timeScale = 1f;
        Destroy(pauseObject);
    }

    public void SyncData(bool upload)
    {
        this.upload = upload;
        confirmationWindow.SetActive(true);
    }

    public void ChoiceConfirmation(bool doAction)
    {
        if (doAction)
        {
            if (upload)
            {
                //uploaddata();
            }
            else
            {
                //downloaddata();
            }
        }

        confirmationWindow.SetActive(false);
    }
}
