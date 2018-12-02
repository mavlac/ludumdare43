using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnTimer : MonoBehaviour {
	
	public Vector3 destinationScale;
	public Color32 destinationColor;
	
	
	Color32 initialColor;
	Vector3 initialScale;
	SpriteRenderer sprite;
	
	void Awake () {
		
		initialScale = transform.localScale;
		
		sprite = GetComponent<SpriteRenderer>();
		sprite.enabled = false;
		initialColor = sprite.color;
	}
	
	
	
	public void StartTimerVisual(float timerLen)
	{
		// visual on
		transform.localScale = initialScale;
		sprite.enabled = true;
		sprite.color = initialColor;
		
		StartCoroutine(TimerVisualCoroutine(timerLen));
	}
	IEnumerator TimerVisualCoroutine(float timerLen)
	{
		float timeBegin = Time.realtimeSinceStartup;
		float timeEnd = timeBegin + timerLen;
		float p = 0; // progress
		
		while (p < 1) {
			
			p = Mathf.InverseLerp(timeBegin, timeEnd, Time.realtimeSinceStartup);
			
			transform.localScale = Vector3.Lerp(initialScale, destinationScale, p);
			sprite.color = Color32.Lerp(initialColor, destinationColor, p);
			
			yield return null;
		}
		
		
		// visual off
		sprite.enabled = false;
	}
}
