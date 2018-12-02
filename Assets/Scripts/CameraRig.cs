using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {
	
	public float speed;
	public float dist;
	public int iterations;
	
	
	public void Shake()
	{
		StartCoroutine(ShakeCoroutine());
	}
	IEnumerator ShakeCoroutine() {
		
		for (int i = 1; i <= iterations; i++) {
			
			Vector3 shakePos =
				new Vector3(
					Random.Range(0, dist / i), Random.Range(0, dist / i), Random.Range(0, dist / i));
			
			for (float t = 0; t < 1; t += Time.unscaledDeltaTime * speed) {
				transform.localPosition = Vector3.Lerp(shakePos, Vector3.zero, t);
				yield return null;
			}
		}
		
		transform.localPosition = Vector3.zero;
		
		yield return null;
	}
}
