using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// given delay, destroy the object after `delay` seconds
public class DelayedDestroy : MonoBehaviour {

	public float delay = 1.0f;

	void Update() {
		StartCoroutine(WaitAndDestroy ());
	}

	IEnumerator WaitAndDestroy() {
		yield return new WaitForSeconds (delay);
		Destroy(gameObject);
	}
}
