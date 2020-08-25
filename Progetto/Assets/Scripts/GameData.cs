using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{

    public int level;
    public int checkpoint;
    public int weapon;

    public GameData(int Level, int Checkpoint, int Weapon)
    {
        level = Level;
        checkpoint = Checkpoint;
        weapon = Weapon;
    }
}
