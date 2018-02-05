using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiningScript : MonoBehaviour {
	public float max_mine_distance;
	private Rigidbody2D rb;
	private Transform tf;
	private int lm; 


	void Start(){
		
		rb = GetComponent<Rigidbody2D>();
		tf = GetComponent<Transform>();
 		lm = LayerMask.GetMask ("Ground");

	}
	void Update () {
		bool on = !gameObject.GetComponent<PlayerController> ().usingGun;

		if(! on)
			return;

		if (Input.GetMouseButtonDown(0)) {
			Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			RaycastHit2D hit = Physics2D.Raycast(tf.position,difference, max_mine_distance,lm);
			if (hit && hit.collider.tag == "Ground") {
				print ("HIt");
				Destroy (hit.collider.gameObject);
			}
		}
	}
}
