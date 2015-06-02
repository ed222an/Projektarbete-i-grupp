using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour 
{
    public GameObject icon;
    private bool canPress;
    private bool isClosed;

    private Animator anim;
    private EnemyLoot coinMake;

    void Awake()
    {
        anim = GetComponent<Animator>();
        coinMake = GetComponent<EnemyLoot>();
        canPress = false;
        isClosed = true;
    }

    void Update()
    {
        if (canPress)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isClosed)
                {
                    isClosed = false;
                    canPress = false;
                    StartCoroutine(OpenChest());
                }
            }
        }
    }

    IEnumerator OpenChest()
    {
        anim.SetTrigger("Open");

        yield return new WaitForSeconds(0.5f);

        coinMake.DropGold();

        icon.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isClosed)
        {
            if (other.tag == "Player")
            {
                icon.SetActive(true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPress = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPress = false;
            icon.SetActive(false);
        }
    }
}
