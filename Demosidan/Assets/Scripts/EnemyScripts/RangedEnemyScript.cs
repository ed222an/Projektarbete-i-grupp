using UnityEngine;
using System.Collections;

public class RangedEnemyScript : MonoBehaviour 
{
    public float searchLazerLength;
    public GameObject bullet;
    public LayerMask rayCastLayers;
    public float attackIntervalTime = 1f;

    public bool turretMode = true;

    private bool lazerActive = true;

    private float attackInterval = 0.2f;

    private RaycastHit2D hit;

	// Use this for initialization
	void Start()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
        if (turretMode)
        {
            //ShowLazer();

            if (UpdateAttackInterval())
            {
                StartCoroutine(Shoot());
                Shoot();
            }
        }
        else
        {
            if (hit != null && hit.transform.gameObject.tag != "Player")
            {
                UpdateLazerPosition();
            }
            else if (hit != null && hit.transform.gameObject.tag == "Player")
            {
                Shoot();
            }
        }
	}

    private void UpdateLazerPosition()
    {
        throw new System.NotImplementedException();
    }

    private bool UpdateAttackInterval()
    {
        if (attackInterval > 0)
        {
            attackInterval -= Time.deltaTime;
            return false;
        }
        else
        {
            attackInterval = attackIntervalTime;
            return true;
        }
    }

    IEnumerator Shoot()
    {
        CreateBullet();
        yield return new WaitForSeconds(0.45f);
        CreateBullet();
        yield return new WaitForSeconds(0.45f);
        CreateBullet();
    }

    void CreateBullet()
    {
        GameObject bulletObj = Instantiate(bullet, transform.position + new Vector3(0.2f, 0.2f, 0f), Quaternion.identity) as GameObject;
        EnemyBullet bulletScript = bulletObj.GetComponent<EnemyBullet>();
        bulletScript.Instantiate(this.gameObject, rayCastLayers);
    }

    //TODO: Create a lazer
    public void ShowLazer()
    {
        hit = Physics2D.Linecast(transform.position + new Vector3(0f, 0.2f, 0f), transform.position + new Vector3(searchLazerLength, 0.2f, 0f), rayCastLayers);
        Debug.DrawLine(transform.position, transform.position + new Vector3(searchLazerLength, 0f, 0f));
    }
}
