using UnityEngine;
using System.Collections;

public class EnemyDeathEventManager : MonoBehaviour 
{
    public delegate void DeathAction();
    public static event DeathAction OnDeath;

    void OnEnable()
    {
        EnemyLife.WorkerBotDeath += EnemyDeath;
    }

    void OnDisable()
    {
        EnemyLife.WorkerBotDeath -= EnemyDeath;
    }

    private void EnemyDeath()
    {
        if (OnDeath != null)
            OnDeath(); 
    }
}
