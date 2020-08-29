using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{

    public int level;
    public int checkpoint;
    public GunType weapon;

    public GameData(int Level, int Checkpoint, GunType Weapon)
    {
        level = Level;
        checkpoint = Checkpoint;
        weapon = Weapon;
    }
}
