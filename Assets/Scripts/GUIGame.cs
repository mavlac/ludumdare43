using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIGame : MonoBehaviour {
	
	public GameManager gameManager;
	
	[Header("Child Elements")]
	public GameObject lblGameOver;
	public Text lblScoreText;
	
	public GameObject buttonPlay;
	
	
	bool gameOverState = false;
	
	
	void Awake () {
		lblGameOver.SetActive(false);
		buttonPlay.GetComponent<Button>().interactable = false;
		buttonPlay.SetActive(false);
	}
	
	void Update() {
		
		if (Input.GetButtonDown("Action") && buttonPlay.GetComponent<Button>().interactable) {
			
			gameManager.Replay();
		}
	}
	
	
	
	public void UpdateGUIElements() {
		lblScoreText.text = Game.score.ToString();
	}
	
	
	
	public void SetGameOverState() {
		gameOverState = true;
		StartCoroutine(DelayedReplayButtonShowCoroutine());
	}
	IEnumerator DelayedReplayButtonShowCoroutine()
	{
		yield return new WaitForSecondsRealtime(2);
		ActivateReplayButton();
	}
	
	public void ActivateReplayButton() {
		buttonPlay.SetActive(true);
		buttonPlay.GetComponent<Button>().interactable = true;
	}
}
