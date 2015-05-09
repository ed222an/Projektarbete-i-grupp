using UnityEngine;
using System.Collections;

public class LevelDoor : MonoBehaviour, ISwitch 
{
    public GameObject door;

    private bool doorisLocked;

    void Awake()
    {
        doorisLocked = true;
    }

    public void SwitchAction()
    {
        doorisLocked = !doorisLocked;

        if (doorisLocked)
            door.SetActive(true);
        else
            door.SetActive(false);
    }
}
