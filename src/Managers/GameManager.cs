using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is what I recommend doing to make it easier to access things from class to class, I learned this from someone but I forgot.
//the comments are just examples of what could be a 'manager'

public class GameManager : MonoBehaviour {
	public static GameManager ins;
	public LevelManager level;  
    public PlayerController playerController;
    public Camera2DFollow camera2D;
    public GUIController guiController;
    public AudioManager audioManager;
    public float timeScale = 1;
    public bool gameActive = false;
    public bool gameOver = false;
    public bool wonGame = false;
	public int afflictionDifficulty=0;
    public int deepestAfflictionDifficulty=0;
    //public ItemManager item;
    //public HealthManager health;
    //public PauseMenuManager pauseMenu;
    //public InventoryManager inventory;

	void Awake(){
		Time.timeScale = 1;
		ins = this;
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		camera2D = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
		guiController = camera2D.gameObject.GetComponent<GUIController>();
		audioManager = GameObject.Find("AudioSystem").GetComponent<AudioManager>();
        gameActive = true;
	}

	// called first
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Time.timeScale = 1;
		ins = this;
		level = GetComponent<LevelManager>();
        if (scene.name.StartsWith("TestLevel"))
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            camera2D = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
            guiController = camera2D.gameObject.GetComponent<GUIController>();
            audioManager = GameObject.Find("AudioSystem").GetComponent<AudioManager>();
        }
		//item = GetComponent<ItemManager>();
		//health = GetComponent<HealthManager>();
		//pauseMenu = GetComponent<PauseMenuManager>();
		//inventory = GetComponent<InventoryManager>();
	}

    void Update()
    {
        if (gameActive)
        {
            Cursor.visible = true; // previously false
            gameOver = !playerController.isAlive();
            if (gameOver)
            {
                GameObject deathMessage = GameObject.Find("Main Camera").transform.Find("Canvas").transform.Find("DeathMessage").gameObject;
                deathMessage.SetActive(true);
                Cursor.visible = true;
                guiController.showGUI(false);
                if (guiController.showInventory())
                    guiController.toggleInventory();
                gameActive = false;
            }
        }
    }

    public void checkPlayerVisibility(Transform target)
    {
        /*if (playerController != null)
        {
            RaycastHit2D playerRaycast = Physics2D.Raycast(target.position, playerController.transform.position - target.position, 999, LayerMask.GetMask("Player", "Ground"));
            if (playerRaycast.transform != null)
            {
                SpriteRenderer targetSprite = target.GetComponent<SpriteRenderer>();
                if (!targetSprite.enabled && (playerRaycast.transform.gameObject.Equals(playerController.gameObject) || Vector3.Distance(target.position, playerController.transform.position) <= 0.3f))
                {
                    targetSprite.enabled = true;
                }
                else if (targetSprite.enabled && !(playerRaycast.transform.gameObject.Equals(playerController.gameObject) || Vector3.Distance(target.position, playerController.transform.position) <= 0.3f))
                {
                    targetSprite.enabled = false;
                }
            }
        }*/
    }
}