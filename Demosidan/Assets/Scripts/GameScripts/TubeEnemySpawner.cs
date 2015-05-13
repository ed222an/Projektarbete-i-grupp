using UnityEngine;
using System.Collections;

public class TubeEnemySpawner : MonoBehaviour, ISwitch
{

    public Transform startTransform;
    public Transform endTransform;
    public Transform enemyParent;
    public float moveSpeed;
    public GameObject workerbotPrefab;
    public bool spawnerEnabled;

    private bool changeDirection;
    private float moveDirection;
    private bool canMove;
    private bool isSpawning;
    private float spawnTime;
    private float maxTime;
    private float minTime;   

    void Awake()
    {
        canMove = true;
        moveDirection = Random.Range(0, 2);
        if (moveDirection == 0)
            moveDirection = -1;

        minTime = 5;
        maxTime = 10;

        SetSpawnTime();
    }

    public void SwitchAction()
    {
        spawnerEnabled = !spawnerEnabled;

        if (spawnerEnabled)
        {
            SetSpawnTime();
            canMove = true;
            isSpawning = false;
        }
        else
        {
            StopCoroutine("TimedEnemySpawn");
        }
    }

    private void SetSpawnTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
        spawnTime = 5;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (spawnerEnabled)
        {
            if (spawnTime > 0)
                spawnTime -= Time.deltaTime;
            else if (spawnTime <= 0 && isSpawning == false)
            {
                isSpawning = true;
                StartCoroutine("TimedEnemySpawn");
            }

            if (transform.position.x < startTransform.position.x || transform.position.x > endTransform.transform.position.x)
                changeDirection = true;

            if (canMove)
                Move();
        }                  
	}

    IEnumerator TimedEnemySpawn()
    {
        canMove = false;

        yield return new WaitForSeconds(1);
        GameObject enemy = (GameObject)Instantiate(workerbotPrefab, transform.position, transform.rotation);
        enemy.transform.SetParent(enemyParent, false);
        yield return new WaitForSeconds(1);

        canMove = true;
        isSpawning = false;

        SetSpawnTime();
    }

    void Move()
    {
        if (changeDirection)
        {
            moveDirection *= -1;
            changeDirection = false;
        }

        if (moveDirection == 1)
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
