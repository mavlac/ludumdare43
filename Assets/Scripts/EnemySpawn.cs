using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	
	public GameManager gameManager;
	
	[Space]
	
	public GameObject enemyPrefab;
	public GameObject enemyBirthFx;
	
	public EnemyBirthplace[] enemyBirthplaces;
	public float initialEnemyDelay;

	int lastBirtplaceIndex = -1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	
	
	public void Spawn()
	{
		// pick spawn birthlace
		int birthplaceIndex;
		do {
			birthplaceIndex = Random.Range(0, enemyBirthplaces.Length);
		} while (birthplaceIndex == lastBirtplaceIndex);
		lastBirtplaceIndex = birthplaceIndex;
		
		Vector3 birthplacePosition = enemyBirthplaces[birthplaceIndex].transform.position;
		
		// spawn fx prefab
		Destroy(
			Instantiate(
				enemyBirthFx,
				birthplacePosition,
				Quaternion.identity,
				enemyBirthplaces[birthplaceIndex].transform) as GameObject, 3f);
		
		// enemy prefab
		GameObject spawnedEnemy =
			Instantiate(
				enemyPrefab,
				birthplacePosition,
				Quaternion.identity) as GameObject;
		
		EnemyController spawnedEnemyCtrl = spawnedEnemy.GetComponent<EnemyController>();
		spawnedEnemyCtrl.InitEnemy(gameManager, initialEnemyDelay);
	}
}
