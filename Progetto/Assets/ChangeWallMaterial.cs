using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallMaterial : MonoBehaviour
{
    public GameObject[] walls;
    public Material material;

    void Start()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<MeshRenderer>().material = material;
        }
    }
}
