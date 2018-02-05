using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class LootDatabase {

    public static Dictionary<string, Loot> database = new Dictionary<string, Loot>(){
        /*NAME            
         *          Name,
         *          Prefab,
         *          Sprite,
         *          Worth 
         *          Effect
         */
        {"gold",
            new Loot(
                "gold",
                "Gold Coin",
                Resources.Load<GameObject>("Prefabs/Loots/gold"),
                Resources.Load<Sprite>("Sprites/gold"),
                300,
                "Heals for 50 HP"
            )},
        {"emerald",
            new Loot(
                "emerald",
                "Emerald Coin",
                Resources.Load<GameObject>("Prefabs/Loots/emerald"),
                Resources.Load<Sprite>("Sprites/emerald"),
                250,
                "Heals for 30 HP"
            )},
        {"sapphire",
            new Loot(
                "sapphire",
                "Sapphire Coin",
                Resources.Load<GameObject>("Prefabs/Loots/sapphire"),
                Resources.Load<Sprite>("Sprites/sapphire"),
                200,
                "Heals for 25 HP"
            )},
        {"pearl",
            new Loot(
                "pearl",
                "Pearl Coin",
                Resources.Load<GameObject>("Prefabs/Loots/pearl"),
                Resources.Load<Sprite>("Sprites/pearl"),
                100,
                "Heals for 10 HP"
            )},
        // POTIONS
        {"potion_health",
            new Loot(
                "potion_health",
                "Potion of Healing",
                Resources.Load<GameObject>("Prefabs/Loots/pearl"),
                Resources.Load<Sprite>("Sprites/red_potion"),
                250,
                "Heals for 25 HP"
            )},
        {"potion_dex",
            new Loot(
                "potion_dex",
                "Potion of Dexterity",
                Resources.Load<GameObject>("Prefabs/Loots/pearl"),
                Resources.Load<Sprite>("Sprites/yellow_potion"),
                250,
                "Allows double jump"
            )},
        {"potion_str",
            new Loot(
                "potion_str",
                "Potion of Strength",
                Resources.Load<GameObject>("Prefabs/Loots/pearl"),
                Resources.Load<Sprite>("Sprites/green_potion"),
                250,
                "Nullifies this level's Curse effect"
            )},
        {"potion_luk",
            new Loot(
                "potion_luk",
                "Potion of Luck",
                Resources.Load<GameObject>("Prefabs/Loots/pearl"),
                Resources.Load<Sprite>("Sprites/blue_potion"),
                250,
                "Increases chances of quality loot"
            )},

    };

    public static List<float> defaultRates = new List<float>() { .05f, .30f, .65f, 1 };
    public static List<float> rates = defaultRates;

    public static Loot getLoot(string itemName)
    {
        return database[itemName];
    }

    public static Loot getRandomPotion()
    {
        return database[(new List<string>() { "red_potion", "yellow_potion", "green_potion", "blue_potion" })[Random.Range(0, 3)]];
    }

    public static Loot getRandomLoot()
    {
        if (Random.value <= rates[0])
            return database["gold"];
        else if (Random.value <= rates[1])
            return database["emerald"];
        else if (Random.value <= rates[2])
            return database["sapphire"];
        else if (Random.value <= rates[3])
            return database["pearl"];
        return null;
    }

    public class Loot
    {
        public string keyName;
        public string name;
        private GameObject prefab;
        private Sprite sprite;
        private int worth;
        private string effect;

        public Loot(string keyName, string name, GameObject prefab, Sprite sprite, int worth, string effect)
        {
            this.keyName = keyName;
            this.name = name;
            this.prefab = prefab;
            this.sprite = sprite;
            this.worth = worth;
            this.effect = effect;
        }

        public GameObject getPrefab()
        {
            return prefab;
        }

        public Sprite getSprite()
        {
            return sprite;
        }

        public int getWorth()
        {
            return worth;
        }

        public string getEffect()
        {
            return effect;
        }

        public string getKey()
        {
            return keyName;
        }
    }
}
