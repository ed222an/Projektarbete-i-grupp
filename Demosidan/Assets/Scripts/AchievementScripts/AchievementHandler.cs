using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementHandler : MonoBehaviour 
{
    public List<Achievement> achievements = new List<Achievement>();

	// Use this for initialization
	void Start()
    {
        achievements.Add(new Achievement("Murder.", "Kill 10 monsters.", 10));
        achievements.Add(new Achievement("Like a bunny.", "Jump 10 times.", 10));
        achievements.Add(new Achievement("Great force in every swing.", "Reach a total of 10 attack damage.", 1));
	}
	
	// Update is called once per frame
	void Update()
    {
        //TODO: A temporary way to test achieves and show them in the upcoming GUI.
        if (StatManager.statChanged)
        {
            SetAchievementProgress("Murder", StatManager.KillCount);
            SetAchievementProgress("Like a bunny.", StatManager.JumpCount);

            StatManager.statChanged = false;
        }
	}

    //Sets the achievement progress to the value supplied.
    public void SetAchievementProgress(string name, int progress)
    {
        Achievement achievement = GetAchievementByName(name);

        if (achievements == null)
        {
            Debug.Log("Achievement is null.");
            return;
        }

        achievement.SetProgress(progress);
    }

    public void AddAchievementProgress(string name, int progress)
    {
        Achievement achievement = GetAchievementByName(name);

        if (achievement == null)
        {
            Debug.Log("Achievement is null.");
        }

        achievement.AddProgress(progress);
    }

    public Achievement GetAchievementByName(string name)
    {
        return achievements.Find(ach => ach.achTitle == name);
    }

    private void LoadAchievements()
    {

    }
}
