using System.Collections;
using UnityEngine;

public class CameraEffector : MonoBehaviour {

	Camera cam;

	float initialFOV;


	private void Awake()
	{
		cam = GetComponent<Camera>();
		initialFOV = cam.orthographicSize;
	}

	public void ModifyCam(float f)
	{
		cam.orthographicSize = initialFOV - f;
	}
}