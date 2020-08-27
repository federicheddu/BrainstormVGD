using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoUIController : MonoBehaviour
{
    private PickWeapon pickWeaponScript;
    private GameObject currentWeapon;

    public Slider ammoSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        pickWeaponScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickWeapon>();
        if (pickWeaponScript == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ammoSlider.gameObject.SetActive(pickWeaponScript.getActiveWeaponGameObject() != null);
    }
}
