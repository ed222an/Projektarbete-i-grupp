using UnityEngine;
using System.Collections;

public class WWWPostPlayerData : MonoBehaviour 
{
    public string URL = "http://www.metalgenre.se/api/rest/status.php";
    public string statName = "kills";
    public int userID = 1; // TEMP TESTING
    public int killCount = 1;

	// Use this for initialization
	void Start()
    {
        StartCoroutine(PostPlayerData());
	}

    IEnumerator PostPlayerData()
    {
        WWWForm form = new WWWForm();

        form.AddField("statName", statName);
        form.AddField("userID", userID);
        form.AddField("statCount", killCount);

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
