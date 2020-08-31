using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightFinished : MonoBehaviour
{
    public GameObject[] toDisableOnStart;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in toDisableOnStart)
        {
            if(g != null)
            {
                g.SetActive(false);
            }
        }

        AudioManager am = AudioManager.instance;
        if (am != null)
        {
            am.Victory();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(7);
        }
    }
}
