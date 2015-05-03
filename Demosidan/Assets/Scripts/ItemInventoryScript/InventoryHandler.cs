using UnityEngine;
using System.Collections;

public class InventoryHandler : MonoBehaviour 
{
    public GameObject inventoryObject;
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.I))
            CloseInventory();
	}

    public void CloseInventory()
    {
        Destroy(inventoryObject);
        GameController.inventoryActive = false;
    }
}
