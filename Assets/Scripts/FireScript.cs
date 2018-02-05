using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// place this in Player
public class FireScript : MonoBehaviour {

	public GameObject laser;
	private CharController character;

	void Start() {
		character = GameObject.Find("Player").GetComponent<CharController>();
	}
	
	// Update is called once per frame
	void Update () {
		bool on = gameObject.GetComponent<PlayerController> ().usingGun;
		if(! on)
			return;
		
		float velocity = 1000;
        float xOffset = 0;
		if (Input.GetMouseButtonDown(0) && Time.timeScale == 1.0){//when the left mouse button is clicked
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference = Vector3.Normalize(difference); 
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            GameObject clonedLaser = Instantiate(laser, new Vector2(transform.position.x + xOffset, transform.position.y), Quaternion.identity);
			clonedLaser.GetComponent<Rigidbody2D> ().AddForce (difference * velocity);
            clonedLaser.transform.rotation = Quaternion.Euler(0, 0, rotation_z);

            // new Vector2(velocity * Mathf.Sin(angle), velocity * Mathf.Cos(angle))
        }
	}
}
