using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GUIText restartText;
    public GameObject gameController;

    void Update()
    {
        CheckForRestart();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevelAdditive("pausemenu");
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
