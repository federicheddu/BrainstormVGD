using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button levelsButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Levels()
    {
        levelsButton.gameObject.SetActive(false);
    }

    public void Back(Button b)
    {
        levelsButton.gameObject.SetActive(true);

    }

    public void FirstLevel(int checkpoint)
    {
        switch (checkpoint)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(1);
                break;
            case 3:
                SceneManager.LoadScene(1);
                break;
        }

    }
    public void SecondLevel(int checkpoint)
    {
        switch (checkpoint)
        {
            case 0:
                SceneManager.LoadScene(2);
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(2);
                break;
            case 3:
                SceneManager.LoadScene(2);
                break;
        }

    }
    public void ThirdLevel(int checkpoint)
    {
        switch (checkpoint)
        {
            case 0:
                SceneManager.LoadScene(3);
                break;
            case 1:
                SceneManager.LoadScene(3);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 3:
                SceneManager.LoadScene(3);
                break;
        }

    }
}
