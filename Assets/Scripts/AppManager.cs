using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AppManager : MonoBehaviour {

	public SoundManager soundManager;
	public GameManager gameManager;
	
	[Space]
	public GameFader gameFader;
	public ScreenFader screenFader;
	

	float defaultTimescale;
	const float slowDownSpeed = 1f;


	private void Awake()
	{
		defaultTimescale = Time.timeScale;
		
		Game.Init();
	}



	// timescale management

	public void SlowDownTimescale()
	{
		StartCoroutine(SlowDownTimescaleCoroutine());
	}
	IEnumerator SlowDownTimescaleCoroutine()
	{
		float newTimeScale;

		while (Time.timeScale > 0)
		{
			newTimeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime * slowDownSpeed, 0, 1);
			Time.timeScale = newTimeScale;
			yield return null;
		}
	}

	public void TimescaleBoost()
	{
		StartCoroutine(TimescaleBoostCoroutine());
	}
	IEnumerator TimescaleBoostCoroutine()
	{
		const float timeScaleBoostSpeed = 0.05f;
		const float timeScaleBoost = 3f;
		
		Time.timeScale = defaultTimescale * timeScaleBoost;
		
		for (float t = 0; t < 1; t += Time.unscaledDeltaTime * timeScaleBoostSpeed)
		{
			Time.timeScale = Mathf.Lerp(Time.timeScale, defaultTimescale, t);
			//Debug.Log(Time.timeScale);
			yield return null;
		}
		
		RestoreTimescale();
	}
	
	void RestoreTimescale()
	{
		Time.timeScale = defaultTimescale;
	}




	// scene management

	public void RestartCurrentScene()
	{
		RestoreTimescale();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void LoadMenuScene()
	{
		RestoreTimescale();
		SceneManager.LoadScene("menu");
	}
	public void LoadGameScene()
	{
		RestoreTimescale();
		SceneManager.LoadScene("game");
	}




	// input management
	public void EscapeOrBackKeysPressed()
	{
		Exit();
	}
	
	
	
	
	
	// OS
	
	public void Exit()
	{
#if UNITY_EDITOR
		Debug.Log("Unable to exit app in editor");
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		AndroidJavaObject unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
		unityActivity.Call<bool>("moveTaskToBack", true);
		}
#else
		Application.Quit();
#endif
	}
}