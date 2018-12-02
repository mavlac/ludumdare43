using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpenIn : MonoBehaviour {
	
	public CameraEffector cameraEffector;
	
	[Space]
	public Vector3 initialPosition;
	public Vector3 initialRotationVector;
	public float FOVModifier;
	public float effectSpeed;
	
	Vector3 defaultPosition;
	Quaternion initialRotation;
	Quaternion defaultRotation;
	
	void Awake () {
		defaultPosition = transform.localPosition;
		defaultRotation = transform.localRotation;
		
		transform.localPosition = initialPosition;
		initialRotation = Quaternion.Euler(initialRotationVector);
		transform.localRotation = initialRotation;
	}
	
	void Start () {
		
		StartCoroutine(OpenInCoroutine());
	}
	IEnumerator OpenInCoroutine() {
		
		float fov = FOVModifier;
		
		for (float t=0; t<1; t+=Time.deltaTime * effectSpeed) {
			
			transform.localPosition = Vector3.Lerp(transform.position, defaultPosition, t);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, defaultRotation, t);
			
			fov = Mathf.Lerp(fov, 0, t);
			cameraEffector.ModifyCam(fov);
			yield return null;
		}
		
		
		transform.localPosition = defaultPosition;
		transform.localRotation = defaultRotation;
		cameraEffector.ModifyCam(0);
	}
}
