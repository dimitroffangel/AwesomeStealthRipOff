using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    public void Start()
    {
        //Items.Add(new Item("Amulet", 4, "An amulet for wearing", 2, 0, Item.ItemType.Crafting));
        //Items.Add(new Item("Cap", 5, "A cap to keep you smexy", 0, 0, Item.ItemType.Unusable));
    }
}
