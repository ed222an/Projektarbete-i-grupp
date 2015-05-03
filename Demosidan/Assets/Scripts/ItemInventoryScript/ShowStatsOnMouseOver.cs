using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowStatsOnMouseOver : MonoBehaviour 
{
    public GameObject infoPanels;

    //Name
    public Text itemName;
    public Text itemType;

    //Attributes
    //Primary
    public Text strValue;
    public Text dexValue;
    public Text intValue;
    //Secondary
    public Text dmgValue;
    public Text AtkSpdValue;
    public Text bonusLifeValue;

    private Item itemInSlot;

    void Awake()
    {
        //TODO: We might want to find all the text fields here, just to make it easier?

        itemInSlot = GetComponentInChildren<Item>();
    }

    public void MouseEnter()
    {
        Debug.Log("Enter");
        Item currentItem = gameObject.GetComponentInChildren<Item>();

        itemName.text = itemInSlot.name;
        itemType.text = itemInSlot.GetTypeNamesString(itemInSlot.itemType);

        strValue.text = itemInSlot.strAmount.ToString();
        dexValue.text = itemInSlot.dexAmount.ToString();
        intValue.text = itemInSlot.intAmount.ToString();

        dmgValue.text = itemInSlot.damage.ToString();
        AtkSpdValue.text = (itemInSlot.atkSpdBonusPercent * 100).ToString();
        bonusLifeValue.text = itemInSlot.bonusLife.ToString();

        infoPanels.SetActive(true);

    }

    public void MouseExit()
    {
        Debug.Log("Exit");
        infoPanels.SetActive(false);
    }
}
