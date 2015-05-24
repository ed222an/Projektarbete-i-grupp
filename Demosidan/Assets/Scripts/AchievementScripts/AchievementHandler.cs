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
        rewardTypeNames.Add(RewardType.dex, "Dexterity");
        rewardTypeNames.Add(RewardType.damage, "Damage");
        rewardTypeNames.Add(RewardType.life, "Life");
        rewardTypeNames.Add(RewardType.atkSpd, "Attack speed");
        rewardTypeNames.Add(RewardType.movementSpd, "Movement speed");
    }

	//Use this for initialization
	void Start()
    {
        achievements.Add(new Achievement("Murder", "Kill 10 enemies.", 10, RewardType.life, 3));
        achievements.Add(new Achievement("Mass murder", "Kill 50 enemies.", 1, RewardType.life, 6));
        achievements.Add(new Achievement("Who likes robots anyways", "Kill 100 enemies.", 1, RewardType.life, 12));
        achievements.Add(new Achievement("A robots worst enemy", "Kill 500 enemies.", 1, RewardType.life, 25));
        achievements.Add(new Achievement("NOT SET", "Kill 5000 enemies.", 1, RewardType.life, 25));

        achievements.Add(new Achievement("Like a bunny", "Jump 10 times.", 1));
        achievements.Add(new Achievement("Like a bunny 2", "Jump 100 times.", 1, RewardType.damage, 1));
        achievements.Add(new Achievement("Like a bunny 3", "Jump 500 times.", 1, RewardType.atkSpd, 0.03f));
        achievements.Add(new Achievement("Like a bunny 4", "Jump 5000 times.", 1, RewardType.atkSpd, 0.09f));

        achievements.Add(new Achievement("Everyone starts with a penny", "Collect your first gold coin.", 1));
        achievements.Add(new Achievement("More than a penny", "Collect 50 gold.", 1, RewardType.atkSpd, 0.02f));
        achievements.Add(new Achievement("Golden riches", "Collect a total of 500 gold.", 1, RewardType.atkSpd, 0.05f));
        achievements.Add(new Achievement("Gold, GOLD GOOOOOLD", "Collect a total of 5000 gold.", 1, RewardType.damage, 10));

        achievements.Add(new Achievement("Big force in every swing", "Reach a total of 4 attack damage.", 4, RewardType.str, 1));
        achievements.Add(new Achievement("Swing like a truck", "Reach a total of 40 attack damage.", 4, RewardType.str, 3));

        achievements.Add(new Achievement("Hot ride", "Get in your hot, metallish vehicle.", 1));


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
