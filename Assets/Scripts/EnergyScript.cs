using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour {

	private CharController character;
	private float debouncer;

	// Use this for initialization
	void Start () {
		debouncer = 0.0f;
		character = gameObject.GetComponentInParent<CharController>();
	}

	// Update is called once per frame
	void Update () {
	}

	// DUMMY FUNCTIONALITY: THIS IS THE GENERAL CODE LOGIC BUT NOT THE ACTUAL IMPLEMENTATION OF GAME LOGIC
	/*
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" && (int)debouncer != (int)Time.time) {
			debouncer = Time.time;
			character.setEnergy (character.getEnergy () - 1);
			// Debug.Log (character.getEnergy ());
		}
	}
	*/
}
