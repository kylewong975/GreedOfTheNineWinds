using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

	public int damageToTake;

	// Use this for initialization
	void Start() {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			CharController enemyController = other.GetComponent<CharController> ();
			int currHealth = enemyController.getHealth ();
			enemyController.setHealth (currHealth - damageToTake);
			// Debug.Log ("enemy");
			Destroy (gameObject);
		} else if (other.tag != "Player" && other.tag != "MainCamera") {
			Debug.Log (other.tag);
			Destroy (gameObject);
		}
	}
}
