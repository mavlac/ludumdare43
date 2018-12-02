using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemIndicator : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	
	
	public void ItemPickedUp() {
		spriteRenderer.enabled = true;
	}
	
	public void ItemDropped() {
		spriteRenderer.enabled = false;
	}
	public void ItemSetToNotPresent() {
		spriteRenderer.enabled = false;
	}
}
