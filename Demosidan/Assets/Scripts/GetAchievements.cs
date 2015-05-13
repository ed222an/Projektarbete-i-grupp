using UnityEngine;
using System.Collections;

public class GetAchievements : MonoBehaviour {

    public string urltoach = "http://www.metalgenre.se/api/achievements/GetAchievement.php?username=Admin";

    void Start()
    {
        Get();
    }

    public void Get()
    {
        StartCoroutine(GetAllAchievements());
    }

    IEnumerator GetAllAchievements()
    {
        WWW getach = new WWW(urltoach);

        yield return getach;

        if (!string.IsNullOrEmpty(getach.error))
            print(getach.error);
        else
            print(getach.text);
    }
}
