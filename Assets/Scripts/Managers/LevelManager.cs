using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public static LevelManager ins;
    public PlayerController player;
    public int lowestPlayerPos = 0, currentPlayerPos;
    public List<GameObject> coinsInLevel;
    public Dictionary<string, int> inventory;
    public bool atTop = false;
    public bool loadFromShop = false;

    private void Awake()
    {
		if (!ins) {
			ins = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
        inventory = new Dictionary<string, int>();

    }

    // Use this for initialization
    void Start () {
        player = GameManager.ins.playerController;
		SceneManager.sceneLoaded += OnSceneLoaded;
    }

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		player = GameManager.ins.playerController;
		lowestPlayerPos = currentPlayerPos = 0;
		if (SceneManager.GetActiveScene ().name.StartsWith ("TestLevel")) {
			
			if (loadFromShop) {
				player.transform.position = new Vector2 (-11, 2);
				loadFromShop = false;
				player.setHealth (player.maxHealth);
				return;
			}
			
			if (SceneManager.GetActiveScene ().name.EndsWith ("0")) {
				player.setHealth (player.maxHealth);
                player.immune = false;
                LootDatabase.rates = LootDatabase.defaultRates;
				player.transform.position = GameObject.FindGameObjectWithTag ("DownPosition").transform.Find ("RestorePoint").transform.position;
				return;
			}
			
			if (atTop) {
				player.transform.position = GameObject.FindGameObjectWithTag ("UpPosition").transform.Find ("Adjust").transform.Find ("RestorePoint").transform.position;
			} else {
				player.transform.position = GameObject.FindGameObjectWithTag ("DownPosition").transform.Find ("RestorePoint").transform.position;
				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-5f, 10f);
			}
		}
	}

	// Update is called once per frame
	void Update () {
        if (GameManager.ins.gameActive)
        {
            updatePlayerPosAndCondition();
            foreach (GameObject x in coinsInLevel)
            {
                if (x != null)
                    GameManager.ins.checkPlayerVisibility(x.transform);
            }
        }
	}

    void updatePlayerPosAndCondition()
    {
        if (GameManager.ins.playerController != null)
        {
            // Allows currentPlayerPos to match the current block level player is at (e.g. base level = 0)
            currentPlayerPos = (int)Mathf.Ceil(GameManager.ins.playerController.transform.position.y) - 1;
            if (currentPlayerPos < lowestPlayerPos)
                lowestPlayerPos = currentPlayerPos;
            player.isAfflicted = (currentPlayerPos - lowestPlayerPos >= 10);
        }
    }
}
