using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : MonoBehaviour
{
    public float GuiPlacementY1;
    public float GuiPlacementX1;
    public float GuiWidthX1;
    public static float ShowVariety = 0;

    private float upgradeHealthValue = 0;
    private float upgradePowerValue = 0;
    private float upgradeBombValue = 0;
    private float upgradeHookValue = 0;

    private float CoinsCollected;
    private float PlayersHealth;
    private float BombCapacity;
    private float HookCapacity;

    public GameObject Cipher;

    // Use this for initialization
    public void Start()
    {
        this.CoinsCollected = CollectibleController.CoinsCollected;
        this.PlayersHealth = PlayerController.Health;
        this.BombCapacity = PlayerController.BombCapacity;
        this.HookCapacity = PlayerController.HookCapacity;

        //this.CollectibleObject = GameObject.Find("Collectible");
        //this.Collectible = CollectibleObject.GetComponent<CollectibleController>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (PlayersHealth > 100)
        {
            PlayerController.Health = PlayersHealth;
        }

        if (upgradeBombValue != 0 ||
           upgradeHealthValue != 0 ||
            upgradeHookValue != 0)
        {
            CollectibleController.CoinsCollected = this.CoinsCollected;
            PlayerController.BombCapacity = this.BombCapacity;
            PlayerController.HookCapacity = this.HookCapacity;
        }
    }

    public void OnGUI()
    {
        if (ShowVariety == 0)
        {
            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * GuiPlacementY1,
                Screen.width * GuiWidthX1, Screen.height * 0.1f),
                "Upgrades"))
            {
                ShowVariety = 1;
            }

            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.1f),
                Screen.width * GuiWidthX1, Screen.height * 0.1f),
                "Trade"))
            {
                ShowVariety = 2;
            }

            if(GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.2f),
                Screen.width * GuiWidthX1, Screen.height * 0.1f),
                "Decipher"))
            {
               if(PlayerController.CodexPages >=1)
               {
                   Cipher.transform.position = new Vector2(6.45f, 0.16f);
                   ShowVariety = 5;
               }
            }

            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.3f),
                Screen.width * GuiWidthX1, Screen.height * 0.1f),
                "Back"))
            {
                Application.LoadLevel("StartingScene");
            }
        }



        if (ShowVariety == 1)
        {
            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * GuiPlacementY1,
                Screen.width * GuiWidthX1, Screen.height * 0.1f)
                , "Health"))
            {
                if (CoinsCollected >= 2 && upgradeHealthValue == 0)
                {
                    upgradeHealthValue++;
                    CoinsCollected -= 2;
                    this.PlayersHealth = 120;
                }

                else if (CoinsCollected >= 4 && upgradeHealthValue == 1)
                {
                    this.PlayersHealth = 140;
                    CoinsCollected -= 4;
                }
            }

            //if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.1f),
            //    Screen.width * GuiWidthX1, Screen.height * 0.1f)
            //    , "Power"))
            //{

            //}

            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.2f),
                Screen.width * GuiWidthX1, Screen.height * 0.1f)
                , "Increase hook Capacity"))
            {
                if (CoinsCollected >= 2 && upgradeHookValue == 0)
                {
                    this.HookCapacity += 2;
                    this.upgradeHookValue++;
                    CoinsCollected -= 2;
                }

                if (CoinsCollected >= 4 && upgradeHookValue == 1)
                {
                    this.HookCapacity += 2;
                    this.upgradeHookValue++;
                    CoinsCollected -= 4;
                }
            }

            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.3f),
                Screen.width * GuiWidthX1, Screen.height * 0.1f)
                , "Increase bombs's capacity"))
            {
                if (CoinsCollected >= 2 && upgradeBombValue == 0)
                {
                    this.BombCapacity += 2;
                    this.upgradeBombValue++;
                    CoinsCollected -= 2;
                }
                if (CoinsCollected >= 4 && upgradeBombValue == 1)
                {
                    this.BombCapacity += 2;
                    upgradeBombValue++;
                    CoinsCollected -= 4;
                }
            }

            if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.4f),
                Screen.width * GuiWidthX1, Screen.height * 0.1f)
                , "Back"))
            {
                Application.LoadLevel("StartingScene");
            }
        }

    }

    private void ShowItems()
    {

    }
}
