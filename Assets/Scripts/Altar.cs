using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour {

	public GameManager gameManager;
	
	[Space]
	public Color32 itemPresentColor;
	
	[Header("Sacrifice")]
	public float sacrificeTimeLen;
	public GameObject sacrificeFx;
	
	
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
		
		if (gameManager.playerController.carryItem && other.gameObject.CompareTag("Player"))
		{
			PlayerWithItemCame();
		}
	}
	void OnTriggerExit(Collider other) {
		
		if (gameManager.playerController.carryItem && other.gameObject.CompareTag("Player"))
		{
			PlayerWithItemLeft();
		}
	}
	
	
	
	public void PlayerWithItemCame() {
		StartCoroutine(LerpToColorCoroutine(itemPresentColor));
		
		markerPulse.Play();
	}
	public void PlayerWithItemLeft() {
		StopAllCoroutines();
		markerSprite.color = initialColor;
		
		markerPulse.ResetToInitialAndEnd();
	}
	
	public void ItemPlaced() {
		markerSprite.color = itemPresentColor;
		
		markerPulse.ResetToInitialAndEnd();
	}
	public void ItemRemoved() {
		StopAllCoroutines();
		markerSprite.color = initialColor;
	}
	
	
	
	
	IEnumerator LerpToColorCoroutine(Color32 c) {
		
		for (float t = 0; t < 1; t+=Time.unscaledDeltaTime * lerpColorSpeed)
		{
			markerSprite.color = Color32.Lerp(initialColor, c, t);
			yield return null;
		}
	}
	
	
	
	
	
	
	
	public void BeginSacrifice(ItemController droppedItemController)
	{
		StartCoroutine(SacrificeProgressCoroutine(droppedItemController));
	}
	IEnumerator SacrificeProgressCoroutine(ItemController sacrificedItemController) {
		
		yield return new WaitForSecondsRealtime(sacrificeTimeLen);
		
		gameManager.Sacrifice();
		
		// spawn fx prefab
		Destroy(
			Instantiate(
				sacrificeFx,
				transform.position,
				Quaternion.identity,
				transform) as GameObject, 5f);
		
		SacrificeFinished(sacrificedItemController);
	}
	void SacrificeFinished(ItemController sacrificedItemController)
	{
		sacrificedItemController.Dispose();
		ItemRemoved();
	}
}
