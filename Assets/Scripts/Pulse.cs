using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	const float startRatio = 1.25f;
	const float speed = 2f;

	Vector3 initialScale;
	

	void Awake ()
	{
		initialScale = transform.localScale;
	}



	public void Play() {
		StartCoroutine(PulseCoroutine());
	}
	IEnumerator PulseCoroutine() {
		
		transform.localScale = initialScale * startRatio;
		
		for(float t = 0; t < 1; t += Time.unscaledDeltaTime * speed)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, initialScale, t);
			yield return null;
		}
		
		transform.localScale = initialScale;
	}
	
	
	public void ResetToInitialAndEnd()
	{
		StopAllCoroutines();
		transform.localScale = initialScale;
	}
}