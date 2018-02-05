using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNav : MonoBehaviour {

	public float enemy_speed;
	public float jumpHeight;

	private Transform playertf;
	private Transform selftf;
	private Rigidbody2D rb;
	private CharController cc;

	void Start () {
		playertf = GameManager.ins.playerController.gameObject.GetComponent<Transform>();
		selftf = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CharController> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.ins.gameActive)
        {
			if (playertf.position.x + .2F > selftf.position.x) {
				gameObject.GetComponent<CharController> ().moveHorizontal (enemy_speed * Time.deltaTime, false, 1.0F);
			} else if (playertf.position.x < .2F + selftf.position.x) {
				gameObject.GetComponent<CharController> ().moveHorizontal (-enemy_speed * Time.deltaTime, true, 1.0F);
			}
			if(Mathf.Abs(playertf.position.x - selftf.position.x) > .5F)
				cc.FaceRight(playertf.position.x < selftf.position.x);

            if (cc.isGrounded && Random.value < .01F)
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
	}
}
