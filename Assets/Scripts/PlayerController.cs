using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public GameManager gameManager;
	
	[Header("Sub Elements")]
	public PlayerItemIndicator playerItemIndicator;
	
	[Space]
	public float moveSpeed;
	
	[HideInInspector]
	public bool carryItem = false;
	ItemController carriedItemController;
	
	Entrance enteredEntrance;
	Altar enteredAltar;
	
	
	Rigidbody rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody>();
	}
	
	void Start () {
		playerItemIndicator.ItemSetToNotPresent();
	}
	
	void Update() {
		
		// item drop
		if (Input.GetButtonDown("Action")) {
			
			if (carryItem && enteredEntrance && !enteredEntrance.IsGuarded()) {
				// drop item on entrance
				enteredEntrance.ItemPlaced();
				enteredEntrance.ActivateGuard(carriedItemController);
				
				playerItemIndicator.ItemDropped();
				carriedItemController.ItemDrop(enteredEntrance.gameObject.transform.position);
				carryItem = false;
				
				gameManager.appManager.soundManager.Play(SoundManager.soundId.itemPlaced);
			}
			else if (!carryItem && enteredEntrance && enteredEntrance.IsGuarded()) {
				// pickup item from guarded entrance
				enteredEntrance.ItemRemoved();
				carriedItemController = enteredEntrance.DeactivateGuard();
				
				carriedItemController.ItemPickedUp();
			
				carryItem = true;
				playerItemIndicator.ItemPickedUp();
				
				gameManager.PlayerItemPickup();
			}
			else if (carryItem && enteredAltar) {
				// drop item on altar
				enteredAltar.ItemPlaced();
				enteredAltar.BeginSacrifice(carriedItemController);
				
				playerItemIndicator.ItemDropped();
				carriedItemController.ItemDrop(enteredAltar.gameObject.transform.position);
				carryItem = false;
				
				gameManager.appManager.soundManager.Play(SoundManager.soundId.itemPlaced);
			}
			else
			{
				// action denied
				gameManager.appManager.soundManager.Play(SoundManager.soundId.actionDenied);
			}
		}
	}
	
	void FixedUpdate () {
		
		
		if (gameManager.playerAlive) {
			float x = Input.GetAxis("Horizontal") * moveSpeed;
			float z = Input.GetAxis("Vertical") * moveSpeed;
			
			Vector3 moveVector = new Vector3(x, 0f, z);
			
			rb.MovePosition(rb.position + moveVector * Time.unscaledDeltaTime);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		
		// autopickup item if not carried
		if (!carryItem && other.gameObject.CompareTag("Item"))
		{
			carriedItemController = other.gameObject.GetComponent<ItemController>();
			carriedItemController.ItemPickedUp();
			
			carryItem = true;
			playerItemIndicator.ItemPickedUp();
			
			gameManager.PlayerItemPickup();
		}
		
		// entering the Entrance drop area
		if (other.gameObject.CompareTag("Entrance"))
		{
			enteredEntrance = other.gameObject.GetComponent<Entrance>();
		}
		// entering the Altar drop area
		if (other.gameObject.CompareTag("Altar"))
		{
			enteredAltar = other.gameObject.GetComponent<Altar>();
		}
	}

	void OnTriggerExit(Collider other) {
		
		// leaving the Entrance drop area
		if (other.gameObject.CompareTag("Entrance"))
		{
			enteredEntrance = null;
		}
		// leaving the Altar drop area
		if (other.gameObject.CompareTag("Altar"))
		{
			enteredAltar = null;
		}
	}
}
