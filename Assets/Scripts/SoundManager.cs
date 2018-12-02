﻿using UnityEngine;

[System.Serializable]
public class soundGroupGame
{
	public AudioClip actionDenied;
	public AudioClip itemBirth;
	public AudioClip enemyBirth;
	public AudioClip itemPickup;
	public AudioClip itemPlaced;
	public AudioClip sacrifice;
	
	public AudioClip point;
	[HideInInspector] public float pointMinDelay = 0.1f;
	[HideInInspector] public float pointLastPlayed;
	
	public AudioClip gameOver;
}
[System.Serializable]
public class soundGroupSystem
{
	public AudioClip guiClick;
}

public sealed class SoundManager : MonoBehaviour
{

	public static bool soundState;
	public static bool musicState;

	public enum soundId : int
	{
		actionDenied,
		itemBirth,
		enemyBirth,
		itemPickup,
		itemPlaced,
		sacrifice,
		point,
		gameOver,

		guiClick,
	};

	// sound file references
	[Header("Assets")]
	public soundGroupGame gameSounds;
	public soundGroupSystem systemSounds;

	// sound source reference
	[Header("Subscripts")]
	public MusicManager musicManager;


	// object cache
	AudioSource audioSrcGameSound; // global audio source


	void Awake()
	{
		audioSrcGameSound = gameObject.GetComponent<AudioSource>();
		
		SetSoundState((PlayerPrefs.GetInt("pref_sound", 1) == 1) ? true : false);
	}



	public void Play(soundId id, float volume = 1)
	{
		if (!soundState) return;

		switch (id)
		{
			// game
			case soundId.actionDenied:
				audioSrcGameSound.PlayOneShot(gameSounds.actionDenied, volume);
				break;
			case soundId.itemBirth:
				audioSrcGameSound.PlayOneShot(gameSounds.itemBirth, volume);
				break;
			case soundId.enemyBirth:
				audioSrcGameSound.PlayOneShot(gameSounds.enemyBirth, volume);
				break;
			case soundId.itemPickup:
				audioSrcGameSound.PlayOneShot(gameSounds.itemPickup, volume);
				break;
			case soundId.itemPlaced:
				audioSrcGameSound.PlayOneShot(gameSounds.itemPlaced, volume);
				break;
			case soundId.sacrifice:
				audioSrcGameSound.PlayOneShot(gameSounds.sacrifice, volume);
				break;
			case soundId.point:
				// limited frequency playback
				if (gameSounds.pointLastPlayed + gameSounds.pointMinDelay > Time.realtimeSinceStartup)
					break;
				audioSrcGameSound.PlayOneShot(gameSounds.point, volume);
				gameSounds.pointLastPlayed = Time.realtimeSinceStartup;
				break;
			case soundId.gameOver:
				audioSrcGameSound.PlayOneShot(gameSounds.gameOver, volume);
				break;
			
			// system
			case soundId.guiClick:
				audioSrcGameSound.PlayOneShot(systemSounds.guiClick, volume);
				break;
		}
	}





	// global sound settings with write to prefs
	public void SetSoundState(bool soundSetState)
	{
		soundState = soundSetState;
		PlayerPrefs.SetInt("pref_sound", soundSetState ? 1 : 0);
	}



	public void SetMusicState(bool musicSetState)
	{
		musicState = musicSetState;
		PlayerPrefs.SetInt("pref_ambience", musicSetState ? 1 : 0);
	}
	public void SetAmbienceTrack(MusicManager.musicId trackId)
	{
		musicManager.LoadMusicTrack(trackId);
	}
	public void PauseMusic()
	{
		musicManager.Pause();
	}
	public void PlayMusic()
	{
		if (!musicState) return;

		musicManager.Play();
	}
	public void PauseMusicWithFade()
	{
		musicManager.FadeOut();
	}
}