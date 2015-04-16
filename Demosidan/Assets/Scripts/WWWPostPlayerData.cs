using UnityEngine;
using System.Collections;

public class WWWPostPlayerData : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/rest/status.php";
    public string statName = "kills";
    public string username = "Admin";
    public string password = "Password";

    private int killCount;

	// Use this for initialization
	void Start()
    {
        StartCoroutine(PostPlayerData());
	}

    IEnumerator PostPlayerData()
    {
        killCount = KillCountManager.KillCount;

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
            print("Kill counted uploaded.");

        //Remove the object that this script belongs to from the scene when it's done.
        DestroyObject(gameObject);
    }
}
