using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryTellingUI : MonoBehaviour
{
    public GameObject[] dialogues;
    public GameObject button;

    public StudentController michele;
    public StudentController alessandro;
    public StudentController federico;
    public StudentController luca;

    public Text talkerText;

    private Coroutine c;
    // Start is called before the first frame update
    void Start()
    {
        c = StartCoroutine(StoryTelling());
    }

    public IEnumerator StoryTelling()
    {
        for(int i=0; i<dialogues.Length; i++)
        {
            dialogues[i].SetActive(true);
            button.SetActive(false);
            string talkerName = dialogues[i].GetComponent<Talker>().talkerName;
            talkerText.text = talkerName;
            StudentController student = getStudentFromName(talkerName);
            student.Talk();
            TypeWriterEffect typeWriter = dialogues[i].GetComponentInChildren<TypeWriterEffect>();
            yield return new WaitUntil(() => typeWriter.hasFinished()); // Aspetto finchè il testo non è stato visualizzato tutto
            button.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            student.Idle();
            dialogues[i].SetActive(false);
        }

        button.SetActive(false);
        SceneManager.LoadScene(2);
        
    }

    public StudentController getStudentFromName(string name)
    {
        if (name.Equals("Federico"))
            return federico;
        if (name.Equals("Alessandro"))
            return alessandro;
        if (name.Equals("Luca"))
            return luca;
        if (name.Equals("Michele"))
            return michele;

        return null;
    }
}
