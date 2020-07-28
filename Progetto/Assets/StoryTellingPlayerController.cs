using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTellingPlayerController : MonoBehaviour
{
    public Transform targetTransform;
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;
    public float stopDistance = 4f;
    private Transform myTransform;
    public GameObject myLight;
    public GameObject gameLight;
    Coroutine changeLightCoroutine;
    public GameObject[] brainstormObjects;
    public GameObject storyTellingUI;

    void Awake()
    {
        myTransform = transform;
    }


    void Start()
    {
        Cursor.visible = false;
        foreach (GameObject g in brainstormObjects)
        {
            g.SetActive(false);
        }
        myLight.SetActive(true);
        gameLight.SetActive(false);
    }

    // https://answers.unity.com/questions/353675/how-to-stop-enemy-within-certain-distance-of-playe.html
    void Update()
    {
        float dist = Vector3.Distance(targetTransform.position, transform.position);
        
        // direzione
        Vector3 targetDir = targetTransform.position - myTransform.position;
        targetDir.y = 0; // per evitare che tilti
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetDir), rotationSpeed * Time.deltaTime);
        if (dist > stopDistance)
        {
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            //changeLightCoroutine = StartCoroutine(ChangeLight());

            myLight.SetActive(false);
            gameLight.SetActive(true);
            StartCoroutine(StartBrainstorm());
        }
    }

    IEnumerator StartBrainstorm()
    {
        // Attivazione brainstorm per ogni studente
        foreach (GameObject g in brainstormObjects)
        {
            yield return new WaitForSeconds(1f);
            g.SetActive(true);
        }

        //Aspetto che tutte le parole siano state "branstormate". Controllo il numero dei figli perchè viene fatta una destroy delle parole
        foreach (GameObject g in brainstormObjects)
        {
            yield return new WaitUntil(() => g.transform.childCount == 0);
        }

        //Attivazione dialoghi
        storyTellingUI.SetActive(true);

    }

}