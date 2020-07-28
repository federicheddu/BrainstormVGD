using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainstormWordsController : MonoBehaviour
{
    public GameObject[] wordsObject;
    private bool finished;

    private Coroutine brainstormCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        foreach(GameObject g in wordsObject)
        {
            g.SetActive(false);
        }
        brainstormCoroutine = StartCoroutine(Brainstorm());
    }


    IEnumerator Brainstorm()
    {
        for(int i=0; i<wordsObject.Length; i++)
        {
            wordsObject[i].SetActive(true);
            Debug.Log(wordsObject[i].GetComponentInChildren<UnityEngine.UI.Text>().text);
            yield return new WaitUntil(() => wordsObject[i].GetComponent<BrainstormEffect>().hasFinished());
            Destroy(wordsObject[i]);
        }
        finished = true;
    }

    public bool hasFinished() { return finished; }
}
