using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacityChanger : MonoBehaviour
{
    public void setOpacity(float opacity)
    {
        Color c = GetComponent<Image>().color;
        c.a = opacity;
        GetComponent<Image>().color = c;
    }
}
