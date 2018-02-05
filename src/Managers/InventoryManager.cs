using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public Dictionary<string, int> inv;
    
    Image inventory;
    public int inv_selected = -1;
    public int worth = 0;
    public int cash = 0;
    public List<string> inv_as_list;
    public bool usable;
    public Button useButton;

	// Use this for initialization
	void Start () {
        inventory = GetComponent<Image>();
        usable = gameObject.name == "Use_Inventory";
        if (usable)
        {
            useButton = inventory.transform.Find("UseButton").GetComponent<Button>();
            useButton.onClick.AddListener(handleItems);
        }
    }
	
	// Update is called once per frame
	void Update () {
        var temp = inv.ToList();
        inv_as_list = (from kvp in temp select kvp.Key).ToList();
        displayInventory(inv);
    }

    void displayInventory(Dictionary<string, int> inv)
    {
        int temp_worth = 0;
        for (int i = 1; i <= 9; i++)
        {
            Image item_img = inventory.transform.Find("item_" + i).GetComponent<Image>();
            if (i <= inv_as_list.Count)
            {
                item_img.sprite = LootDatabase.getLoot(inv_as_list[i - 1]).getSprite();
                item_img.transform.Find("Count").GetComponent<Text>().text = inv[inv_as_list[i - 1]].ToString();
                if(!inv_as_list[i - 1].StartsWith("potion"))
                    temp_worth += LootDatabase.getLoot(inv_as_list[i - 1]).getWorth() * inv[inv_as_list[i - 1]];
                if (i - 1 == inv_selected)
                {
                    item_img.transform.localScale = new Vector3(1.3f, .52f, 1.0f) * 1.2f;
                } else
                {
                    item_img.transform.localScale = new Vector3(1.3f, .52f, 1.0f);
                }
            }
            item_img.gameObject.SetActive(i <= inv_as_list.Count);
        }
        worth = temp_worth;
        inv_selected = inv_selected < inv_as_list.Count ? inv_selected : -1;
        inventory.transform.Find("Worth").GetComponent<Text>().text = "Worth: " + worth + " - Cash: " + cash;
        Text desc = inventory.transform.Find("loot_description").GetComponent<Text>();
        LootDatabase.Loot selected_loot = inv_selected == -1 ? null : LootDatabase.getLoot(inv_as_list[inv_selected]);
        desc.text = (inv_selected == -1 ? "" : selected_loot.name)
            + "\n" + (inv_selected == -1 ? "" : ("Use: " + selected_loot.getEffect()))
            + "\n" + (inv_selected == -1 ? "" : ("Worth: " + selected_loot.getWorth() + " gold"));
        if (usable)
        {
            if(inv_selected != -1)
            {
                useButton.enabled = true;
            } else
            {
                useButton.enabled = false;
            }
        }
    }

    public void selectLoot(int x)
    {
        inv_selected = inv_selected == x ? -1 : x;
    }

    public LootDatabase.Loot getSelectedLoot()
    {
        return LootDatabase.getLoot(inv_as_list[inv_selected]);
    }

    public void handleItems()
    {
        string itemType = inv_as_list[inv_selected];
        PlayerController player = GameManager.ins.playerController;
        switch (itemType)
        {
            case "gold":
                player.setHealth(player.getHealth() + 50);
                break;
            case "emerald":
                player.setHealth(player.getHealth() + 30);
                break;
            case "sapphire":
                player.setHealth(player.getHealth() + 25);
                break;
            case "pearl":
                player.setHealth(player.getHealth() + 10);
                break;
            case "potion_health":
                player.setHealth(player.getHealth() + 25);
                break;
            case "potion_dex":
                player.setHealth(player.getHealth() + 50);
                break;
            case "potion_str":
                player.immune = true;
                break;
            case "potion_luk":
                for(int i = 0; i < 3; i++)
                {
                    LootDatabase.rates[i] += .1f;
                }
                break;
        }
        inv[itemType] -= 1;
        if (inv[itemType] == 0)
            inv.Remove(itemType);
    }
}
