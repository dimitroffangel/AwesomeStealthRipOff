using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private bool IsStarted;
    private bool SpaceClicked;

    private float ShowOptionsValue = 1f;

    public float GuiPlacementY1;
    public float GuiPlacementY2;

    public float GuiPlacementX1;
    public float GuiPlacementX2;

    public float GuiWidthX1;
    public float GuiWidthX2;

    public GameObject Player;
    public string Score;

    // Use this for initialization
  public void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
   public void Update()
  {
      if (Application.loadedLevel >= 1 && !SpaceClicked)
      {
          IsStarted = true;
      }
          if (IsStarted)
          {
              Time.timeScale = 1;
          }

          if (Input.GetKeyDown(KeyCode.Escape))
          {
              this.SpaceClicked = true;
              IsStarted = false;
              Time.timeScale = 0;
          }
    }

    public void OnGUI()
   {
        //TODO Test if show button can work for everthing when options are done
       if (!IsStarted)
       {
           if (ShowOptionsValue != 4)
           {
               if (GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * GuiPlacementY1,
                   Screen.width * GuiWidthX1, Screen.height * 0.1f)
                   , "Play Game"))
               {
                   IsStarted = true;
                   Application.LoadLevel("FirstLevel");
               }

               //if(GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY2 + 0.2f),
               //    Screen.width * GuiWidthX1, Screen.height * 0.1f)
               //    , "CityExplore"))
               //{
               //    Application.LoadLevel("PlanFinder");
               //    DontDestroyOnLoad(this.Player);
               //}

           }
           if (ShowOptionsValue == 1f)
           {
               if (GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * GuiPlacementY2,
                   Screen.width * GuiWidthX2, Screen.height * 0.1f)
                   , "Options"))
               {
                   ShowOptionsValue = 2f;
               }

               if (GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 + 0.1f),
                   Screen.width * GuiWidthX2, Screen.height * 0.2f / 2),
                   "Shop"))
               {
                   Application.LoadLevel("ShopScene");
                   Time.timeScale = 1;
               }
                
               if(GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.1f),
                   Screen.width * GuiWidthX1, Screen.height * 0.1f)
                   ,"Level Selection"))
               {
                   ShowOptionsValue = 4;
               }

               if(GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.2f),
                   Screen.width * GuiWidthX1, Screen.height * 0.2f / 2),
                   "Quit"))
               {
                   Application.Quit();
               }
           }


           if (ShowOptionsValue == 2f)
           {
               if (GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 - 0.1f),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f)
                   , "Resolution"))
               {
                   this.ShowOptionsValue = 3f;
                   // Screen.SetResolution(640, 1136, true);
               }

               if (GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f)
                   , "Audio"))
               {

               }

               if(GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 + 0.1f),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f)
                   ,"FullScreen"))
               {
                  Screen.fullScreen = !Screen.fullScreen;
                   Debug.Log(Screen.fullScreen);
                   //TODO: POP-Up button for "Yer sure? and Timer of 10 seconds"
               }

               if (GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 + 0.2f),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f),
                   "Back"))
               {
                   this.ShowOptionsValue = 1f;
               }
           }

           //TODO: FullScreen
           //Found it!!! Screen.fullScreen = !Screen.fullScreen;
           if (ShowOptionsValue == 3f)
           {
               if(GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 - 0.1f),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f ),
                   "640 x 1136"))
               {
                   Screen.SetResolution(640, 1136, false);
               }

               if(GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f),
                   "1024 x 768"))
               {
                   Screen.SetResolution(1024, 768, false);
               }

               if(GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 + 0.1f),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f),
                   "1280 x 960"))
               {
                   Screen.SetResolution(1024, 768, false);
               }

               if(GUI.Button(new Rect(Screen.width * GuiPlacementX2, Screen.height * (GuiPlacementY2 + 0.2f),
                   Screen.width * GuiWidthX2, Screen.height * 0.1f),
                   "Back"))
               {
                   this.ShowOptionsValue = 2f;
               }
           }

           if(ShowOptionsValue == 4f)
           {
               var buttonRect = new Rect(Screen.width * (GuiPlacementX2 + 0.2f), Screen.height * (GuiPlacementY2),
                   Screen.width* GuiWidthX2, Screen.height * 0.1f);

               if (buttonRect.Contains(Event.current.mousePosition))
               {
                   Debug.Log("asd");
                   this.Score = string.Format("Best score: {0}", CameraController.Score);
                   GUI.TextField(buttonRect, this.Score);
               }

               if(GUI.Button(buttonRect,
                    "1"))
               {
                   PlayerController.LoadScore();
                   Application.LoadLevel("FirstLevel");
               }

               if(GUI.Button(new Rect(Screen.width * GuiPlacementX1, Screen.height * (GuiPlacementY1 + 0.1f),
                   Screen.width * GuiWidthX1, Screen.height * 0.1f)
                   ,"Back"))
               {
                   ShowOptionsValue = 1f;
               }
           }
       }
   }
}
