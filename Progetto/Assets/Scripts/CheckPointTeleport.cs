using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTeleport : MonoBehaviour
{

    public GameObject start, check1, check2, check3;
    public Transform tr;



    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        GameSettings.SetLevelFromIndex(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // Questa riga in realtà nella build non serve. La terremo comunque per quando avviamo la scena su unity
        switch(GameSettings.GetCheckpoint())
        {
            case 1:
                tr.position = check1.transform.position;
                break;
            case 2:
                tr.position = check2.transform.position;
                break;
            case 3:
                tr.position = check3.transform.position;
                break;
            default:
                tr.position = start.transform.position;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "checkpoint1")
            GameSettings.SetCheckpoint(1);
        if (collision.transform.tag == "checkpoint2")
            GameSettings.SetCheckpoint(2);
        if (collision.transform.tag == "checkpoint3")
            GameSettings.SetCheckpoint(3);
        if (collision.transform.tag == "checkstart")
            GameSettings.SetCheckpoint(0);
    }

}
