using UnityEngine;
using System.Collections;

public class HiddenSpotController : MonoBehaviour
{
    public bool IsHidden;
    public GameObject Enemy;
    public static float HiddenKills = 0;

    // Use this for initialization
   public void Start()
    {

    }

    // Update is called once per frame
   public void Update()
    {
        //Debug.Log(gameObject.transform.position.x - Enemy.transform.position.x);
       if(PlayerController.IsHidden
       && Input.GetButtonDown("Fire1")
       && gameObject.transform.position.x - Enemy.transform.position.x <= 0.7f
       && gameObject.transform.position.x - Enemy.transform.position.x >=  -0.6f)
       {
           Destroy(Enemy);
           this.IsHidden = true;
           CameraController.Score += 400;
 
       }

       if(PlayerController.IsHidden
         && Input.GetKeyDown(KeyCode.F)
         && gameObject.transform.position.x - Enemy.transform.position.x <= 0.7f
         && gameObject.transform.position.x - Enemy.transform.position.x >= -0.6f)
       {
           var random = Random.Range(1, 5);
           CollectibleController.CoinsCollected += random;
           this.IsHidden = true;
       }
    }

    public void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.textField);
        style.alignment = TextAnchor.MiddleCenter;

        if(IsHidden)
        {
            //CameraController.Score += 400;
            GUI.TextField(new Rect(50, 120, 50, 40), "+400", style);
            Invoke("HideScore", 4);
           
        }
    }

    public void HideScore()
    {
        this.IsHidden = false;
        Debug.Log(CameraController.Score);
    }
}
