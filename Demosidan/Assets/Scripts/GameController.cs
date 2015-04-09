using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GUIText restartText;
	// Update is called once per frame
	void Update()
    {
        CheckForRestart();
	}

    void CheckForRestart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            return;

        restartText.enabled = true;

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);
    }
}
