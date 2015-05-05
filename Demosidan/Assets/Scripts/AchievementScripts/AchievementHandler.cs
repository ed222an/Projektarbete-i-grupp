using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementHandler : MonoBehaviour 
{
    public List<Achievement> achList = new List<Achievement>();

	// Use this for initialization
	void Start()
    {
        achList.Add(new Achievement("10 kills", "Kill 10 monsters.", 10));
        achList.Add(new Achievement("10 jumps", "Jump 10 times.", 10));
	}
	
	// Update is called once per frame
	void Update()
    {
        if (StatManager.statChanged)
            foreach (Achievement ach in achList)
            {
                if (ach.IsComplete())
                    Debug.Log(ach);
            }
	}

    private void LoadAchievements()
    {

    }
}
