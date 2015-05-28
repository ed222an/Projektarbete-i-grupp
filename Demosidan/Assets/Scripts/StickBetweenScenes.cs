using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StickBetweenScenes : MonoBehaviour
{
    private static List<GameObject> gameObjects = new List<GameObject>();
    private string[] tagsOnStickyObjects = new string[] { "GameController", "Inventory", "Player", "UI" };
    private static bool inactiveObjects = false;

    public static int[] nonGameLevels = new int[] { 0, 17, 37, 38 };
    public static List<GameObject> objToInstantiate = new List<GameObject>();

    void Awake()
    {
        if (objToInstantiate.Count != 0)
        {
            foreach (GameObject obj in objToInstantiate)
                AddSingleObject(obj);

            objToInstantiate.Clear();
        }

        if (inactiveObjects)
        {
            foreach (GameObject obj in gameObjects)
            {
                Debug.Log("Setting " + obj + " to active.");
                obj.SetActive(true);
            }
            inactiveObjects = false;
        }

        GameObject[] objectsToAdd;

        //Add all object that shall stick between every scene.
        foreach(string tag in tagsOnStickyObjects)
        {
            objectsToAdd = GameObject.FindGameObjectsWithTag(tag);

            if (objectsToAdd.Length != 0)
            {
                if (CheckIfObjectExists(objectsToAdd))
                {
                    GameObject objectToDestroy = GetObjectForRemoval(objectsToAdd);
                    if (objectToDestroy == null)
                    {
                        Debug.LogWarning("Unable to get object for destruction in " + this.GetType() + "::Awake()");
                    }
                    else
                    {
                        Debug.Log(objectToDestroy + " destroyed Unique ID = " + objectToDestroy.GetInstanceID());
                        Destroy(objectToDestroy);
                    }                    
                }
                else
                {
                    Debug.Log("obj " + objectsToAdd[0] + " added to sticky list, unique ID = " + objectsToAdd[0].GetInstanceID());
                    DontDestroyOnLoad(objectsToAdd[0]);
                    gameObjects.Add(objectsToAdd[0]);
                }
            }
        }
    }

    void Update()
    {
        //If were in a blacklisted scene, remove all objects
        if (IsInBlacklistedScene())
        {
            HandleBlacklistedScenes();
            return;
        }
    }

    GameObject GetObjectForRemoval(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            //Only runs once, always removes the first object. 
            if (!gameObjects.Find(gObj => gObj.GetInstanceID() == obj.GetInstanceID()))
                return obj;
        }

        return null;
    }

    void HandleBlacklistedScenes()
    {
        int levelId = Application.loadedLevel;
        foreach (int level in nonGameLevels)
        {
            //If the current level is blacklisted for the objects, destroy them.
            if (levelId == level)
            {
                //Clear list from nulls
                gameObjects.RemoveAll(obj => obj == null);

                //Inactivate objects
                foreach (GameObject obj in gameObjects)
                {
                    obj.SetActive(false);
                    Debug.Log("Setting " + obj + " to inactive, blacklisted scene.");
                }

                inactiveObjects = true;
            }
        }
    }

    bool IsInBlacklistedScene()
    {
        int levelId = Application.loadedLevel;

        foreach (int level in nonGameLevels)
        {
            if (levelId == level)
                return true;
        }

        return false;
    }

    bool CheckIfObjectExists(GameObject[] otherObjs)
    {        
        foreach (GameObject aObj in otherObjs)
        {
            //Debug.Log(aObj);
            if (gameObjects.Find(obj => obj == aObj))
                return true;
        }

        return false;
    }

    private void AddSingleObject(GameObject obj)
    {
        if (obj == null)
            return;

        obj = Instantiate(obj);
        DontDestroyOnLoad(obj);
        gameObjects.Add(obj);
    }
}
