using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour 
{
    private int killCount;
    private int jumpCount;
    private int deathCount;
    private int goldCount;

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

    public int GoldCount
    {
        get { return goldCount; }
    }

    private AchievementHandler achHandler;

    void Awake()
    {
        achHandler = GetComponent<AchievementHandler>();
    }

    public void UdpateAllStats(int kills, int deaths, int jumps, int gold)
    {
        killCount = kills;
        achHandler.SetAchievementProgressByType(AchType.kill, kills);
        jumpCount = jumps;
        achHandler.SetAchievementProgressByType(AchType.jump, jumps);
        deathCount = deaths;
        goldCount = gold;
        achHandler.SetAchievementProgressByType(AchType.gold, gold);
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

    public void AddGold(int amount = 1)
    {
        goldCount += amount;
        achHandler.AddAchievementProgressByType(AchType.gold, amount);
    }
}
