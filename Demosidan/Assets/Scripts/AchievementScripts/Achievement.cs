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

public enum AchType
{
    NONE,
    kill,
    jump,
    gold,
    totalDamage,
    END
}

public class Achievement
{
    private string achTitle;
    private string achDescription;

    private int achProgress = 0;
    private int achCompleteAt;

    private RewardType rewardType = RewardType.NONE;
    private float rewardValue = 0;

    private bool isComplete = false;
    private AchType achType = AchType.NONE;

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

    public AchType AchType
    {
        get { return achType; }
    }

    #endregion

    public Achievement(string title, string description, int completeAt, RewardType rewardType, float rewardValue, AchType achType)
        : this(title, description, completeAt, achType)
    {
        this.rewardType = rewardType;
        this.rewardValue = rewardValue;
    }

    public Achievement(string title, string description, int completeAt, RewardType rewardType, float rewardValue)
        : this(title, description, completeAt)
    {
        this.rewardType = rewardType;
        this.rewardValue = rewardValue;
    }

    public Achievement(string title, string description, int completeAt, AchType achType)
        : this(title, description, completeAt)
    {
        this.achType = achType;
    }

    public Achievement(string title, string description, int completeAt)
    {
        achTitle = title;
        achDescription = description;
        achCompleteAt = completeAt;
    }

    public void AddProgress(int progress)
    {
        //You can insert negative values to remove ach progress.
        if (progress > achCompleteAt)
            progress = achCompleteAt;

        achProgress += progress;

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
