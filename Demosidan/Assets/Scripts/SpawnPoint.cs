using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public static int spawnAt;

    public int spawnPointId;

	// Use this for initialization
	void Start()
    {
        if (spawnAt == spawnPointId)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

            foreach(GameObject obj in player)
                obj.transform.position = transform.position;
        }
	}
}
