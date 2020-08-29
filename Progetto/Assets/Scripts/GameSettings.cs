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
            Debug.Log("Checkpoint: " + GetCheckpoint().ToString());
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

    public static GunType GetWeapon()
    {
        switch(PlayerPrefs.GetInt("weapon",3))
        {
            case 0: return GunType.Pistol;
            case 1: return GunType.Assault;
            case 2: return GunType.LMG;
            default: return GunType.None;
        }
    }
    public static void SetWeapon(GunType weapon)
    {
        switch (weapon)
        {
            case GunType.Pistol: PlayerPrefs.SetInt("weapon", 0); break;
            case GunType.Assault: PlayerPrefs.SetInt("weapon", 1); break;
            case GunType.LMG: PlayerPrefs.SetInt("weapon", 2); break;
            default: PlayerPrefs.SetInt("weapon",3); break;
        }
        PlayerPrefs.Save();
    }

    public static void SetLevelFromIndex(int index)
    {
        if (index == 2)
            SetLevel(1);
        if (index == 4)
            SetLevel(2);
        if (index == 6)
            SetLevel(3);
    }

}
