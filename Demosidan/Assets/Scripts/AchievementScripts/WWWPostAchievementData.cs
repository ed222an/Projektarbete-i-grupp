using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWWPostAchievementData
{
    public string URL = "http://www.metalgenre.se/api/achievements/PostAchievement.php";

    //Fields.
    const string achievement = "achievement";
    const string achievementIsDone = "achievementIsDone";
    const string username = "username";
    const string password = "password";

    private bool isDone = true;

    public bool IsDone
    {
        get { return isDone; }
    }

    public IEnumerator PostAchievement(IList<Achievement> achList)
    {
        isDone = false;

        foreach (var ach in achList)
        {
            WWWForm form = new WWWForm();

            form.AddField(achievement, ach.AchTitle);
            form.AddField(achievementIsDone, ach.IsComplete() ? 1 : 0);
            form.AddField(username, CommunityUser.Username);
            form.AddField(password, CommunityUser.Password);

            WWW w = new WWW(URL, form);
            yield return w;

            if (!string.IsNullOrEmpty(w.error))
                Debug.Log(w.error);
            else
                Debug.Log("Achievement " + ach.AchTitle + " uploaded.");
        }

        isDone = true;
    }
}
