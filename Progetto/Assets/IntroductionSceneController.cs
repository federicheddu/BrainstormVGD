using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntroductionSceneController : MonoBehaviour
{
    public enum FlowMode { oneByOne, atTheSameTime };
    public enum PlayerPosition { farFromTheTable, inFrontOfTheTable };
    public enum LevelNumber { One = 1, Two = 2, Three = 3 };

    public string playerTag = "Player";
    public GameObject gameLight;
    public GameObject[] brainstormObjects;


    [Header("Uis")]
    public GameObject storyTellingUi;
    public GameObject finalCountdownUi;

    [Header("Flow Settings")]
    public PlayerPosition playerInitialPosition;
    public FlowMode flowMode;

    private GameObject player;
    private StoryTellingPlayerController playerStory;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerStory = player.GetComponent<StoryTellingPlayerController>();

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
        if (playerInitialPosition.Equals(PlayerPosition.farFromTheTable))
        {
            yield return new WaitUntil(() => !playerStory.isInFrontOfTheTable());
        }
        gameLight.SetActive(true);
        // Attivazione brainstorm per ogni studente
        foreach (GameObject g in brainstormObjects)
        {
            yield return new WaitForSeconds(1f);
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
        //Attivazione dialoghi
        storyTellingUi.SetActive(true);
        // Attesa finchè i dialoghi non finiscono
        StoryTellingUI scoryTellingUiScript = storyTellingUi.GetComponent<StoryTellingUI>();
        yield return new WaitUntil(() => scoryTellingUiScript.hasFinished());
        storyTellingUi.SetActive(false);
        finalCountdownUi.SetActive(true);
        CountdownUI countdownUiScript = finalCountdownUi.GetComponent<CountdownUI>();
        yield return new WaitUntil(() => countdownUiScript.hasFinished());
        // Caricamento primo livello
        StartMenu.LoadLevel(gameObject, 1, 1);
    }
}
