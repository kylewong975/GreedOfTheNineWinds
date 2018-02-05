using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    GameObject oldCamera, player;

    Canvas canvas;
    Button exitButton, buyButton, sellButton;
    InputField amount;
    Dictionary<string, int> inv;
    public string[] keys;
    public int[] amts;
    InventoryManager player_inv, seller_inv;

    // Use this for initialization
    void Start()
    {
        oldCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        oldCamera.SetActive(false);
        player.SetActive(false);
        if (GameObject.Find("EventSystem").GetComponent<GameManager>() != null)
            inv = new Dictionary<string, int>(GameManager.ins.level.inventory);
        else
        {
            inv = new Dictionary<string, int>();
            for (int i = 0; i < keys.Length; i++)
            {
                inv[keys[i]] = amts[i];
            }
        }

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        exitButton = canvas.transform.Find("ExitButton").GetComponent<Button>();
        buyButton = canvas.transform.Find("BuyButton").GetComponent<Button>();
        sellButton = canvas.transform.Find("SellButton").GetComponent<Button>();
        amount = canvas.transform.Find("Amount").GetComponent<InputField>();
        player_inv = canvas.transform.Find("Player_Inventory").GetComponent<InventoryManager>();
        player_inv.inv = this.inv;
        player_inv.cash = GameManager.ins.guiController.inventory.cash;
        seller_inv = canvas.transform.Find("Seller_Inventory").GetComponent<InventoryManager>();
        seller_inv.inv = new Dictionary<string, int>()
        {
            {"potion_health", 250 },
            {"potion_dex", 250 },
            {"potion_str", 250 },
            {"potion_luk", 250 },
        };

        exitButton.onClick.AddListener(exitShop);
        buyButton.onClick.AddListener(buyItem);
        sellButton.onClick.AddListener(sellItem);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buyItem()
    {
        try
        {
            int amt = amount.text == "" ? 1 : int.Parse(amount.text);
            LootDatabase.Loot otherLoot = LootDatabase.getLoot(seller_inv.inv_as_list[seller_inv.inv_selected]);
            if (amt >= 1 && seller_inv.inv_selected != -1 && player_inv.cash >= otherLoot.getWorth() * amt)
            {
                player_inv.cash -= otherLoot.getWorth() * amt;
                if (!player_inv.inv.ContainsKey(otherLoot.getKey()))
                {
                    player_inv.inv.Add(otherLoot.getKey(), 0);
                }
                player_inv.inv[otherLoot.getKey()] += amt;
            }
        }
        catch
        {

        }
    }

    void sellItem()
    {
        try
        {
            int amt = amount.text == "" ? 1 : int.Parse(amount.text);
            LootDatabase.Loot otherLoot = LootDatabase.getLoot(player_inv.inv_as_list[player_inv.inv_selected]);
			if (amt >= 1 && player_inv.inv_selected != -1 && player_inv.inv[otherLoot.getKey()] - amt >= 0)
            {
                player_inv.cash += otherLoot.getWorth() * amt;
                player_inv.inv[otherLoot.getKey()] -= amt; //1
                if (player_inv.inv[otherLoot.getKey()] <= 0)
                {
                    player_inv.inv.Remove(otherLoot.getKey());
                }
            }
        }
        catch
        {
        }
    }

    void exitShop()
    {
        GameManager.ins.level.loadFromShop = true;
        oldCamera.SetActive(true);
        player.SetActive(true);
        GameManager.ins.level.inventory = new Dictionary<string, int>(player_inv.inv);
        GameManager.ins.guiController.inventory.cash = player_inv.cash;
        GameManager.ins.guiController.inventory.inv = player_inv.inv;
        SceneController.openScene("TestLevel0");
    }
}
