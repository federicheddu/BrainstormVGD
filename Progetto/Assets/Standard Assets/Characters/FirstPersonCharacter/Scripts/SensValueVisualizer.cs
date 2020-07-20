using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SensValueVisualizer : MonoBehaviour
{

    private Slider slider;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        slider = GetComponentInParent<Slider>();
        ShowValue();
    }

    /* Questo codice era in update, ma per ottimizzare questo metodo viene chiamato solo quando il valore dello slider viene cambiato, così
       non viene eseguito ad ogni frame*/
    public void ShowValue()
    {
        double value = Math.Floor((slider.value-slider.minValue)*(100/(slider.maxValue-slider.minValue)));
        text.text = String.Concat(value.ToString(), "%");
    }
}
