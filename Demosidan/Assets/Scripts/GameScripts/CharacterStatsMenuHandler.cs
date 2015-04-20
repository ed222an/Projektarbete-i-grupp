using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatsMenuHandler : MonoBehaviour 
{
    public GameObject statsMenuObject;

    public Text strValue;
    public Text dexValue;
    public Text intValue;

	// Use this for initialization
	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update()
    {
	    

        //Update values
        strValue.text = "" + 0; //Get the values.
        dexValue.text = "" + 0; //Get the values.
        intValue.text = "" + 0; //Get the values.
	}

    public void CloseStatsMenu()
    {
        Destroy(statsMenuObject);
        GameController.characterStatsMenuActive = false;
    }
}
