using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static float MIN_SENS = 1f;
    public static float MAX_SENS = 6f;

    private static int roomCheckpoint = 0;
    private static int lavaCheckpoint = 0;
    private static int castleCheckpoint = 0;
    private static float mouseSensibility = 2f;
    public static int currentLevel = 1;


    public void Start()
    {
        PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if(pm != null)
        {
            pm.setMouseSensibility(mouseSensibility);
        }
    }

    public static void setRoomCheckpoint(int checkpoint)
    {
        if (checkpoint >= 0 && checkpoint <= 3)
            roomCheckpoint = checkpoint;
    }

    public static void setLavaCheckpoint(int checkpoint)
    {
        if (checkpoint >= 0 && checkpoint <= 3)
            lavaCheckpoint = checkpoint;
    }

    public static void setCastleCheckpoint(int checkpoint)
    {
        if (checkpoint >= 0 && checkpoint <= 3)
            castleCheckpoint = checkpoint;
    }

    public static int getRoomCheckpoint()
    {
        return roomCheckpoint;
    }

    public static int getLavaCheckpoint()
    {
        return lavaCheckpoint;
    }

    public static int getCastleCheckpoint()
    {
        return castleCheckpoint;
    }

    public void setMouseSensibility(float mouseSens)
    {
        if(mouseSens >= MIN_SENS && mouseSens <= MAX_SENS)
        {
            mouseSensibility = mouseSens;
            PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.setMouseSensibility(mouseSensibility);
            }
        }
    }

    public static float getMouseSensibility()
    {
        return mouseSensibility;
    }

}
