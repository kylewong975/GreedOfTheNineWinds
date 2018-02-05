using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {
	
	private CharController character;
	private float debouncer;
	private ParticleSystem flare;

	// Use this for initialization
	void Start () {
		debouncer = 0.0f;
		character = gameObject.GetComponentInParent<CharController>();
		flare =  gameObject.GetComponentsInChildren<ParticleSystem> ()[0];
	}
	
	// Update is called once per frame
	void Update () {
	}

	// touched enemy
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" && (int)debouncer != (int)Time.time) {
			debouncer = Time.time;
			character.setHealth (character.getHealth () - 5);
			flare.Emit (30);
			Debug.Log ("Playing");
		} else if (col.tag == "MoverEnemy" && (int)debouncer != (int)Time.time) {
			debouncer = Time.time;
			character.setHealth (character.getHealth () - 12);
			flare.Emit (40);
		}
	}

	// keep losing health while you touch the enemy indefinitely
	void OnTriggerStay2D(Collider2D col) {
		if (col.tag == "Enemy" && (int)debouncer != (int)Time.time) {
			debouncer = Time.time;
			character.setHealth (character.getHealth () - 2);
			// Debug.Log (character.getHealth ());
			flare.Emit(30);
			Debug.Log ("Playing");
		}
		else if (col.tag == "MoverEnemy" && (int)debouncer != (int)Time.time) {
			debouncer = Time.time;
			character.setHealth (character.getHealth () - 5);
			// Debug.Log (character.getHealth ());
			flare.Emit(40);
			Debug.Log ("Playing");
		}
	}
}
