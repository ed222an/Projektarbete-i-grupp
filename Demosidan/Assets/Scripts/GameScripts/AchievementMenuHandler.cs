using UnityEngine;
using System.Collections;

public class AchievementMenuHandler : MonoBehaviour {

    public GameObject achievementMenuObject;

    // Update is called once per frame
    void Update()
    {
        //Handle a second press, this should close the window
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Escape) || !GameController.achievementActive)
            CloseAchievementMenu();
    }

    public void CloseAchievementMenu()
    {
        Destroy(achievementMenuObject);
        GameController.achievementActive = false;
    }
}
