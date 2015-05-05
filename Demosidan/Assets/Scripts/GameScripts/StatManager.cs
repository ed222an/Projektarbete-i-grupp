using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour 
{
    public static bool statChanged = false;

    private static int killCount;
    private static int jumpCount;
    private static int deathCount;

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
        statChanged = true;
    }

    public static void AddJump(int amount = 1)
    {
        jumpCount += amount;
        statChanged = true;
    }

    public static void AddDeath(int amount = 1)
    {
        deathCount += amount;
        statChanged = true;
    }
}
