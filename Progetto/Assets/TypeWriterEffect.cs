using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.001f;
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
        StartShowingText();
    }

    public void StartShowingText()
    {
        c = StartCoroutine(ShowText());
    }

    public bool hasFinished()
    {
        return finished;
    } 

    IEnumerator ShowText()
    {
        finished = false;
        for(int i=0; i<fullText.Length; i++)
        {
            text.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
        finished = true;
        StopCoroutine(c);
    }
}
