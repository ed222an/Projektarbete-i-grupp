using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseObject;
    public GameObject confirmationWindow;
    public GameObject loadingPanel;
    public Button uploadButton;
    public Button downloadButton;

    private WWWGetPlayerData d;
    private WWWPostPlayerData pd;
    private WWWPostAchievementData pa;
    private AchievementHandler ah;

    private bool upload;
    private bool isWorking;

    void Awake()
    {
        ah = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();

        Time.timeScale = 0.0f;
        isWorking = false;

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
        d = new WWWGetPlayerData();
        pd = new WWWPostPlayerData();
        pa = new WWWPostAchievementData();
    }

    void Update()
    {
        if (isWorking)
        {
            if (d.IsDone && pd.IsDone && pa.IsDone)
            {
                isWorking = false;
                loadingPanel.SetActive(false);
            }
        }
        else
        {
            //Handle a second press, this should close the window
            if (Input.GetKeyDown(KeyCode.Escape))
                ClosePausMenu();
        } 
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
            isWorking = true;
            loadingPanel.SetActive(true);

            if (upload && CommunityUser.IsLoggedIn)
            {
                StartCoroutine(pd.PostPlayerData());
                StartCoroutine(pa.PostAchievement(ah.GetAllAchievements()));
                //uploaddata();
            }
            else if (!upload && CommunityUser.IsLoggedIn)
            {
                StartCoroutine(d.UpdateAllStats());
                //downloaddata();
            }
        }

        confirmationWindow.SetActive(false);
    }
}
