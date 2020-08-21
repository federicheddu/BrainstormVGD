using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemis : MonoBehaviour
{
    GameObject player;
    public GameObject[] enemies;
    bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (flag)
            {
                flag = false;
                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                }
                AudioManager.instance.Play("Teletrasport");
                Destroy(gameObject);
            }
        }
    }
}
