using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainstormWordsController : MonoBehaviour
{
    public GameObject[] wordsObject;
    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        foreach(GameObject g in wordsObject)
        {
            g.SetActive(false);
        }
        StartCoroutine(Brainstorm());
    }


    IEnumerator Brainstorm()
    {
        for(int i=0; i<wordsObject.Length; i++)
        {
            // Il game object si occuperà all'attivazione di fare l'effetto brainstorm, quindi andare verso il giocatore
            wordsObject[i].SetActive(true);
            // Si aspetta finchè non finisce, poi si elimina e si passa alla parola successiva
            yield return new WaitUntil(() => wordsObject[i].GetComponent<BrainstormEffect>().hasFinished());
            Destroy(wordsObject[i]);
        }
        finished = true; 
    }

    public bool hasFinished() { return finished; }
}
