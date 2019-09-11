using UnityEngine;
using System.Collections;

public class CipherController : MonoBehaviour
{
    public string[] Alphabet;
    public float NextAlphabet = 1f;

    public int CurrentFirstAlphabet = 0;
    public int CurrentSecondAlphabet = 0;
    public int CurrentThirdAlphabet = 0;
    public int CurrentFourthAlphabet = 0;

    private float PressedKey = 0;
    private float CiphersDeciphered = 0;
    private float CurrentCipher = 0;

    // Use this for initialization
    public void Start()
    {
        Alphabet = new string[] 
        {"A","B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
        "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z"};
    }

    // Update is called once per frame
    public void Update()
    {

        if (CurrentFirstAlphabet >= 25)
        {
            this.CurrentFirstAlphabet = 0;
        }

        if (CurrentSecondAlphabet >= 25)
        {
            this.CurrentSecondAlphabet = 0;
        }

        if (CurrentThirdAlphabet >= 25)
        {
            this.CurrentThirdAlphabet = 0;
        }

        if (CurrentFourthAlphabet >= 25)
        {
            this.CurrentFourthAlphabet = 0;
        }

        if (CurrentFirstAlphabet <= -1)
        {
            this.CurrentFirstAlphabet = 24;
        }

        if (CurrentSecondAlphabet <= -1)
        {
            this.CurrentSecondAlphabet = 24;
        }
        if (CurrentThirdAlphabet <= -1)
        {
            this.CurrentThirdAlphabet = 24;
        }
        if (CurrentFourthAlphabet <= -1)
        {
            this.CurrentFourthAlphabet = 24;
        }

        if(CurrentCipher <= 0)
        {
            CurrentCipher = 1;
        }
    }

    public void OnGUI()
    {
        GUI.color = Color.black;
        GUI.backgroundColor = Color.clear;
        if (ShopMenu.ShowVariety == 5)
        {
            GUI.color = Color.white;
        }
        else
        {
            GUI.color = Color.black;

        }
        var e = Event.current;
        var rect = new Rect(300, 210, 40, 40);
        var rectSecond = new Rect(350, 210, 40, 40);
        var rectThird = new Rect(400, 210, 40, 40);
        var rectFourth = new Rect(450, 210, 40, 40);

        if (CurrentCipher == 1)
        {
            GUI.Box(new Rect(325, 120, 140, 40), "Ceaser cipher");
            GUI.Box(new Rect(180, 140, 400, 40), "It is a guilt that i read to you in this moment messer EZIO");
        }

        if (CurrentCipher == 2)
        {
            GUI.Box(new Rect(325, 120, 140, 40), "Ceaser cipher Part II");
            GUI.Box(new Rect(200, 140, 400, 40), "My time has passed. It is an hour to tell you the secret\n that has troubled me for so long. It is an");
        }
        GUI.Box(rect, string.Format("{0}", Alphabet[CurrentFirstAlphabet]));
        GUI.Box(rectSecond, string.Format("{0}", Alphabet[CurrentSecondAlphabet]));
        GUI.Box(rectThird, string.Format("{0}", Alphabet[CurrentThirdAlphabet]));
        GUI.Box(rectFourth, string.Format("{0}", Alphabet[CurrentFourthAlphabet]));

        var currentState = 0;
        if (GUI.Button(new Rect(250, 240, 200, 60), "Pay 25 coins for the deciphering") && currentState == 0)
        {
 
            if (CollectibleController.CoinsCollected >= 25)
            {
                CollectibleController.CoinsCollected -= 25;
                this.CiphersDeciphered += 1;
            }

                //this is thing is fooli' me for far too long.
            else
            {
                currentState = 1;
                GUI.TextField(new Rect(500, 290, 60, 60), "Yer' coins  ain't enough for ma'");
                Debug.Log("bla");
            }
        }
        else if(currentState == 1)
        {
            GUI.TextField(new Rect(250, 240, 200, 60), "Yer' coins  ain't enough for ma'");
        }


            GUI.Box(new Rect(450, 260, 100, 60), string.Format("{0}", CurrentCipher));

            if (GUI.Button(new Rect(500, 290, 60, 60), "Next"))
            {
                this.CurrentCipher++;
                Debug.Log(CurrentCipher);
            }
            if (GUI.Button(new Rect(250, 290, 60, 60), "Back"))
            {
                this.CurrentCipher--;
                Debug.Log(CurrentCipher);
            }

            if (GUI.Button(new Rect(350, 290, 100, 60), "Return to Shop"))
            {
                ShopMenu.ShowVariety = 0;
                gameObject.transform.position = new Vector2(7.08f, -10.59f);
            }

            if (GUI.Button(new Rect(500, 200, 60, 40), "Enter") && CurrentCipher == 1)
            {
                //hclr
                if (Alphabet[CurrentFirstAlphabet] == "H" &&
                   Alphabet[CurrentSecondAlphabet] == "C" &&
                   Alphabet[CurrentThirdAlphabet] == "L" &&
                   Alphabet[CurrentFourthAlphabet] == "R")
                {
                    this.CiphersDeciphered += 1;
                    GUI.TextField(new Rect(100, 190, 60, 40), string.Format("{0} / 30", CiphersDeciphered));
                    Debug.Log("Cipher deciphered");
                }
            }

            else if (GUI.Button(new Rect(500, 200, 60, 40), "Enter") && CurrentCipher == 2)
            {
                //item lwhp
                if (Alphabet[CurrentFirstAlphabet] == "L" &&
                   Alphabet[CurrentSecondAlphabet] == "W" &&
                   Alphabet[CurrentThirdAlphabet] == "H" &&
                   Alphabet[CurrentFourthAlphabet] == "P")
                {
                    this.CiphersDeciphered += 1;
                    GUI.TextField(new Rect(100, 190, 60, 40), string.Format("{0} / 30", CiphersDeciphered));
                }
            }

            if (rect.Contains(e.mousePosition))
            {

                if (Input.GetKeyDown(KeyCode.W))
                {
                    NextAlphabet += 0.25f;
                    this.PressedKey = 1;
                    Debug.Log(CurrentFirstAlphabet);
                    Debug.Log(NextAlphabet);
                }

                if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 1)
                {
                    NextAlphabet--;
                    CurrentFirstAlphabet++;
                }

                else if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 2)
                {
                    NextAlphabet++;
                    CurrentFirstAlphabet--;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    NextAlphabet -= 0.25f;
                    PressedKey = 2;
                }
            }

            if (rectSecond.Contains(e.mousePosition))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    NextAlphabet += 0.25f;
                    this.PressedKey = 1;
                    Debug.Log(CurrentFirstAlphabet);
                    Debug.Log(NextAlphabet);
                }

                if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 1)
                {
                    NextAlphabet--;
                    CurrentSecondAlphabet++;
                }

                else if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 2)
                {
                    NextAlphabet++;
                    CurrentSecondAlphabet--;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    NextAlphabet -= 0.25f;
                    PressedKey = 2;
                }
            }

            if (rectThird.Contains(e.mousePosition))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    NextAlphabet += 0.25f;
                    this.PressedKey = 1;
                    Debug.Log(CurrentFirstAlphabet);
                    Debug.Log(NextAlphabet);
                }

                if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 1)
                {
                    NextAlphabet--;
                    CurrentThirdAlphabet++;
                }

                else if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 2)
                {
                    NextAlphabet++;
                    CurrentThirdAlphabet--;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    NextAlphabet -= 0.25f;
                    PressedKey = 2;
                }
            }

            if (rectFourth.Contains(e.mousePosition))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    NextAlphabet += 0.25f;
                    this.PressedKey = 1;
                    Debug.Log(CurrentFirstAlphabet);
                    Debug.Log(NextAlphabet);
                }

                if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 1)
                {
                    NextAlphabet--;
                    CurrentFourthAlphabet++;
                }

                else if ((NextAlphabet % 2 == 0 || NextAlphabet % 3 == 0 || NextAlphabet % 5 == 0 || NextAlphabet % 7 == 0)
                    && PressedKey == 2)
                {
                    NextAlphabet++;
                    CurrentFourthAlphabet--;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    NextAlphabet -= 0.25f;
                    PressedKey = 2;
                }
            }
        }
    }
