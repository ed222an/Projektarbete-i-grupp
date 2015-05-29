using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelection : MonoBehaviour 
{
    public Text selectedDwarf;
    public GameObject miner;
    public GameObject monk;
    public GameObject engineer;
    public GameObject loadingImage;

    private GameObject chosenDwarf;

    public void SelectMiner()
    {
        selectedDwarf.text = "Selected Dwarf : The Miner";
        chosenDwarf = miner;
    }

    public void SelectMonk()
    {
        selectedDwarf.text = "Selected Dwarf : The Monk";
        chosenDwarf = monk;
    }

    public void SelectEngineer()
    {
        selectedDwarf.text = "Selected Dwarf : The Engineer";
        chosenDwarf = engineer;
    }

    public void BackToMenu()
    {
        loadingImage.SetActive(true);
        Application.LoadLevel(0);
    }

    public void StartPlaying()
    {
        if (chosenDwarf == null)
            return;

        StickBetweenScenes.objToInstantiate = chosenDwarf;
        SpawnPoint.spawnAt = 1;

        loadingImage.SetActive(true);
        Application.LoadLevel(3);
    }
}
