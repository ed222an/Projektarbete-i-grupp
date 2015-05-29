using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour 
{
    public GameObject icon;
    public int sceneToLoad;
    public int spawnPoint = 1;

    private GameObject controller;
    private bool canPress;

    void Awake()
    {
        canPress = false;
        controller = GameObject.FindWithTag("GameController");
    }

    void Update()
    {
        if (canPress)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                canPress = false;
                StartCoroutine(ChangeLevel());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPress = true;
            icon.SetActive(true);
        }
    }

    IEnumerator ChangeLevel()
    {
        if (controller == null)
            controller = GameObject.FindWithTag("GameController");
        float fadeTime = controller.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        GameController.characterStatsMenuActive = false;
        SpawnPoint.spawnAt = spawnPoint;
        Application.LoadLevel(sceneToLoad);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPress = false;
            icon.SetActive(false);
        }
    }
}
