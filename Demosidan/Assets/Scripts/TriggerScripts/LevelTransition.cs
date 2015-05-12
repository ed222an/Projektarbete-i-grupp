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
        controller = GameObject.Find("GameController");

    }
	void Start() 
    {
        
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("you pressed e in a trigger");
                StartCoroutine(ChangeLevel());
            }
        }
    }

    IEnumerator ChangeLevel()
    {
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
            Debug.Log("Enter " + this.GetType().Name + " on object " + gameObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            icon.SetActive(false);
            Debug.Log("exit ladder trigger");
        }
    }
}
