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


    public void FirstLevel(int checkpoint)
    {
        GameSettings.SetLevel(1);
        switch (checkpoint)
        {
            case 0: // Caso in cui venga premuto play e nessun checkpoint
                GameSettings.SetCheckpoint(1);
                SceneManager.LoadScene(1);
                break;
            case 1:
                GameSettings.SetCheckpoint(1);
                SceneManager.LoadScene(2);
                break;
            case 2:
                GameSettings.SetCheckpoint(2);
                SceneManager.LoadScene(2);
                break;
            case 3:
                GameSettings.SetCheckpoint(3);
                SceneManager.LoadScene(2);
                break;
        }

    }
    public void SecondLevel(int checkpoint)
    {
        GameSettings.SetLevel(2);
        switch (checkpoint)
        {
            case 0: // Caso in cui venga premuto play e nessun checkpoint
                GameSettings.SetCheckpoint(0);
                SceneManager.LoadScene(3); // Introduzione secondo livello
                break;
            case 1:
                GameSettings.SetCheckpoint(1); // Il livello parte dall'inizio senza introduzione
                SceneManager.LoadScene(4);
                break;
            case 2:
                GameSettings.SetCheckpoint(2); // Il livello parte dal primo checkpoint
                SceneManager.LoadScene(4);
                break;
            case 3:
                GameSettings.SetCheckpoint(3); // Il livello parte dal secondo checkpoint
                SceneManager.LoadScene(4);
                break;
        }

    }
    public void ThirdLevel(int checkpoint)
    {
        GameSettings.SetLevel(3);
        switch (checkpoint)
        {
            case 0: // Caso in cui venga premuto play e nessun checkpoint
                GameSettings.SetCheckpoint(0);
                SceneManager.LoadScene(5);
                break;
            case 1:
                GameSettings.SetCheckpoint(1);
                SceneManager.LoadScene(6);
                break;
            case 2:
                GameSettings.SetCheckpoint(2);
                SceneManager.LoadScene(6);
                break;
            case 3: // Lotta boss finale
                GameSettings.SetCheckpoint(3);
                SceneManager.LoadScene(6);
                break;
        }
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
}
