using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	private CharController character;

	// Use this for initialization
	void Start () {
		character = gameObject.GetComponentInParent<CharController>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Ground") 
			|| col.gameObject.layer == LayerMask.NameToLayer("GroundL0") 
			|| col.gameObject.layer == LayerMask.NameToLayer("Enemy")
			|| col.gameObject.layer == LayerMask.NameToLayer("StrongGround")) { 
            character.isJumping = false;
            character.isGrounded = true;
        }
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Ground") 
			|| col.gameObject.layer == LayerMask.NameToLayer("GroundL0") 
			|| col.gameObject.layer == LayerMask.NameToLayer("Enemy") 
			|| col.gameObject.layer == LayerMask.NameToLayer("StrongGround"))
		{
            character.isJumping = false;
            character.isGrounded = true;
        }
    }

	void OnTriggerExit2D(Collider2D col)
	{
        if (character.isJumping)
            character.isGrounded = false;
        else
            StartCoroutine("DelayOffGround", .05);
	}

    IEnumerator DelayOffGround(float Count)
    {
        yield return new WaitForSeconds(Count);
        character.isGrounded = false;
    }
}
