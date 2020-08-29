using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    /* Le scene sono:
           0: menù inizio gioco
           1: introduzione liv camera da letto
           2: LIVELLO 1 - CAMERA DA LETTO
           3: introduzione liv lava
           4: LIVELLO 2 - THE FLOOR IS LAVA
           5: introduzione liv castello
           6: LIVELLO 3 - CASTELLO e lotta boss finale
           7: dialoghi finali
     */

    public void Quit()
    {
        Application.Quit();
    }

    public void Start()
    {
        GameObject manager = GameObject.Find("AudioManager");
        if(manager != null)
        {
            manager.GetComponent<AudioManager>().StopMusicMenu();
        }
        
    }

    public void FirstLevel(int checkpoint)
    {
        GameSettings.SetLevel(1);
        if (checkpoint < 0 || checkpoint > 3)
            checkpoint = 0;
        GameSettings.SetCheckpoint(checkpoint);
        LoadScene(2);
    }
    public void SecondLevel(int checkpoint)
    {
        GameSettings.SetLevel(2);
        if (checkpoint < 0 || checkpoint > 3)
            checkpoint = 0;
        GameSettings.SetCheckpoint(checkpoint);
        LoadScene(4);
    }
    public void ThirdLevel(int checkpoint)
    {
        GameSettings.SetLevel(3);
        if (checkpoint < 0 || checkpoint > 3)
            checkpoint = 0;
        GameSettings.SetCheckpoint(checkpoint);
        LoadScene(6);
    }

    // Metodo utile se da un gameobject qualunque si vuole passare ad un'altra scena
    public static void LoadLevel(GameObject g, int level, int checkpoint)
    {
        StartMenu s = g.AddComponent<StartMenu>();
        switch (level)
        {
            case 1: // Bedroom
                s.FirstLevel(checkpoint);
                break;
            case 2: // Lava
                s.SecondLevel(checkpoint);
                break;
            case 3: // Castle
                s.ThirdLevel(checkpoint);
                break;
        }
    }

    public void LoadDialogue(int level) {
        GameSettings.SetCheckpoint(0);
        if (level == 1)
            LoadScene(1); // intro liv1
        else if (level == 2)
            LoadScene(3); // intro liv2
        else if (level == 3)
            LoadScene(5); // intro liv3
    }

    private void LoadScene(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }
}
