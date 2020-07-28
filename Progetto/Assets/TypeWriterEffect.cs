using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    private string fullText;
    private Text text;
    private Coroutine c;
    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        fullText = text.text;
        text.text = "";
        finished = false;
        c = StartCoroutine(ShowText());
    }

    public bool hasFinished()
    {
        return finished;
    } 

    IEnumerator ShowText()
    {
        for(int i=0; i<fullText.Length; i+=2)
        {
            text.text = fullText.Substring(0, i+1);
            yield return new WaitForSeconds(0.0001f);
        }
        finished = true;
    }
}
