using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthColorChanger : MonoBehaviour
{
    private Image i;
    public Slider slider;

    public Color happyColor = Color.green;
    public Color normalColor = Color.yellow;
    public Color badColor = Color.red;


    private void Start()
    {
        i = GetComponent<Image>();

    }
    // Update is called once per frame
    void Update()
    {
        double value = slider.value * 100 / slider.maxValue;
        if(value > 45)
        {
            i.color = happyColor;
        }
        if(value <= 45)
        {
            i.color = normalColor;
        }
        if(value <= 25)
        {
            i.color = badColor;
        }
    }
}
