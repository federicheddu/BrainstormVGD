using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallMaterial : MonoBehaviour
{
    public GameObject[] walls;
    public Material prevMaterial;
    public Material mat;

    void Start()
    {
        StartCoroutine(Startttt());
    }

    public void ChangeTo(Material m, float delay, float duration)
    {
        StartCoroutine(Change(m, delay, duration));
    }

    private IEnumerator Change(Material m, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<MeshRenderer>().material = m;
        }
        yield return new WaitForSeconds(duration);
         for (int i = 0; i < walls.Length; i++)
         {
             walls[i].GetComponent<MeshRenderer>().material = prevMaterial;
         }
    }

    private IEnumerator Change(Material m, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<MeshRenderer>().material = m;
        }
    }

    IEnumerator Startttt()
    {
        yield return new WaitForSeconds(0f);
        StartCoroutine(Change(mat, 4, 3));
    }

}
