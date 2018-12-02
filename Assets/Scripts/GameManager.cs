using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public AppManager appManager;
	
	[Space]
	public PlayerController playerController;
	
	[Space]
	public GUIGame guiGame;
	public CameraRig cameraRig;
	
	
	[Header("Sub Managers")]
	public EnemySpawn enemySpawn;
	
	public float initialEnemySpawnDelay;
	public float enemySpawnFrequency;
	public int occasionalDoubleSpawnScoreMilestone;
	public int occasionalDoubleSpawnProbability;
	
	[Header("Item Spawn")]
	public float newItemSpawnTimer;
	public GameObject itemPrefab;
	public GameObject itemBirthFx;
	public ItemSpawnTimer itemSpawnTimer;
	
	[Space]
	public Color32 playerDeadScreenFaderFlashColor;
	
	[HideInInspector]
	public bool playerAlive = true;
	
	
	// Use this for initialization
	void Start () {
		
		appManager.screenFader.FadeScreenIn();
		
		// game begins!

		appManager.soundManager.musicManager.LoadMusicTrack(MusicManager.musicId.game);
		appManager.soundManager.musicManager.Play();
		
		InitGame();
	}
	
	// Update is called once per frame
	void Update () {
		// editor debug ingame input
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.R)) appManager.RestartCurrentScene();
		if (Input.GetKeyDown(KeyCode.K)) PlayerDead();
#endif
		
		
		// escape = cancel
		// back to title
		if (Input.GetButtonDown("Cancel")) {
			appManager.LoadMenuScene();
		}

	}
	
	
	
	void InitGame() {
		
		Game.InitNewGameValues();
		
		StartCoroutine(InitialEnemySpawnDelay());
	}
	
	IEnumerator InitialEnemySpawnDelay() {
		yield return new WaitForSecondsRealtime(initialEnemySpawnDelay);
		StartCoroutine(EnemySpawnCoroutine());
		yield return null;
	}
	IEnumerator EnemySpawnCoroutine()
	{
		while (playerAlive) {
			
			enemySpawn.Spawn();
			// sometimes extra bad guy
			if (Game.score >= occasionalDoubleSpawnScoreMilestone &&
				Random.Range(0, occasionalDoubleSpawnProbability) == 0)
			{
				enemySpawn.Spawn();
			}
			
			appManager.soundManager.Play(SoundManager.soundId.enemyBirth);
			
			yield return new WaitForSeconds(enemySpawnFrequency);
		}
		
		yield return null;
	}
	
	
	
	public void EnemyKilledAtEntrance() {
		
		Game.IncrementScore();
		guiGame.UpdateGUIElements();
		
		appManager.soundManager.Play(SoundManager.soundId.point);
	}
	public void EnemyCameThroughEntrance(EnemyController enemyController) {
		
		PlayerDead();
	}
	
	
	
	
	void PlayerDead()
	{
		appManager.SlowDownTimescale();
		
		//cameraRig.Shake();
		appManager.screenFader.FadeScreenIn(playerDeadScreenFaderFlashColor);
		appManager.gameFader.LerpToSemiTransparent(true);
		guiGame.lblGameOver.SetActive(true);
		playerAlive = false;
		

		appManager.soundManager.Play(SoundManager.soundId.gameOver);
		
		appManager.soundManager.musicManager.LoadMusicTrack(MusicManager.musicId.gameOver);
		appManager.soundManager.musicManager.Play();
		
		guiGame.SetGameOverState();
	}
	
	public void PlayerItemPickup() {
		appManager.soundManager.Play(SoundManager.soundId.itemPickup);
	}
	
	
	
	
	
	public void Sacrifice() {
		
		appManager.soundManager.Play(SoundManager.soundId.sacrifice);
		cameraRig.Shake();
		appManager.screenFader.FadeScreenIn(Color.white);
		//appManager.TimescaleBoost(); buggy :(
		
		// destroy all enemy instances
		EnemyController[] enemies = FindObjectsOfType<EnemyController>();
		foreach(EnemyController enemy in enemies)
		{
			enemy.KillInstance();
			Game.IncrementScore();
		}
		guiGame.UpdateGUIElements();
		
		appManager.soundManager.Play(SoundManager.soundId.point);
		
		// timer for new item instantiation
		StartCoroutine(ItemSpawnTimerCoroutine());
	}
	IEnumerator ItemSpawnTimerCoroutine()
	{
		itemSpawnTimer.StartTimerVisual(newItemSpawnTimer);
		yield return new WaitForSeconds(newItemSpawnTimer);
		
		SpawnNewItem();
	}
	
	void SpawnNewItem() {
		
		if (playerAlive) {
			
			// spawn new item prefab
			GameObject spawnedItem =
				Instantiate(
					itemPrefab,
					Vector3.zero,
					Quaternion.identity) as GameObject;
			
			//ItemController spawnedItemCtrl = spawnedItem.GetComponent<ItemController>();
			
			
			// spawn fx prefab
			Destroy(
				Instantiate(
					itemBirthFx,
					Vector3.zero,
					Quaternion.identity, spawnedItem.transform) as GameObject, 3f);
			
			
			appManager.soundManager.Play(SoundManager.soundId.itemBirth);
		}
	}
	
	
	
	
	
	public void Replay()
	{
		appManager.RestartCurrentScene();
	}
}
