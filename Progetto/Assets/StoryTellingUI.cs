using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryTellingUI : MonoBehaviour
{
    public GameObject[] dialogues;
    public GameObject button;

    private Coroutine c;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        c = StartCoroutine(StoryTelling());
        //GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(new Vector3(0, 75, 0));
    }

    public IEnumerator StoryTelling()
    {
        for(int i=0; i<dialogues.Length; i++)
        {
            dialogues[i].SetActive(true);
            button.SetActive(false);
            TypeWriterEffect typeWriter = dialogues[i].GetComponentInChildren<TypeWriterEffect>();
            yield return new WaitUntil(() => typeWriter.hasFinished()); // Aspetto finchè il testo non è stato visualizzato tutto
            button.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            dialogues[i].SetActive(false);
        }
        button.SetActive(false);
        SceneManager.LoadScene(1);
        
    }
}
