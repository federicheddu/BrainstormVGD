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
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        c = StartCoroutine(StoryTelling());
    }

    public bool hasFinished()
    {

        return finished;
    }

    public IEnumerator StoryTelling()
    {
        for(int i=0; i<dialogues.Length; i++)
        {
            dialogues[i].SetActive(true);
            button.SetActive(false); // Disattivazione scritta "press any key to continue"
            TalkerName talkerName = dialogues[i].GetComponent<Talker>().talkerName; // Recupero il nome dello studente che deve parlare
            talkerText.text = talkerName.ToString(); // Nella UI il nome della persona che parla cambia nel nome dello studente
            StudentController student = getStudentFromName(talkerName); // Recupero il gameobject dello studente che deve parlare
            student.Talk(); // Lo studente inizia a parlare
            TypeWriterEffect typeWriter = dialogues[i].GetComponentInChildren<TypeWriterEffect>(); // Nella UI iniziano ad apparire le scritte del dialogo
            yield return new WaitUntil(() => typeWriter.hasFinished()); // Aspetto finchè il testo non è stato visualizzato tutto
            button.SetActive(true); // Attivazione scritta "press any key to continue"
            yield return new WaitUntil(() => Input.anyKeyDown); // L'utente preme qualsiasi cosa per andare avanti
            student.Idle(); // Lo studente che ha appena parlato si mette in idle
            dialogues[i].SetActive(false); 
        }

        button.SetActive(false);
        talkerText.gameObject.SetActive(false);
        finished = true;
    }

    public StudentController getStudentFromName(TalkerName name)
    {
        if (name == TalkerName.Federico)
            return federico;
        if (name == TalkerName.Alessandro)
            return alessandro;
        if (name == TalkerName.Luca)
            return luca;
        if (name == TalkerName.Michele)
            return michele;

        return null;
    }
}
