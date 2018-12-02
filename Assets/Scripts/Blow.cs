using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blow : MonoBehaviour {

	public float speed;


	Vector3 initialScale;
	
	void Awake () {
		initialScale = transform.localScale;
	}
	
	void Start () {
		StartCoroutine(BlowCoroutine());
	}
	
	IEnumerator BlowCoroutine()
	{
		for (float t = 0; t < 1; t += Time.unscaledDeltaTime * speed)
		{
			transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, t);
			yield return null;
		}
		
		transform.localScale = initialScale;
	}
	
	
	public void Reset() {
		transform.localScale = initialScale;
	}
	
}
