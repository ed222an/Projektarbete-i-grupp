using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AchievementMenuHandler : MonoBehaviour {

    public GameObject achievementMenuObject;
    public GameObject statisticTab;
    public GameObject achievementTab;
    public GameObject achievementDisplayObject;
    public Button achievementTabButton;
    public Button statisticsTabButton;
    public Sprite completeImage;

    private GameObject contentPanel;

    private IList<Achievement> achList;
    private List<GameObject> achievementDisplayObjects = new List<GameObject>();

    void Awake()
    {
        contentPanel = GameObject.Find("Content Panel");
    }

    void Start()
    {
        achList = GameObject.Find("GameController").GetComponent<AchievementHandler>().achievements;

        foreach (Achievement ach in achList)
        {
            GameObject achDispObj = GameObject.Instantiate(achievementDisplayObject);
            achDispObj.transform.SetParent(contentPanel.transform, false);
            achievementDisplayObjects.Add(achDispObj);
        }

        UpdateAchievements();
    }

    // Update is called once per frame
    void Update()
    {
        if (achList == null)
        {
            Debug.LogWarning("Achlist is null or empty (No GameController object in scene?)");
            return;
        }

        //Handle a second press, this should close the window
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Escape))
            CloseAchievementMenu();

        UpdateAchievements();
    }

    private void UpdateAchievements()
    {
        if (achievementDisplayObjects != null)
        {
            for (int i = 0; i < achievementDisplayObjects.Count; i++)
            {
                AchievementDisplay display = achievementDisplayObjects[i].GetComponent<AchievementDisplay>();
                display.titleText.text = achList[i].achTitle;
                if (achList[i].IsComplete())
                    display.achievementImage.GetComponent<Image>().sprite = completeImage;
                display.descriptionText.text = achList[i].achDescription;
                if (achList[i].achProgress != 0)
                {
                    display.progressBar.fillAmount = ((float)achList[i].achProgress / (float)achList[i].achCompleteAt);
                }
                display.progressValue.text = achList[i].achProgress.ToString() + " / " + achList[i].achCompleteAt.ToString();
            }
        }
    }

    public void CloseAchievementMenu()
    {
        Destroy(achievementMenuObject);
    }

    public void ShowStatistics()
    {
        //Switch color on tabs to indicate which is the current one (dark grey = current)
        SwitchButton(statisticsTabButton, achievementTabButton);
        //Set the tab as last sibling to draw it on top
        statisticTab.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ShowAchievements()
    {
        SwitchButton(achievementTabButton, statisticsTabButton);

        achievementTab.GetComponent<RectTransform>().SetAsLastSibling();
    }

    void SwitchButton(Button chosenButton, Button otherButton)
    {
        chosenButton.GetComponent<Button>().interactable = false;
        otherButton.GetComponent<Button>().interactable = true;

        chosenButton.GetComponent<Image>().color = new Color32(30, 30, 30, 255);
        chosenButton.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        otherButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        otherButton.GetComponentInChildren<Text>().color = new Color32(30, 30, 30, 255);
    }

    void OnDestroy()
    {
        GameController.achievementActive = false;
    }
}
