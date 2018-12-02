using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	public SpriteRenderer sprite;
	
	[Space]
	public float moveSpeed;
	const float moveSpeedVariability = 0f;
	
	[Space]
	public GameObject enemyKilledFx; 
	
	[HideInInspector]
	public GameManager gameManager;
	
	bool deactivated = false;
	
	bool enemyMoving = false;
	float speed;
	
	Rigidbody rb;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		
		speed = moveSpeed + Random.Range(-moveSpeedVariability, moveSpeedVariability);
	}
	
	void FixedUpdate () {
		
		if (enemyMoving)
		{
			Vector3 desiredPosition = Vector3.zero;
			Vector3 moveVector = desiredPosition - rb.position;
			moveVector = Vector3.Normalize(moveVector);
			
			rb.MovePosition(rb.position + moveVector * speed * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.CompareTag("Entrance"))
		{
			Entrance enteredEntrance = other.gameObject.GetComponent<Entrance>();
			if (enteredEntrance.IsGuarded())
			{
				// kill current enemy
				KillInstance();
				// free the guarding item
				enteredEntrance.SetGuardFree();
				
				gameManager.EnemyKilledAtEntrance();
				
			} else if (!deactivated) {
				// entrance not guarded
				enteredEntrance.EnemyCameThrough();
				HighlightSpriteInSortingLayer();
				gameManager.EnemyCameThroughEntrance(this);
			}
		}
	}



	public void InitEnemy(GameManager gameManagerReference, float initialEnemyMoveDelay) {
		gameManager = gameManagerReference;
		StartCoroutine(InitialEnemyDelayCoroutine(initialEnemyMoveDelay));
	}
	IEnumerator InitialEnemyDelayCoroutine(float delay)
	{
		yield return new WaitForSecondsRealtime(delay);
		enemyMoving = true;
		yield return null;
	}
	
	
	public void KillInstance() {
		
		if (deactivated) return;
		
		deactivated = true;
		
		// spawn fx prefab
		Destroy(
			Instantiate(
				enemyKilledFx,
				transform.position,
				Quaternion.identity,
				transform.parent) as GameObject, 3f);
		
		// get rid of current enemy instance
		Destroy(gameObject);
	}
	
	
	
	
	void HighlightSpriteInSortingLayer() {
		sprite.sortingOrder = 1;
	}
}
