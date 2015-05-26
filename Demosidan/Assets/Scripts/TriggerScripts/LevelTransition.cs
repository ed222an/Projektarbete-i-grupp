using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour 
{
    private GameObject controller;

    public GameObject icon;
    public int sceneToLoad;
    public int spawnPoint = 1;

    void Awake()
    {
        controller = GameObject.FindWithTag("GameController");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {            
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ChangeLevel());
            }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            icon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            icon.SetActive(false);
        }
    }
}
