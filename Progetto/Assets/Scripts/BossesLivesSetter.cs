using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossesLivesSetter : MonoBehaviour
{
    public Target[] target;
    public GameObject[] toActivateOnWin;

    private Slider slider;
    private Text lifeText;
    private float bossesMaxLife, life;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        lifeText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        life = 0f;
        bossesMaxLife = 0f;

        Target[] activeBosses = GetActiveBosses();
        Debug.Log("Active bosses:" + activeBosses);

        foreach (Target t in activeBosses)
        {
            bossesMaxLife += t.maxHealt;
            life += t.health;
        }

        slider.value = life;
        slider.maxValue = bossesMaxLife;

        lifeText.text = slider.value.ToString()+"/"+slider.maxValue.ToString();
        if (activeBosses.Length == target.Length && life == 0f)
        {
            foreach (GameObject g in toActivateOnWin)
            {
                if (g != null)
                    g.SetActive(true);
            }
            AudioManager am = AudioManager.instance;
            if (am != null)
            {
                am.Victory();
            }
            StartCoroutine(LastLevel());
        }
    }

    private Target[] GetActiveBosses()
    {
        List<Target> activeBosses = new List<Target>();
        foreach (Target t in target)
        {
            if (t != null && t.gameObject.activeInHierarchy)
                activeBosses.Add(t);
        }
        Debug.Log("Active bosses:" + activeBosses);
        return activeBosses.ToArray();
    }

    public void SetVolume(float volume)
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource audioSource in sources)
        {
            audioSource.volume = volume;
        }
    }


    IEnumerator LastLevel()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}
