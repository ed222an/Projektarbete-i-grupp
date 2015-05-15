using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillAllEnemies : MonoBehaviour 
{
    public Transform enemies;
    public GameObject unlockTarget;
    public Text completionText;
    public bool killAll;
    public int totalEnemies;

    private int enemiesKilled;
    private Text objectiveText;
    private string objectiveString;
    private bool objectiveIsDone;

    void Awake()
    {
        objectiveText = GetComponent<Text>();
        objectiveString = "Kill all enemies ";
        EnemyDeathEventManager.OnDeath += AddKill;
    }

    void OnDisable()
    {
        EnemyDeathEventManager.OnDeath -= AddKill;
    }

	void Start () 
    {
        if (killAll)
        {
            totalEnemies = enemies.childCount;
        }      
	}

    private void AddKill()
    {
        Debug.Log("Killed one");
        enemiesKilled += 1;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (!objectiveIsDone)
        {
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
