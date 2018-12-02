using UnityEngine;

public class Levitate : MonoBehaviour {
	
	public float speed;
	public float amplitude;
	
	float rad = 0;
	float diff;
	float newY, oldY;
	
	Vector3 defaultLocalPosition;


	void Awake()
	{
		SetDefaultPositionToCurrent();
	}


	void Update ()
	{
		rad += speed * Time.deltaTime;
		
		if (rad > 2 * Mathf.PI)
			rad -= 2 * Mathf.PI;
		
		newY = Mathf.Sin(rad) * amplitude;
		diff = oldY - newY;
		oldY = newY;
		
		transform.Translate(0, diff, 0, Space.World); 
	}




	void SetDefaultPositionToCurrent()
	{
		defaultLocalPosition = transform.localPosition;
	}
	public void SetLocalPositionToDefault()
	{
		transform.localPosition = defaultLocalPosition;
	}
}