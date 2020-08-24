using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		if ( SceneManager.GetActiveScene().name == "ProvaLivelloScrivania")
		{
			Play("MusicLiv1");

		} else if (SceneManager.GetActiveScene().name == "New Scene")
		{
			Play("MusicLiv2");
		}
		else if (SceneManager.GetActiveScene().name == "SampleScene" ||SceneManager.GetActiveScene().name == "Livello Castello" 
			|| SceneManager.GetActiveScene().name == "DemoScene")
		{
			Play("MusicLiv3");
		} else Debug.Log("Hai sbagliato a scrivere il livello");
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		//codice per eseguire il suono un p? diverso tramite la variazione impostata
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StopPlaying(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}

	public void BossMusic()
    {
		StopPlaying("MusicLiv3");
		Play("MusicBoss");
    }

	public void IncreaseVolume()
	{
		AudioSource[] sources = GetComponents<AudioSource>();
		foreach (AudioSource audioSource in sources)
		{
			audioSource.volume += 0.005f;
		}
	}

	public void DecreaseVolume()
	{
		AudioSource[] sources = GetComponents<AudioSource>();
		foreach (AudioSource audioSource in sources)
		{
			audioSource.volume -= 0.005f;
		}
	}

}
