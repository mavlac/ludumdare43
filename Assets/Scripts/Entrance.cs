using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {
	
	public GameManager gameManager;
	
	[Space]
	public Color32 enemyCameColor;
	public Color32 itemPresentColor;
	
	
	bool guardActive = false;
	ItemController guardingItem;
	
	
	SpriteRenderer markerSprite;
	Pulse markerPulse;
	
	const float lerpColorSpeed = 5f;
	Color32 initialColor;
	
	
	
	void Awake() {
		markerSprite = GetComponentInChildren<SpriteRenderer>();
		markerPulse = markerSprite.gameObject.GetComponent<Pulse>();
		initialColor = markerSprite.color;
	}
	
	void OnTriggerEnter(Collider other) {
		
		if (gameManager.playerController.carryItem && other.gameObject.CompareTag("Player") && !guardActive)
		{
			PlayerWithItemCame();
		}
	}
	void OnTriggerExit(Collider other) {
		
		if (other.gameObject.CompareTag("Player") && !guardActive)
		{
			PlayerWithItemLeft();
		}
	}
	
	
	
	public void PlayerWithItemCame() {
		StopAllCoroutines();
		StartCoroutine(LerpToColorCoroutine(itemPresentColor));
		
		markerPulse.Play();
	}
	public void PlayerWithItemLeft() {
		StopAllCoroutines();
		markerSprite.color = initialColor;
		
		markerPulse.ResetToInitialAndEnd();
	}
	
	public void ItemPlaced() {
		StopAllCoroutines();
		StartCoroutine(LerpToColorCoroutine(itemPresentColor));
		
		markerPulse.ResetToInitialAndEnd();
	}
	public void ItemRemoved() {
		StopAllCoroutines();
		markerSprite.color = initialColor;
	}
	
	public void EnemyCameThrough() {
		StopAllCoroutines();
		StartCoroutine(LerpToColorCoroutine(enemyCameColor));
	}
	
	
	
	IEnumerator LerpToColorCoroutine(Color32 c) {
		
		for (float t = 0; t < 1; t+=Time.unscaledDeltaTime * lerpColorSpeed)
		{
			markerSprite.color = Color32.Lerp(initialColor, c, t);
			yield return null;
		}
	}
	
	
	
	
	
	
	
	public void ActivateGuard(ItemController droppedItemController)
	{
		guardActive = true;
		guardingItem = droppedItemController;
	}
	public ItemController DeactivateGuard() {
		guardActive = false;
		return guardingItem;
	}
	public void SetGuardFree()
	{
		guardingItem.ItemFreedFromGuardingEntrance();
		
		guardActive = false;
		guardingItem = null;
		
		ItemRemoved();
	}
	public bool IsGuarded()
	{
		return guardActive;
	}
}
