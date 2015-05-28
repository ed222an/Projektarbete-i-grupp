using UnityEngine;
using System.Collections;

public class WWWPostPlayerData : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/stats/PostStats.php";

    private string[] statNames = new string[] { "kills", "deaths", "jumps", "gold" };

    public IEnumerator PostPlayerData()
    {
        StatManager statManager = GameObject.FindWithTag("GameController").GetComponent<StatManager>();

        foreach (string statName in statNames)
        {
            WWWForm form = new WWWForm();
            form.AddField("statName", statName);
            switch (statName)
            {
                case "kills":
                    form.AddField("statCount", statManager.KillCount);
                    break;
                case "deaths":
                    form.AddField("statCount", statManager.DeathCount);
                    break;
                case "jumps":
                    form.AddField("statCount", statManager.JumpCount);
                    break;
                case "gold":
                    form.AddField("statCount", statManager.GoldCount);
                    break;
                default:
                    break;
            }
            form.AddField("username", CommunityUser.Username);
            form.AddField("password", CommunityUser.Password);

            WWW w = new WWW(URL, form);
            yield return w;

            if (!string.IsNullOrEmpty(w.error))
                print(w.error);
            else
                print(statName + " uploaded successfully");
        }
    }
}
