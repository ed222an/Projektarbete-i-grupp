using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour 
{
    //TODO: This is temp while we test achieves
    public static bool statChanged = false;

    private static int killCount;
    private static int jumpCount;
    private static int deathCount;

    public static int KillCount
    {
        get { return killCount; }
    }

    public static int JumpCount
    {
        get { return jumpCount; }
    }

    public static int DeathCount
    {
        get { return deathCount; }
    }

    GUIText text;

    void Awake()
    {
        text = GetComponent<GUIText>();
        //killCount = 0;
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
