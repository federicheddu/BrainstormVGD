using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CovidHealthBardImageChanger : MonoBehaviour
{
    public GameObject[] bosses;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(CheckBossesDeath());
    }

    IEnumerator CheckBossesDeath()
    {
        yield return new WaitUntil(() => bosses[0] != null || bosses[1] != null);
        image.sprite = Resources.Load<Sprite>("one_virus_sprite");
    }
}
