using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour 
{
    private int killCount;
    private int jumpCount;
    private int deathCount;

    public int KillCount
    {
        get { return killCount; }
    }

    public int JumpCount
    {
        get { return jumpCount; }
    }

    public int DeathCount
    {
        get { return deathCount; }
    }

    private AchievementHandler achHandler;

    void Awake()
    {
        achHandler = GetComponent<AchievementHandler>();
    }

    public void AddKill(int amount = 1)
    {
        killCount += amount;
        achHandler.AddAchievementProgressByType(AchType.kill, 1);
    }

    public void AddJump(int amount = 1)
    {
        jumpCount += amount;
        achHandler.AddAchievementProgressByType(AchType.jump, 1);
    }

    public void AddDeath(int amount = 1)
    {
        deathCount += amount;
    }
}
