using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
 
    public KeyCode keyToPressToPause = KeyCode.Escape;
    public GameObject pauseMenuUI;
    public GameObject[] UisToDisableOnPause;

    private PlayerMovement script;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            script = player.GetComponent<PlayerMovement>();
        }
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKeyDown(keyToPressToPause))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        for(int i=0; i<UisToDisableOnPause.Length; i++)
        {
            UisToDisableOnPause[i].SetActive(true);
        }

        if(script != null)
        {
            script.enabled = true; //Per sbloccare la camera e i movimenti
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < UisToDisableOnPause.Length; i++)
        {
            UisToDisableOnPause[i].SetActive(false);
        }
        if (script != null)
        {
            script.enabled = false; //Per bloccare la camera
        }

        Time.timeScale = 0f; 
        GameIsPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
