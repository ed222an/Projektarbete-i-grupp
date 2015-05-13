using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillAllEnemies : MonoBehaviour 
{
    public Transform enemies;
    public GameObject unlockTarget;
    public Text completionText;

    private int totalEnemies;
    private int enemiesKilled;
    private Text objectiveText;
    private string objectiveString;
    private bool objectiveIsDone;

    void Awake()
    {
        objectiveText = GetComponent<Text>();
        objectiveString = "Kill all enemies ";
    }

	void Start () 
    {
        totalEnemies = enemies.childCount;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!objectiveIsDone)
        {
            enemiesKilled = totalEnemies - enemies.childCount;
            objectiveText.text = objectiveString + enemiesKilled + " / " + totalEnemies;

            if (enemiesKilled == totalEnemies)
            {
                objectiveIsDone = true;
                objectiveText.color = Color.green;
                objectiveText.text += " DONE";
                unlockTarget.SetActive(false);

                StartCoroutine("showCompletionText");
            }
        }        
	}

    //Shows what happened after completing the objective and fades it out
    IEnumerator showCompletionText()
    {
        completionText.enabled = true;
        yield return new WaitForSeconds(1f);       

        float fadeDuration = 1f;
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            Color newColor = completionText.color;
            newColor.a = Mathf.Lerp(1, 0, currentTime / fadeDuration);
            completionText.color = newColor;
            currentTime += Time.deltaTime;
            yield return null;
        }

        completionText.enabled = false;
        yield break;
    }
}
