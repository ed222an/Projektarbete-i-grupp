using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetAchievements : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/achievements/GetAchievement.php";

    private Dictionary<string, string> header = new Dictionary<string, string>();
    private Dictionary<string, string> dic = new Dictionary<string, string>();

    void Start()
    {

    }

    public IEnumerator GetAllAchievementsOnUser()
    {
        header.Clear();
        header.Add("username", CommunityUser.Username);
        WWW getAch = WWWUtility.CreateWWWWithHeaders(URL, header);

        yield return getAch;

        Dictionary<string, string> myDic = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(getAch.error))
            print(getAch.error);
        else
        {
            List<Dictionary<string, string>> values = SimpleJason.ConvertJSONMany(getAch.text);
            AchievementHandler achHandler = GameObject.FindWithTag("GameController").GetComponent<AchievementHandler>();
            foreach (Dictionary<string, string> d in values)
            {
                string achName, achProgress;
                int achProg = 0;

                d.TryGetValue("achievement", out achName);
                d.TryGetValue("achievementIsDone", out achProgress);
                int.TryParse(achProgress, out achProg);

                achHandler.SetAchievementProgress(achName, achProg);
            }
        }
    }
}
