using UnityEngine;
using System.Collections;

public class Achievement
{
    public string achTitle;
    public string achDescription;

    public int achProgress = 0;
    public int achCompleteAt;

    private bool isComplete = false;

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
