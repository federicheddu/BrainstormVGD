using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuLabelsColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Color normalColor;
    public Color onHoverColor;
    public Image borders;
    public Image background;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = onHoverColor;
        if (borders != null)
        {
            Image i = borders.GetComponent<Image>();
            if (i != null)
            {
                i.color = onHoverColor;
            }
        }
        if (background != null)
        {
            Image i = background.GetComponent<Image>();
            if (i != null)
            {
                Color c = i.color;
                c.a = 0.65f;
                i.color = c;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
        if (borders != null)
        {
            Image i = borders.GetComponent<Image>();
            if (i != null)
            {
                i.color = normalColor;
            }
        }
        if (background != null)
        {
            Image i = background.GetComponent<Image>();
            if (i != null)
            {
                Color c = i.color;
                c.a = 0.3f;
                i.color = c;
            }
        }
    }

}
