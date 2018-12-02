using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	
	public AppManager appManager;
	
	[Space]
	public GUIMenu guiMenu;
	
	void Start () {
		
		appManager.screenFader.FadeScreenIn();
	}
	
	void Update () {
		// editor debug ingame input
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.R)) appManager.RestartCurrentScene();
#endif
		
		// space = action
		if (Input.GetButtonDown("Action")) {
			ButtonPlayDown();
		}
		// escape = cancel
		if (Input.GetButtonDown("Cancel")) {
			appManager.Exit();
		}
	}
	
	
	
	
	public void ButtonPlayDown ()
	{
		guiMenu.buttonPlay.GetComponent<Button>().interactable = false;
		
		appManager.screenFader.FadeScreenOut();
		StartCoroutine(GameSceneLoadTimerCoroutine());
		
		appManager.soundManager.Play(SoundManager.soundId.guiClick);
	}
	IEnumerator GameSceneLoadTimerCoroutine()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		appManager.LoadGameScene();
	}
}
