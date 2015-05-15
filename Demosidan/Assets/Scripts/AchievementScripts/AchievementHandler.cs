using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class AchievementHandler : MonoBehaviour 
{
    private List<Achievement> achievements = new List<Achievement>();
    private Dictionary<RewardType, string> rewardTypeNames = new Dictionary<RewardType, string>();

    void Awake()
    {
        rewardTypeNames.Add(RewardType.str, "Strength");
        rewardTypeNames.Add(RewardType.dex, "Dex");
        rewardTypeNames.Add(RewardType.damage, "Damage");
        rewardTypeNames.Add(RewardType.life, "Life");
        rewardTypeNames.Add(RewardType.atkSpd, "Attack speed");
        rewardTypeNames.Add(RewardType.movementSpd, "Movement speed");
    }

	//Use this for initialization
	void Start()
    {
        achievements.Add(new Achievement("Murder", "Kill 10 monsters.", 1, RewardType.life, 3));
        achievements.Add(new Achievement("Like a bunny", "Jump 10 times.", 1, RewardType.atkSpd, 0.05f));
        achievements.Add(new Achievement("Big force in every swing", "Reach a total of 4 attack damage.", 4));
	}
	
	//Update is called once per frame
	void Update()
    {
        //TODO: A temporary way to test achieves and show them in the GUI.
        if (StatManager.statChanged)
        {
            SetAchievementProgress("Murder", StatManager.KillCount);
            SetAchievementProgress("Like a bunny", StatManager.JumpCount);
            Debug.Log("Ach stats updated");
            StatManager.statChanged = false;
        }
	}

    //Sets the achievement progress to the value supplied.
    public void SetAchievementProgress(string name, int progress)
    {
        Achievement achievement = GetAchievementByName(name);

        if (achievement == null)
        {
            Debug.Log("Achievement is null.");
            return;
        }
        else if (achievement.IsComplete())
            return;

        achievement.SetProgress(progress);
    }

    public void AddAchievementProgress(string name, int progress)
    {
        Achievement achievement = GetAchievementByName(name);

        if (achievement == null)
        {
            Debug.Log("Achievement is null.");
            return;
        }
        else if (achievement.IsComplete())
            return;

        achievement.AddProgress(progress);
    }

    public Achievement GetAchievementByName(string name)
    {
        return achievements.Find(ach => ach.AchTitle == name);
    }

    public IList<Achievement> GetAllAchievements()
    {
        return achievements.AsReadOnly();
    }

    public IList<Achievement> GetAllCompletedAchievements()
    {
        List<Achievement> compList = new List<Achievement>();
        foreach (Achievement ach in achievements)
        {
            if (ach.IsComplete())
                compList.Add(ach);
        }

        return compList.AsReadOnly();
    }

    private void LoadAchievements()
    {

    }

    public string GetRewardTypeString(RewardType type)
    {
        if (type >= RewardType.END)
            return "ERROR: Unknown item type, too big.";
        else if (type <= RewardType.NONE)
            return "ERROR: Unknown item type, too small.";

        string name;
        if (rewardTypeNames.TryGetValue(type, out name))
            return name;

        return "ERROR: Unknown item type.";
    }

    public float GetActiveBonusByType(RewardType type)
    {
        float bonus = 0.0f;

        foreach (Achievement ach in GetAllCompletedAchievements())
        {
            if (ach.RewardType == type)
                bonus += ach.RewardValue;
        }

        return bonus;
    }

    public float GetActiveMovementSpeedBonus()
    {
        return 0.0f;
    }
}
