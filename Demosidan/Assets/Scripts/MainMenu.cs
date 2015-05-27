using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public GameObject loggedInPanel;
	
	void Update () 
    {
        if (CommunityUser.IsLoggedIn)
        {
            loggedInPanel.SetActive(true);
        }
        else
        {
            loggedInPanel.SetActive(false);
        }
	}
}
