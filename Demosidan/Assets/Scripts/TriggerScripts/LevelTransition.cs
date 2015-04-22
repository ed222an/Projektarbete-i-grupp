using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour 
{
    private BoxCollider2D trigger;
    private GameObject controller;

    public GameObject icon;
    public int sceneToLoad;

    void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
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
