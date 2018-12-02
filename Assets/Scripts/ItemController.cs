using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
	
	[Header("Child Elements")]
	public GameObject spriteChild;
	public GameObject shadowChild;
	public TrailRenderer trail;
	public Blow blow;
	
	Rigidbody rb;
	Collider col;
	
	void Awake() {
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	}
	
	void Start() {
		trail.enabled = false;
	}
	
	
	
	public void ItemPickedUp() {
		
		// disable self
		rb.isKinematic = true;
		col.enabled = false;
		spriteChild.SetActive(false);
		shadowChild.SetActive(false);
	}
	
	public void ItemDrop(Vector3 v) {
		
		// position itself and show, but dont enable anything else
		// item guarding not interactable
		transform.position = v;
		
		spriteChild.SetActive(true);
		shadowChild.SetActive(true);
		
		blow.Reset();
	}
	
	public void ItemFreedFromGuardingEntrance() {
		StartCoroutine(FindNewPositionWhenSetFreeFromGuard());
	}
	IEnumerator FindNewPositionWhenSetFreeFromGuard()
	{
		trail.enabled = true;
		trail.Clear();
		
		Vector3 desiredPos = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
		const float newPosLerpSpeed = 3f;
		
		for (float t = 0; t < 1; t += Time.deltaTime * newPosLerpSpeed) {
			rb.position = Vector3.Lerp(rb.position, desiredPos, t);
			yield return null;
		}
		
		// new position found - reset properties to be picked up again
		trail.enabled = false;
		rb.isKinematic = false;
		col.enabled = true;
		
		blow.Reset();
	}
	
	
	
	
	public void Dispose() {
		Destroy(gameObject);
	}
}
