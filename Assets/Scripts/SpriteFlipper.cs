using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour {
	
	public float frequency;
	
	SpriteRenderer spriteRenderer;
	
	void Start() {
		
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(FlipCoroutine());
	}
	
	IEnumerator FlipCoroutine()
	{
		while (true) {
			
			spriteRenderer.flipX = true;
			yield return new WaitForSecondsRealtime(frequency);
			spriteRenderer.flipX = false;
			yield return new WaitForSecondsRealtime(frequency);
		}
	}
}
