using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetAchievements : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/achievements/GetAchievement.php";

    private Dictionary<string, string> header = new Dictionary<string, string>();

    public IEnumerator GetAllAchievementsOnUser()
    {
        header.Clear();
        header.Add("username", CommunityUser.Username);
        WWW getAch = WWWUtility.CreateWWWWithHeaders(URL, header);

        yield return getAch;

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
