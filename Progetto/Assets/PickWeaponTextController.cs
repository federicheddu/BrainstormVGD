using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickWeaponTextController : MonoBehaviour
{
    private PickWeapon pW;
    private Text t;

    // Start is called before the first frame update
    void Start()
    {
        pW = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickWeapon>();
        t = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pW.canPick)
        {

        }
        else
        {

        }
    }
}
