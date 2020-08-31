using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastDialogueController : MonoBehaviour
{
    public GameObject[] dialogues;
    public GameObject button;
    public Text talkerText;

    public Sprite[] sprites;

    private Image image;


    private Coroutine c;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        image = transform.parent.gameObject.GetComponentInChildren<Image>();
        c = StartCoroutine(Flow());
    }


    public IEnumerator Flow()
    {
        int i;
        for (i = 0; i < dialogues.Length; i++)
        {
            if(sprites.Length < i)
            {
                image.sprite = sprites[i];
            }
            dialogues[i].SetActive(true);
            yield return new WaitForSeconds(2f);
            dialogues[i].SetActive(false);
        }
        dialogues[i - 1].SetActive(true);
        yield return new WaitForSeconds(4f);
        button.SetActive(false);
        talkerText.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
