using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedUIController : MonoBehaviour
{
    public GameObject[] uisToDisableOnStart;
    public Canvas toDisableOnStart;
    public GameObject countdown;
    // Start is called before the first frame update
    void Start()
    {
        // Blocco del gioco
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

        foreach (GameObject ui in uisToDisableOnStart)
        {
            if (ui != null)
                ui.SetActive(false);
        }
        toDisableOnStart.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AudioManager.instance.SetIgnoreShoot(true);
    }

    // Update is called once per frame
    public void Menu()
    {
        AudioManager.instance.SetIgnoreShoot(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        countdown.SetActive(true);
        AudioManager.instance.SetIgnoreShoot(false);
        StartCoroutine(NextLevelCoroutine());
    }

    IEnumerator NextLevelCoroutine()
    {
        CountdownUI c = countdown.GetComponent<CountdownUI>();
        yield return new WaitUntil(() => c.hasFinished());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





}
