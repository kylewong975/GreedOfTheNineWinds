using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    private Canvas canvas;
    private Text fpsCounter, deathMessage;
    private PlayerController playerController;
    private Slider healthSlider;
    public InventoryManager inventory;
    private Transform cursor;

    //FPS STUFF
    private int frameCount = 0;
    double dt = 0.0;
    double fps = 0.0;
    double updateRate = 1.0;  // 4 updates per sec.

    public bool displayFPS = true;

    private static GUIController ins;
    private void Awake()
    {
        if (!ins)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        fpsCounter = canvas.transform.Find("FPS_Counter").GetComponent<Text>();
        deathMessage = canvas.transform.Find("DeathMessage").GetComponent<Text>();
        healthSlider = canvas.transform.Find("HealthBar").GetComponent<Slider>();
        inventory = canvas.transform.Find("Use_Inventory").GetComponent<InventoryManager>();
        inventory.inv = GameManager.ins.level.inventory;
        playerController = GameManager.ins.playerController;

        cursor = canvas.transform.Find("Cursor");

        //deathMessage.transform.Find("RestartGameButton").GetComponent<Button>().onClick.AddListener(RestartGame);

        if (displayFPS)
            InvokeRepeating("updateFPS", 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        // FPS DISPLAY
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0 / updateRate;
        }

        // GAME STUFF

        if (GameManager.ins.gameActive)
        {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, transform.Find("Canvas").GetComponent<Camera>(), out pos);
			cursor.GetComponent<Image>().transform.position = canvas.transform.TransformPoint(pos);
            healthSlider.value = playerController.getHealth();
        }
        
	}

    void updateFPS() {
        fpsCounter.text = "FPS: " + (int) fps;
    }

    void RestartGame()
    {
        SceneController.openScene("TestLevel0");
    }

    public void showGUI(bool show)
    {
        //cursor.SetActive(show);
    }

    public bool showInventory()
    {
        return inventory.gameObject.activeInHierarchy;
    }

    public void toggleInventory()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
    }
}
