using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntroductionSceneController : MonoBehaviour
{
    public enum FlowMode { oneByOne, atTheSameTime };
    public enum Level { Bedroom, Lava, Castle, None };
    public enum LevelNumber { One = 1, Two = 2, Three = 3 };


    // Fields
    public string playerTag = "Player";
    public GameObject gameLight;
    public GameObject[] brainstormObjects;

    [Header("Uis")]
    public GameObject storyTellingUi;
    public GameObject finalCountdownUi;

    [Header("Scene Settings")]
    public FlowMode flowMode;
    [Range(0f, 1.5f)] public float delayBetweenBrainstorms = 0.5f ;
    public Level nextScene;

    private GameObject player;
    GameObject manager;
    private void Start()
    {
        manager = GameObject.Find("AudioManager");
        player = GameObject.FindGameObjectWithTag(playerTag);

        Cursor.visible = false;
        foreach (GameObject g in brainstormObjects)
        {
            g.SetActive(false);
        }
        gameLight.SetActive(false);

        StartCoroutine(Flow());
    }

    IEnumerator Flow()
    {
        StoryTellingPlayerController playerStory = player.GetComponent<StoryTellingPlayerController>();
        yield return new WaitUntil(() => !playerStory.isInFrontOfTheTable());
        gameLight.SetActive(true);
        // Attivazione brainstorm per ogni studente
        foreach (GameObject g in brainstormObjects)
        {
            yield return new WaitForSeconds(delayBetweenBrainstorms);
            g.SetActive(true);
        }
        if (flowMode.Equals(FlowMode.oneByOne)) // Solo se sono uno per volta
        {
            //Aspetto che tutte le parole siano state "branstormate". Controllo il numero dei figli perchè viene fatta una destroy delle parole
            foreach (GameObject g in brainstormObjects)
            {
                yield return new WaitUntil(() => g.transform.childCount == 0);
            }
        }
        if(storyTellingUi != null)
        {
            //Attivazione dialoghi
            storyTellingUi.SetActive(true);
            // Attesa finchè i dialoghi non finiscono
            StoryTellingUI scoryTellingUiScript = storyTellingUi.GetComponent<StoryTellingUI>();
            yield return new WaitUntil(() => scoryTellingUiScript.hasFinished());
            storyTellingUi.SetActive(false);
        }
        if(finalCountdownUi != null)
        {
            finalCountdownUi.SetActive(true);
            CountdownUI countdownUiScript = finalCountdownUi.GetComponent<CountdownUI>();
            yield return new WaitUntil(() => countdownUiScript.hasFinished());
        }
        // Caricamento livello
        if(nextScene == Level.None)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {                
            StartMenu.LoadLevel(gameObject, GetIndexFromLevel(nextScene), 0);
        }
    }

    public int GetIndexFromLevel(Level l)
    {
        switch (l)
        {
            case Level.Bedroom:
                return 1;
            case Level.Lava:
                return 2;
            case Level.Castle:
                return 3;
            default:
                break;
        }
        return -1;
    }
}
