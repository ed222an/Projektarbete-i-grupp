using UnityEngine;
using System.Collections;

public class WWWPostPlayerData : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/stats/PostStats.php";
    public string statName = "kills";
    public string username = "Admin";
    public string password = "Password";

    public void PostPlayerKillData()
    {
        StartCoroutine(PostPlayerData());
    }

    IEnumerator PostPlayerData()
    {
        int killCount = GameObject.FindWithTag("StatManager").GetComponent<StatManager>().KillCount; //TODO: This should be passed to the function instead

        WWWForm form = new WWWForm();

        form.AddField("statName", statName);
        form.AddField("statCount", killCount);
        form.AddField("username", username);
        form.AddField("password", AES.encrypt(password));

        Debug.Log("encrypted PW: " + AES.encrypt(password) + " name: " + username + " Kills: " + killCount);

        WWW w = new WWW(URL, form);
        yield return w;

        if (!string.IsNullOrEmpty(w.error))
            print(w.error);
        else
            print("Kill count uploaded.");
    }
}
