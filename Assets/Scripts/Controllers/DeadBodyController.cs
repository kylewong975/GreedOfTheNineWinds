using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyController : MonoBehaviour {

    public bool active = true;

	// Use this for initialization
	void Awake () {
        StartCoroutine("DeathDelay", 20.0);
    }
	
	// Update is called once per frame
	void Update () {
        GameManager.ins.checkPlayerVisibility(transform);
    }

    public IEnumerator DeathDelay(float Count)
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.layer = LayerMask.NameToLayer("DeadBody");
        active = false;
        yield return new WaitForSeconds(Count);
        Destroy(this.gameObject);
    }
}
