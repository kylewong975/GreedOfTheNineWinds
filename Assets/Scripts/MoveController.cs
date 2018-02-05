using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

	private float enemy_speed=100F;
	private Rigidbody2D rb;
	private CharController cc;
	private bool isCarryingBlock = false;

	public GameObject wall;

	private int currentDir;
	public int freeMap=15;
	//LEFT RIGHT UP DOWN


	void Start () {
		currentDir = (int)(4 * Random.value);
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CharController> ();
		changeVelocity ();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground" && isCarryingBlock == false && Random.value > .80) {
			Destroy (coll.gameObject);
			isCarryingBlock = true;
			transform.Find ("Wall").gameObject.SetActive (true);
		} else {
			//blocked
			if (Random.value > .95)
				rb.velocity = -rb.velocity;
			else {
				currentDir = (int)(4 * Random.value);
				changeVelocity ();
			}
			
		}
	}

	void changeVelocity(){
		float hor=0, ver=0;
		if (currentDir == 0)
			ver = -1;
		else if (currentDir == 1)
			ver = 1;
		else if (currentDir == 2)
			hor = 1;
		else
			hor = -1;
		rb.velocity = new Vector2 (hor, ver)* Time.deltaTime *enemy_speed;
	}
	// Update is called once per frame
	void Update () {
		if (GameManager.ins.gameActive)
		{
			if (Mathf.Abs (transform.position.x - (int)transform.position.x) < .1 &&
				Mathf.Abs (transform.position.y - (int)transform.position.y) < .1) {
				//at center
				if (Random.value > .90 && isCarryingBlock == true) {
					float hor=0, ver=0;
					if (currentDir == 0)
						ver = -1;
					else if (currentDir == 1)
						ver = 1;
					else if (currentDir == 2)
						hor = 1;
					else
						hor = -1;
					isCarryingBlock = false;
					transform.Find ("Wall").gameObject.SetActive (false);
					Instantiate (wall, new Vector3((int)transform.position.x,(int)transform.position.y,0), Quaternion.identity);
					transform.position = new Vector2((int)transform.position.x,(int)transform.position.y) - new Vector2 (hor, ver);
					currentDir = (int)(4 * Random.value);
					changeVelocity ();
				}
			}
			if (Random.value > .98) {
				currentDir = (int)(4 * Random.value);
				changeVelocity ();
			}

		}
}
}
