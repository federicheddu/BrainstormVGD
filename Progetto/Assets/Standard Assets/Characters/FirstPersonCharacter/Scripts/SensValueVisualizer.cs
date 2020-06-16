using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SensValueVisualizer : MonoBehaviour
{

    public GameObject player;
    public Slider slider;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        slider.value = (float) Math.Floor(player.GetComponent<PlayerMovement>().mouseSens * 100 / slider.maxValue);
    }

    /* Questo codice era in update, ma per ottimizzare questo metodo viene chiamato solo quando il valore dello slider viene cambiato, così
       non viene eseguito ad ogni frame*/
    public void ShowValue()
    {
        double value = Math.Floor(player.GetComponent<PlayerMovement>().mouseSens * 100 / slider.maxValue);
        text.text = value.ToString();
    }
}
