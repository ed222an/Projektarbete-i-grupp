using UnityEngine;
using System.Collections;

public enum RewardType
{
    NONE,
    str,
    dex,
    damage,
    life,
    atkSpd,
    movementSpd,
    END
}

public class Achievement
{
    private string achTitle;
    private string achDescription;

    private int achProgress = 0;
    private int achCompleteAt;

    private RewardType rewardType;
    private float rewardValue;

    private bool isComplete = false;

    #region get/set

    public string AchDescription
    {
        get { return achDescription; }
    }

    public string AchTitle
    {
        get { return achTitle; }
    }

    public int AchProgress
    {
        get { return achProgress; }
        set { achProgress = value; }
    }

    public int AchCompleteAt
    {
        get { return achCompleteAt; }
    }

    public RewardType RewardType
    {
        get { return rewardType; }
    }

    public float RewardValue
    {
        get { return rewardValue; }
    }

    #endregion

    public Achievement(string title, string description, int completeAt, RewardType rewardType = RewardType.NONE, float rewardValue = 0)
    {
        achTitle = title;
        achDescription = description;
        achCompleteAt = completeAt;
        this.rewardType = rewardType;
        this.rewardValue = rewardValue;
    }

    public void AddProgress(int progress)
    {
        //You can insert negative values to remove ach progress.
        if (progress > achCompleteAt)
            progress = achCompleteAt;

        achCompleteAt += progress;

        CheckIfComplete();
    }

    public void SetProgress(int progress)
    {
        if (progress < 0)
            progress = 0;
        else if (progress > achCompleteAt)
            progress = achCompleteAt;

        achProgress = progress;

        CheckIfComplete();
    }

    public bool IsComplete()
    {
        return isComplete;
    }

    void CheckIfComplete()
    {
        if (achProgress >= achCompleteAt)
            isComplete = true;
    }
}
