using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementMenuHandler : MonoBehaviour {

    public GameObject achievementMenuObject;
    public GameObject statisticTab;
    public GameObject achievementTab;
    public Button achievementTabButton;
    public Button statisticsTabButton;

    // Update is called once per frame
    void Update()
    {

        Debug.Log(GameController.achievementActive);
        //Handle a second press, this should close the window
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Escape))
            CloseAchievementMenu();
    }

    public void CloseAchievementMenu()
    {
        Destroy(achievementMenuObject);
        GameController.achievementActive = false;
    }

    public void ShowStatistics()
    {
        //Disable this tabs button to avoid unnecessary interaction
        statisticsTabButton.GetComponent<Button>().interactable = false;
        //Enable the other tabs button to be able to switch
        achievementTabButton.GetComponent<Button>().interactable = true;
        //Switch color on tabs to indicate which is the current one (dark grey = current)
        statisticsTabButton.GetComponent<Image>().color = new Color32(30, 30, 30, 255);
        statisticsTabButton.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        achievementTabButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        achievementTabButton.GetComponentInChildren<Text>().color = new Color32(30, 30, 30, 255);
        //Set the tab as last sibling to draw it on top
        statisticTab.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ShowAchievements()
    {
        achievementTabButton.GetComponent<Button>().interactable = false;
        statisticsTabButton.GetComponent<Button>().interactable = true;
        achievementTabButton.GetComponent<Image>().color = new Color32(30, 30, 30, 255);
        achievementTabButton.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        statisticsTabButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        statisticsTabButton.GetComponentInChildren<Text>().color = new Color32(30, 30, 30, 255);
        achievementTab.GetComponent<RectTransform>().SetAsLastSibling();
    }
}
