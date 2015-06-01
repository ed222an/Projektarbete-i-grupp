using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AchievementMenuHandler : MonoBehaviour {

    public GameObject achievementMenuObject;
    public GameObject statisticTab;
    public GameObject achievementTab;
    public GameObject achievementDisplayObject;
    public GameObject achievementDisplayRewardObject;
    public Button achievementTabButton;
    public Button statisticsTabButton;
    public Sprite completeImage;
    public Text kills;
    public Text jumps;
    public Text gold;
    public Text deaths;

    private GameObject contentPanel;
    private StatManager statMan;

    private IList<Achievement> achList;
    private List<GameObject> achievementDisplayObjects = new List<GameObject>();

    void Awake()
    {
        contentPanel = GameObject.Find("Content Panel");
        if (GameObject.FindGameObjectWithTag("GameController"))
        {
            statMan = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatManager>();
        }
    }

    void Start()
    {
        AchievementHandler achHandler = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();

        achList = achHandler.GetAllAchievements();

        foreach (Achievement ach in achList)
        {
            GameObject achDispObj;

            if (ach.RewardType == RewardType.NONE)
                achDispObj = GameObject.Instantiate(achievementDisplayObject);
            else//If there's a reward on the achievement, instantiate a reward object and set the reward.
            {
                achDispObj = GameObject.Instantiate(achievementDisplayRewardObject);
                AchievementDisplay display = achDispObj.GetComponent<AchievementDisplay>();
                if (ach.RewardType == RewardType.atkSpd)
                    display.rewardText.text = "Reward: " + (ach.RewardValue * 100).ToString() + "% " + achHandler.GetRewardTypeString(ach.RewardType);
                else
                    display.rewardText.text = "Reward: " + ach.RewardValue.ToString() + " " + achHandler.GetRewardTypeString(ach.RewardType);
            }
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
        UpdateLifetimeStats();
    }

    private void UpdateLifetimeStats()
    {
        kills.text = statMan.KillCount.ToString();
        jumps.text = statMan.JumpCount.ToString();
        gold.text = statMan.GoldCount.ToString();
        deaths.text = statMan.DeathCount.ToString();
    }

    private void UpdateAchievements()
    {
        if (achievementDisplayObjects != null)
        {
            for (int i = 0; i < achievementDisplayObjects.Count; i++)
            {
                AchievementDisplay display = achievementDisplayObjects[i].GetComponent<AchievementDisplay>();
                display.titleText.text = achList[i].AchTitle;
                if (achList[i].IsComplete())
                {
                    display.achievementImage.GetComponent<Image>().sprite = completeImage;
                    if (achList[i].RewardType != RewardType.NONE)
                        display.rewardText.GetComponentInParent<Image>().color = new Color32(246, 233, 75, 255);
                }

                display.descriptionText.text = achList[i].AchDescription;
                if (achList[i].AchProgress != 0)
                {
                    display.progressBar.fillAmount = ((float)achList[i].AchProgress / (float)achList[i].AchCompleteAt);
                }
                display.progressValue.text = achList[i].AchProgress.ToString() + " / " + achList[i].AchCompleteAt.ToString();
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
