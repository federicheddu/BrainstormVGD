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
                //caso nemici debbano comparire
                if (!enemies[0].activeSelf)
                {
                    foreach (GameObject enemy in enemies)
                    {
                        enemy.SetActive(true);
                    }
                    AudioManager.instance.Play("Teletrasport");
                }
                //caso del boss
                else if(enemies[0].name == "Boss")
                {
                    enemies[0].GetComponent<BossMovement>().enabled = true;
                    for (int i = 0; i < 9; i++) {
                        enemies[0].transform.GetChild(0).transform.GetChild(i + 2).gameObject.SetActive(true);
                            }
                }
                else   //caso nemici siano già presenti ma con lo scipt di azione disabilitato
                {
                    foreach (GameObject enemy in enemies)
                    {
                        enemy.GetComponent<Actions>().enabled = true;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
