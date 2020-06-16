using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject player;
    public KeyCode keyToPressToPause = KeyCode.Escape;
    public GameObject pauseMenuUI;

    private PlayerMovement script;

    // Start is called before the first frame update
    void Start()
    {
        script = player.GetComponent<PlayerMovement>();
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

        script.enabled = true; //Per sbloccare la camera e i movimenti
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        script.enabled = false; //Per bloccare la camera

        Time.timeScale = 0f; 
        GameIsPaused = true;
    }

    public void Quit()
    {
        Debug.Log("Replace with code to Quit Game");
    }

    public void Levels()
    {
        Debug.Log("Replace with code to show Game Levels");
    }

}
