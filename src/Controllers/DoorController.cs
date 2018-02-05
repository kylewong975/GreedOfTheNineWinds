using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public bool playerInDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.ins.playerController.gameObject)
        {
            playerInDoor = true;
            transform.Find("speechbubble").GetComponent<SpriteRenderer>().enabled = true;
        }
        //show E tooltip
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.ins.playerController.gameObject)
        {
            playerInDoor = false;
            transform.Find("speechbubble").GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
