using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickWeaponTextController : MonoBehaviour
{
    public GameObject text;

    private PickWeapon pW;

    // Start is called before the first frame update
    void Start()
    {
        pW = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetActive(pW.canPick);
    }
}
