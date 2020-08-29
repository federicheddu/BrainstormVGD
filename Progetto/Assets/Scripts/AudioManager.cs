using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public bool ignoreShoot = false;

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

		if ( SceneManager.GetActiveScene().name == "LivelloCamera")
		{
			Play("MusicLiv1");

		} else if (SceneManager.GetActiveScene().name == "New Scene")
		{
			Play("MusicLiv2");
		}
		else if (SceneManager.GetActiveScene().name == "SampleScene" ||SceneManager.GetActiveScene().name == "Livello Castello" 
			|| SceneManager.GetActiveScene().name == "Demo Scene")
		{
			Play("MusicLiv3");
		} else Debug.Log("Hai sbagliato a scrivere il livello");
	}

	public void Play(string sound)
	{
		if (ignoreShoot && (sound.Equals("GunShoot") || sound.Equals("GunReload"))) // suoni ignorati quando sono attive le ui
			return;

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

	public void Victory()
	{
		StopPlaying("MusicBoss");
		Play("Victory");
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

	public void ToggleIgnoreShoot()
    {
		ignoreShoot = !ignoreShoot;
    }

	public void SetIgnoreShoot(bool v)
    {
		ignoreShoot = v;
    }

	public void StopMusicMenu()
    {
		StopPlaying("MusicLiv1");
		StopPlaying("MusicLiv2");
		StopPlaying("MusicLiv3");
		StopPlaying("MusicBoss");
	}
}
