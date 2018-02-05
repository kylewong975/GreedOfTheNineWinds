using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharController {
	public static PlayerController myplayer;
    public Dictionary<string, KeyCode> keyDict;
    public Dictionary<string, bool> keyBools;
	public bool usingGun = true;
	public Sprite gun_sprite;
	public Sprite pick_sprite;
    private bool speedTime = false;
	private bool double_jumped = false;
    public bool immune = false;
	private SpriteRenderer sr;
	private Light l;
	private int last_lowest_height;


    // Use this for initialization
    new void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (myplayer) {
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		myplayer = this;
		sr = gameObject.GetComponentsInChildren<SpriteRenderer> () [1];
		l = gameObject.GetComponentsInChildren<Light> ()[0];
        base.Start();
        jumpMaxSpeed = Physics2D.gravity.y * -1 / 3;
        keyDict = new Dictionary<string, KeyCode>()
        {
			{"moveLeft", KeyCode.A },
			{"moveRight", KeyCode.D },
            {"jumpOnce", KeyCode.Space },
            {"jumpHeld", KeyCode.Space },
            {"weap_1", KeyCode.Alpha1 },
            {"weap_2", KeyCode.Alpha2 },
            {"weap_3", KeyCode.Alpha3 },
            {"weap_4", KeyCode.Alpha4 },
            {"weap_5", KeyCode.Alpha5 },
            {"weap_6", KeyCode.Alpha6 },
            {"enter", KeyCode.E },
            {"inventory", KeyCode.Q },
            {"reload", KeyCode.R },
            {"pickup", KeyCode.F },
            {"drop", KeyCode.G },
            {"light", KeyCode.L  },
            {"debugWin", KeyCode.N },
            {"debugTimeSpeed", KeyCode.T },
        };
    }

	// called first
	void OnEnable()
	{
		
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if(l!=null)
			l.range = 22 - 5*GameManager.ins.afflictionDifficulty;
	}


	// Update is called once per frame
	new void Update () {
        if (GameManager.ins.gameActive)
        {
            base.Update();
            //Debug.DrawRay (transform.position, Vector2.down * 0.3f, Color.green);
            keyBools = new Dictionary<string,bool>()
            {
				{"moveLeft", Input.GetKeyDown(keyDict["moveLeft"])},
				{"moveRight", Input.GetKeyDown(keyDict["moveRight"])},
                {"jumpOnce", Input.GetKeyDown(keyDict["jumpOnce"])},
                {"jumpHeld", Input.GetKey(keyDict["jumpHeld"])},
                {"shootHold", Input.GetMouseButton(0)},
                {"shootOnce", Input.GetMouseButtonDown(0)},
                {"weap_1", Input.GetKeyDown(keyDict["weap_1"])},
                {"weap_2", Input.GetKeyDown(keyDict["weap_2"])},
                {"weap_3", Input.GetKeyDown(keyDict["weap_3"])},
                {"weap_4", Input.GetKeyDown(keyDict["weap_4"])},
                {"weap_5", Input.GetKeyDown(keyDict["weap_5"])},
                {"weap_6", Input.GetKeyDown(keyDict["weap_6"])},
                {"inventory", Input.GetKeyDown(keyDict["inventory"]) },
                {"enter", Input.GetKeyDown(keyDict["enter"]) },
                {"reload", Input.GetKeyDown(keyDict["reload"])},
                {"pickup", Input.GetKeyDown(keyDict["pickup"])},
                {"drop", Input.GetKeyDown(keyDict["drop"])},
                {"light", Input.GetKeyDown(keyDict["light"])},
                {"debugWin", Input.GetKeyDown(keyDict["debugWin"])},
                {"debugTimeSpeed", Input.GetKeyDown(keyDict["debugTimeSpeed"])},
            };

			if (Input.GetKeyDown("c")){
				usingGun=!usingGun;

				if(usingGun)
					sr.sprite = gun_sprite;
				else 
					sr.sprite = pick_sprite;
			}

            if (canMove)
            {
                Movement();
            }
			if (canShoot)
			{
				Attack();
			}
            if (isAfflicted && !immune)
                HandleAffliction();
            KeyHandler();

			//update lowest height
			if (GetComponent<Transform>().position.y < last_lowest_height)
				last_lowest_height = (int)GetComponent<Transform> ().position.y;


            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - rb.position.x > .1)
            {
                FaceRight(false);
				sr.flipX = true;
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - rb.position.x < -.1)
            {
                FaceRight(true);
				sr.flipX = false;
            }
        }
        //zaWarudo();
        //time stop disabled for performance issues

        //anim.SetFloat ("Speed Y", Mathf.Abs(rb.velocity.y));
        //anim.SetFloat ("Speed X", Mathf.Abs(rb.velocity.x));
    }

    void HandleAffliction()
	{	
		if ((int) GetComponent<Transform> ().position.y - last_lowest_height < 5)
			return; 

        switch(GameManager.ins.afflictionDifficulty)
        {
			case 0:
                setHealth(maxHealth);
                break;
            case 1:
				setHealth (health - 5);
                break;
			case 2:
				setHealth (health - 5);
                // HALLUCINATING HEALTH BAR
				break;
			case 3:
				setHealth (health - 10);
				break;
            case 4:
				setHealth (health - 15);
				break;
			default:
				setHealth (health - 20);
				break;
        }
		last_lowest_height = (int)GetComponent<Transform> ().position.y;
    }

    void Attack() {
        
	}

	void Movement() {

        if (isGrounded && keyBools["jumpOnce"]) {
			isJumping = true;
			double_jumped = false;
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		} else if (!isGrounded && !keyBools["jumpHeld"]) {
			if(rb.velocity.y > jumpMaxSpeed)
				rb.velocity = new Vector2 (rb.velocity.x, jumpMaxSpeed);
		} else if (!double_jumped && keyBools["jumpOnce"]) {
			double_jumped = true;
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		}

        float horizInput = Mathf.Round(Input.GetAxis("Horizontal") * 100f) / 100f;
        if (!isGrounded)
            horizInput *= 1.2f;

		if ((keyBools["moveLeft"] && rb.velocity.x > 0) || (keyBools["moveRight"] && rb.velocity.x < 0)) {
			moveHorizontal (horizInput, false, 2.0f);
		} else {
			moveHorizontal (horizInput, false);
		}


        if(horizInput != 0.00)
        {
            isHealing = false;
        }
	}

    void KeyHandler()
    {
        try
        {
            if (keyBools["inventory"])
            {
                GameManager.ins.guiController.toggleInventory();
                Debug.Log("toggling inv: " + GameManager.ins.guiController.inventory.inv.Keys.ToString());
            }
            if (keyBools["debugWin"])
            {
                GameManager.ins.gameOver = true;
                GameManager.ins.wonGame = true;
            }
            if (keyBools["debugTimeSpeed"])
            {
                speedTime = !speedTime;
                Time.timeScale = speedTime ? 10f : 1f;
            }
            if (keyBools["enter"] && GameObject.Find("DoorTrigger").GetComponent<DoorController>().playerInDoor)
            {
                SceneController.openScene("ShopScene");
            }
            // TESTING SELECTING LOOT ITEMS
            if (GameManager.ins.guiController.showInventory())
            {
                if (keyBools["weap_1"])
                {
                    GameManager.ins.guiController.inventory.selectLoot(0);
                }
                if (keyBools["weap_2"])
                {
                    GameManager.ins.guiController.inventory.selectLoot(1);
                }
                if (keyBools["weap_3"])
                {
                    GameManager.ins.guiController.inventory.selectLoot(2);
                }
                if (keyBools["weap_4"])
                {
                    GameManager.ins.guiController.inventory.selectLoot(3);
                }
                if (keyBools["weap_5"])
                {
                    GameManager.ins.guiController.inventory.selectLoot(4);
                }
                if (keyBools["weap_6"])
                {
                    GameManager.ins.guiController.inventory.selectLoot(5);
                }
            }
        } catch
        {

        }
    }

    public override void takeDamage(int damage, bool right)
    {
        if (health != 1 && damage >= health & damage < health*2)
            damage = health - 1;
        setHealth(health - damage);
    }

}