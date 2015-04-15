using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GUIText restartText;

    private bool isPaused = false;
	// Update is called once per frame
	void Update()
    {
        CheckForRestart();
	}

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || isPaused)
        {
            isPaused = true;
            Time.timeScale = 0.0f;

            GUI.Box(new Rect(400, 75, 300, 400), "Menu");

            if (GUI.Button(new Rect(470, 150, 150, 20), "Continue"))
                isPaused = false;

            if (GUI.Button(new Rect(470, 200, 150, 20), "Back to Main Menu"))
            {
                isPaused = false;
                Application.LoadLevel("mainmenu");
            }

            if (!isPaused)
                Time.timeScale = 1.0f;
        }
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
