using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour 
{
	// Update is called once per frame
	void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            return;

        if (Input.GetKeyDown("R"))
            Application.LoadLevel(Application.loadedLevel);
	}
}
