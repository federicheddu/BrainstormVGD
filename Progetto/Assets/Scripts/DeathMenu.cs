﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public GameObject[] uisToDisableOnStart;
    public Canvas toDisableOnStart;
    // Start is called before the first frame update
    void Start()
    {
        // Blocco del gioco
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

        foreach(GameObject ui in uisToDisableOnStart)
        {
            ui.SetActive(false);
        }
        toDisableOnStart.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Update is called once per frame
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    private void RestartLevel(int checkpoint)
    {
        StartMenu.LoadLevel(GameObject.FindGameObjectWithTag("Player").gameObject, GameSettings.GetLevel(), checkpoint);
    }

    public void RestartLevel()
    {
        RestartLevel(0);
    }

    public void RestartLevelFromLastCheckpoint()
    {
        RestartLevel(GameSettings.GetCheckpoint());
    }




}
