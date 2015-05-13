using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Switch : MonoBehaviour
{
	public GameObject icon;
	public float waitTime = 0.1f;
    public GameObject target;
    public GameObject objective;
    public Text completionText;
    public bool onAndOffSwitch;

	private Animator anim;
	private bool canFlip = true;
    private Component targetScript;
    private Text objectiveText;

    void Awake()
    {
        targetScript = target.GetComponent<MonoBehaviour>();
    }

	void Start() 
	{
        objectiveText = objective.GetComponent<Text>();
		anim = gameObject.GetComponent<Animator> ();
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		{            
			if (Input.GetKeyDown(KeyCode.E))
			{
				if(canFlip)
				{
					canFlip = false;
					StartCoroutine(FlipSwitch());
				}
			}
		}
	}
	
	IEnumerator FlipSwitch()
	{
		anim.SetTrigger ("Flip");
        
		yield return new WaitForSeconds (waitTime);
        if (targetScript is ISwitch)
        {
            ISwitch script = (ISwitch)targetScript;
            script.SwitchAction();
            objectiveText.color = Color.green;
            objectiveText.text += " DONE";
            StartCoroutine("showCompletionText");
        }

        if (onAndOffSwitch)
            canFlip = true;
        else
            icon.SetActive(false);
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
	
	void OnTriggerEnter2D(Collider2D other)
	{
        if (canFlip)
        {
            if (other.tag == "Player")
            {
                icon.SetActive(true);
            }
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
