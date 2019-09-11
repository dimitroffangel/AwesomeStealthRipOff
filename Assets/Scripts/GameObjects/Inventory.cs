using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
    public int SlotsX;
    public int SlotsY;
    public GUISkin Skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> Slots = new List<Item>();
    private bool ShowInventory;
    private ItemDatabase Database;
    private bool ShowTooltip;
    private string ToolTip;

    private bool DraggingItem;
    private Item DraggedItem;
    private int PrevIndex;

    private bool IsEnemyTheft;
    private float PlayersHealth;
    private bool IsScriptHidden = true;

    // Use this for initialization
   public void Start()
    {
        this.IsEnemyTheft = PlayerController.IsGuardTheft;
        this.PlayersHealth = PlayerController.Health;

        for (int i = 0; i < (SlotsX * SlotsY); i++)
        {
           this.Slots.Add(new Item());
           inventory.Add(new Item());
        }
        this.Database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        for (int i = 0; i < inventory.Count- 13; i++)
        {
            AddItem(i + 1);
        }
        //AddItem(1);
        //AddItem(2);
        //RemoveItem(1);

        //print(InventoryContains(2));

        //inventory[0] = Database.Items[0];
        //inventory[1] = Database.Items[1];
    }

    public void OnGUI()
   {
       this.ToolTip = "";
       GUI.skin = this.Skin;
       if (InventoryContains(8) && !IsScriptHidden)
       {
           GUI.backgroundColor = Color.clear;
           GUI.TextField(new Rect(50, 50, 50, 50), string.Format("{0} / 30", PlayerController.CodexPages));
           Invoke("HideScripts", 3f);
       }

       GUI.backgroundColor = Color.black;

        if(ShowInventory)
        {
            DrawInventory();
            if (ShowTooltip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 100), ToolTip);
            } 
        } 
        if(DraggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 30, 30)
                , DraggedItem.Icon);
        }
   }

    public void HideScripts()
    {
        this.IsScriptHidden = true;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            //Time.timeScale = 0;
            this.ShowInventory = !ShowInventory;
        }

        if (PlayerController.IsGuardTheft)
        {
            PlayerController.IsGuardTheft = !PlayerController.IsGuardTheft;
            RandomAdd();
        }

        for (int i = 1; i <= 30; i++)
        {
            if (PlayerController.CodexPages == i)
            {
                this.IsScriptHidden = false;
            }
        }
        Debug.Log(PlayerController.CodexPages);
        Debug.Log(PlayersHealth);
    }

    public void RandomAdd()
    {
        var random = Random.Range(1,7);
        if(random  != 0)
        {
            AddItem(random + 2);
            AddItem(random);
        }
    }

    public void DrawInventory()
    {
        var e = Event.current;
        int i = 0;
        for(int y = 0; y < this.SlotsY; y++)
        {
            for(int x = 0; x < this.SlotsX; x++)
            {
                var slotRect = new Rect(x * 50, y * 50, 40, 40);
                GUI.Box(slotRect, "", this.Skin.GetStyle("Slot"));
                Slots[i] = inventory[i];
                if(Slots[i].ItemName != null)
                {
                    GUI.DrawTexture(slotRect, Slots[i].Icon);
                    if(slotRect.Contains(e.mousePosition))
                    {
                        CreateToolTrip(Slots[i]);
                        this.ShowTooltip = true;
                        //e.button == 1 for right click
                        if(e.button == 0 && e.type == EventType.mouseDrag && !DraggingItem)
                        {
                            this.DraggingItem = true;
                            PrevIndex = i;
                            this.DraggedItem = Slots[i];
                            inventory[i] = new Item();
                        }
                        if(e.type == EventType.mouseUp && DraggingItem)
                        {
                            inventory[PrevIndex] = inventory[i];
                            inventory[i] = DraggedItem;
                            DraggingItem = false;
                            DraggedItem = null;
                        }
                    }
                }

                else
                {
                    if(slotRect.Contains(e.mousePosition))
                    {
                        if(e.type == EventType.MouseUp && DraggingItem)
                        {
                            inventory[i] = DraggedItem;
                            DraggingItem = false;
                            DraggedItem = null;
                        }
                    }
                }

                if(ToolTip == "")
                {
                    ShowTooltip = false;
                }

                if(e.isMouse && e.type == EventType.mouseDown && e.button == 1)
                {
                    if(Slots[i].itemType == Item.ItemType.Consumable)
                    {
                        UseConsumable(Slots[i], i, true);
                        if (PlayerController.Health < 100)
                        {
                            PlayerController.Health += 40;
                        }
                    }
                }

                i++;
            }
        }
    }

    private void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch(item.ItemID)
        {

            case 7: Debug.Log("consumed");
                break;

        }

        if(deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    public string CreateToolTrip(Item item)
    {
        this.ToolTip = item.ItemName + "\n" + "\n" + item.ItemDescription;
        return ToolTip;
    }

    public void RemoveItem(int id)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].ItemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < Database.Items.Count; j++ )
                {
                    if(Database.Items[j].ItemID == id)
                    {
                        this.inventory[i] = Database.Items[j];
                    }
                }
                    break;
            }
        }
    }

    public bool InventoryContains(int id)
    {
        bool result = false;
        for (int i = 0; i < inventory.Count; i++ )
        {
            result = inventory[i].ItemID == id;
            if(result)
            {
                break; 
            }
        }
        return result;
    }

    public void SaveInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            PlayerPrefs.SetInt("Inventory ", inventory[i].ItemID);
        }
    }

    public void LoadInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? 
                Database.Items[PlayerPrefs.GetInt("Inventory " + i)] 
                : new Item();
        }
    }
}
