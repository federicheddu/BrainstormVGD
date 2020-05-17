using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MenuLabelsColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Color normalColor;
    public Color onHoverColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = onHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}
