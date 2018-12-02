using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {
	
	public float lerpSpeed;

	public Color32 defaultColor;
	
	const float transparent = 0f;
	const float opaque = 1f;


	Color c;

	Image image;


	void Awake ()
	{
		image = GetComponent<Image>();
		image.enabled = false;
	}



	public void FadeScreenOut() {
		FadeScreenOut(defaultColor);
	}
	public void FadeScreenOut(Color fadeTo)
	{
		Reset();
		image.color = fadeTo;
		image.enabled = true;
		
		StartCoroutine(
			FadeScreenOutCoroutine(lerpSpeed));
	}
	IEnumerator FadeScreenOutCoroutine(float speed)
	{
		for (float t = 0f; t <= 1f; t+= Time.unscaledDeltaTime * speed)
		{
			SetAlpha(t);
			yield return null;
		}
		
		SetAlpha(opaque);
		
		FadeScreenOutCallback();
	}
	
	public void FadeScreenOutCallback()
	{
		// dummy
	}
	
	
	
	public void FadeScreenIn() {
		FadeScreenIn(defaultColor);
	}
	public void FadeScreenIn(Color fadeFrom)
	{
		Reset();
		SetAlpha(opaque);
		image.color = fadeFrom;
		image.enabled = true;
		
		StartCoroutine(FadeScreenInCoroutine());
	}
	IEnumerator FadeScreenInCoroutine()
	{
		for (float t = 0f; t <= 1f; t+= Time.unscaledDeltaTime * lerpSpeed)
		{
			SetAlpha(1 - t);
			yield return null;
		}
		
		SetAlpha(transparent);
		image.enabled = false;
	}
	
	
	
	
	private void SetAlpha(float alpha)
	{
		c = image.color;
		c.a = Mathf.Clamp(alpha, 0, 1);
		image.color = c;
	}
	
	public void Reset()
	{
		StopAllCoroutines();
		SetAlpha(transparent);
		image.enabled = false;
	}
}