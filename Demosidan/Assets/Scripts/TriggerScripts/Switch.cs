using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
	public GameObject icon;
	public float waitTime = 0.1f;
    public GameObject target;    

	private Animator anim;
	private bool canFlip = true;
    private Component targetScript;

    void Awake()
    {
        targetScript = target.GetComponent<MonoBehaviour>();
    }

	void Start() 
	{
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
        }
		canFlip = true;
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
