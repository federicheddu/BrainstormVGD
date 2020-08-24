using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static float MIN_SENS = 1f;
    public static float MAX_SENS = 6f;


    public void Start()
    {
        PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if(pm != null && PlayerPrefs.HasKey("mouseSens"))
        {
            pm.setMouseSensibility(GetMouseSensibility());
        }
    }

    public static void SetCheckpoint(int checkpoint)
    {
        if (checkpoint >= 0 && checkpoint <= 3)
        {
            PlayerPrefs.SetInt("checkpoint", checkpoint);
            PlayerPrefs.Save();
        }
    }

    public static void SetLevel(int level)
    {
        if (level >= 0 && level <= 3)
        {
            PlayerPrefs.SetInt("level", level);
            PlayerPrefs.Save();
        }
            
    }

    public void SetMouseSensibility(float mouseSens)
    {
        if(mouseSens >= MIN_SENS && mouseSens <= MAX_SENS)
        {
            PlayerPrefs.SetFloat("mouseSens", mouseSens);
            PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.setMouseSensibility(GetMouseSensibility());
            }
            PlayerPrefs.Save();
        }
    }

    public static float GetMouseSensibility()
    {
        return PlayerPrefs.GetFloat("mouseSens", 2f);
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 1);
    }

    public static int GetCheckpoint()
    {
        return PlayerPrefs.GetInt("checkpoint", 0);
    }

}
