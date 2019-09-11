using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{
    public string ItemName;
    public int ItemID;
    public string ItemDescription;
    public Texture2D Icon;
    public int ItemPower;
    public int ItemArmor;
    public int ItemVisibility;
    public int ItemSpeed;
    public ItemType itemType;
    
    public enum ItemType
    {
        Weapon,
        Quest,
        Crafting,
        Armor,
        Unusable,
        Consumable
    }

    public Item(string name, int id, string description, int power, int speed, ItemType type)
    {
        this.ItemName = name;
        this.ItemID = id;
        this.ItemDescription = description;
        this.ItemPower = power;
        this.ItemSpeed = speed;
        this.itemType = type;
    }

    public Item()
    {
        //this.ItemID = -1;
    }


}
