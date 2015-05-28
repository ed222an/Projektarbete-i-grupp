using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWWGetPlayerData
{
    public string URL = "http://www.metalgenre.se/api/stats/GetStats.php";

    private string[] statNames = new string[] { "kills", "deaths", "jumps", "gold" };
    private Dictionary<string, string> header = new Dictionary<string, string>();
    private bool isDone = true;

    public bool IsDone
    {
        get { return isDone; }
    }

    public IEnumerator UpdateAllStats()
    {
        isDone = false;

        StatManager statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();

        int kills = 0, deaths = 0, jumps = 0, gold = 0;

        header.Clear();
        header.Add("username", CommunityUser.Username);
        WWW getStat = WWWUtility.CreateWWWWithHeaders(URL, header);

        yield return getStat;

        if (string.IsNullOrEmpty(getStat.text))
            Debug.Log(getStat.error);
        else
        {
            List<Dictionary<string, string>> dataList = SimpleJason.ConvertJSONMany(getStat.text);
            string switchString, temp;

            foreach (Dictionary<string, string> data in dataList)
            {
                data.TryGetValue("statName", out switchString);
                data.TryGetValue("statCount", out temp);

                switch (switchString)
                {
                    case "kills":
                        int.TryParse(temp, out kills);
                        break;
                    case "deaths":
                        int.TryParse(temp, out deaths);
                        break;
                    case "jumps":
                        int.TryParse(temp, out jumps);
                        break;
                    case "gold":
                        int.TryParse(temp, out gold);
                        break;
                    default:
                        break;
                }
            }
            statManager.UdpateAllStats(kills, deaths, jumps, gold);
        }

        isDone = true;
    }
}
