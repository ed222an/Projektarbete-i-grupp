using UnityEngine;
using System.Collections;

public class TeleportHome : MonoBehaviour {

    private GameObject controller;
    public int sceneToLoad;

    void Awake()
    {
        controller = GameObject.Find("GameController");

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
            StartCoroutine(ChangeLevel());
            Debug.Log("Enter " + this.GetType().Name + " on object " + gameObject.name);
        }
    }
}
