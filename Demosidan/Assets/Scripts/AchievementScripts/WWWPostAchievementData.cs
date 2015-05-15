using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWWPostAchievementData : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/achievements/PostAchievement.php";
    public string userUsername = "Admin";
    public string userPassword = "Password";

    //Fields.
    const string achievement = "achievement";
    const string achievementIsDone = "achievementIsDone";
    const string username = "username";
    const string password = "password";

    public void PostAchievementData(Achievement ach)
    {
        StartCoroutine(PostAchievement(ach));
    }

    public void PostAllAchievements(List<Achievement> achList)
    {
        foreach (Achievement ach in achList)
        {
            StartCoroutine(PostAchievement(ach));
        }
    }

    IEnumerator PostAchievement(Achievement ach)
    {
        WWWForm form = new WWWForm();

        form.AddField(achievement, ach.AchTitle);
        form.AddField(achievementIsDone, ach.IsComplete() ? 1 : 0);
        form.AddField(username, userUsername);
        form.AddField(password, AES.encrypt(userPassword));

        WWW w = new WWW(URL, form);
        yield return w;

        if (!string.IsNullOrEmpty(w.error))
            print(w.error);
        else
            print("Achievement " + ach.AchTitle + " uploaded.");
    }
}
