using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{

    public MapPoint up, right, down, left;

    public bool isLevel;
    public bool isLocked;

    public string lvlToLoad, lvlToCkeck, lvlName;


    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && lvlToLoad != null)
        {
            isLocked = true;

            if(lvlToCkeck != null)
            {
                if(PlayerPrefs.HasKey(lvlToCkeck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(lvlToCkeck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    } 
                }
            }

            if(lvlToLoad == lvlToCkeck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
