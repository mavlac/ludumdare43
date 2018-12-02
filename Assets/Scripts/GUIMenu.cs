using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour {
	
	[Header("Child Elements")]
	public string topScorePrefix;
	public Text lblTopScoreText;
	public GameObject buttonPlay;
	
	void Start() {
		lblTopScoreText.text = topScorePrefix + Game.topScore.ToString();
	}
}
