using UnityEngine;
using System.Collections;

public class KillCountManager : MonoBehaviour 
{
    private static int killCount;

    public static int KillCount
    {
        get { return killCount; }
    }

    GUIText text;


    void Awake()
    {
        text = GetComponent<GUIText>();
        killCount = 0;
    }
	
	void Update () 
    {
        text.text = "Kill Count: " + killCount;
	}

    public static void AddKill(int amount = 1)
    {
        killCount += amount;
    }
}
