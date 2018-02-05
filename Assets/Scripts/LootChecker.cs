using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class LootChecker : MonoBehaviour {

    bool isTouchingLoot = false;
	GameObject lootObj;
	public Text loot_text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isLoot()
    {
        return isTouchingLoot;
    }

    public GameObject lootObject()
    {
        return lootObj;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            isTouchingLoot = true;
            lootObj = other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            isTouchingLoot = true;
            lootObj = other.gameObject;

			if (LootDatabase.database.Keys.Contains(other.gameObject.tag)) {
                string otherSprite = other.GetComponent<SpriteRenderer>().sprite.name;
                if (!GameManager.ins.level.inventory.ContainsKey(otherSprite))
                    GameManager.ins.level.inventory.Add(otherSprite, 1);
				else
                    GameManager.ins.level.inventory[otherSprite] += 1;
                Destroy (other.gameObject);
			}
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isTouchingLoot = false;
        lootObj = null;
    }
}
