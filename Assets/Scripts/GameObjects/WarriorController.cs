using UnityEngine;
using System.Collections;

public class WarriorController : MonoBehaviour
{
    public  float healthBar = 100f;
    public float lookSpeed = 0.5f;
    private float TimeChangeDir = 150f;
    private bool IsHiddenTextField;
  
    private bool HasSeen;
    private bool DoesFight;
    private bool IsHit;
    private bool IsBlind;

    public static float DeadGuards = 0;

    private Rigidbody2D Rb;
    private Animator Animate;


    public Transform ViewStart;
    public Transform ViewEnd;
    public GameObject Player;
    public GameObject HiddenSpot;


    //public float HealthBar
    //{
    //    get
    //    {
    //        return this.healthBar;
    //    }
    //}

    public Animator AnimationValue
    {
        get
        {
            return this.Animate;
        }
    }

    public void Start()
    {
        this.Rb = this.GetComponent<Rigidbody2D>();
        this.Animate = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        Raycasting();
        Behaviours();

        if (healthBar <= 0)
        {
            Destroy(gameObject);
            CameraController.Score += 100;
            DeadGuards++;
        }

        Debug.Log(IsBlind);

        //Debug.Log(DeadGuards);
        //Debug.Log(healthBar);

        if (DeadGuards != 0 && healthBar <= 100 + (DeadGuards * 40))
        {
            healthBar +=  40;
            //Debug.Log(healthBar);
        }
        //Debug.Log(DeadWarriors);
    }

    public void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            this.DoesFight = true;
            var playerAnimation = collider.gameObject.GetComponent<PlayerController>().Animate;

            if (playerAnimation.GetInteger("animTransition") == 1)
            {
                this.IsHit = true;
            }

            else
            {
                this.IsHit = false;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("SmokeBomb"))
        {
            this.IsBlind = true;
        }
    }

    public void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.textField);
        style.alignment = TextAnchor.MiddleCenter;

        if(healthBar <= 0 && !IsHiddenTextField)
        {
            GUI.TextField(new Rect(50, 120, 50, 40), "+100", style);
            Invoke("HideField", 4);
        }
    }

    public void HideField()
    {
        this.IsHiddenTextField = true;
    }

    public void Raycasting()
    {
        var playerLayer = 1 << LayerMask.NameToLayer("Player");
        var hiddenSpotLayer = 1 << LayerMask.NameToLayer("HiddenSpot");
        var viewStartPosition = ViewStart.transform.position;
        var viewEndPosition = ViewEnd.transform.position; 

        if (TimeChangeDir > 75)
        {
            viewStartPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.50f);
            viewEndPosition = new Vector2(gameObject.transform.position.x - 3.73f, gameObject.transform.position.y + 0.30f);
            Debug.DrawLine(viewStartPosition, viewEndPosition, Color.white);
        }

        else if (TimeChangeDir < 75)
        {
            viewStartPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f);
            viewEndPosition = new Vector2(gameObject.transform.position.x + 3.73f, gameObject.transform.position.y + 0.3f);
            Debug.DrawLine(viewStartPosition, viewEndPosition, Color.white);
        }
        this.HasSeen = Physics2D.Linecast(viewStartPosition, viewEndPosition, playerLayer);

        //take the bool varieble from the crowd
        //this.IsPlayerHidden = Physics2D.Linecast(viewStartPosition, viewEndPosition, hiddenSpotLayer);
        //Debug.DrawLine(viewStartPosition, viewEndPosition, Color.yellow);
    }

    public void Behaviours()
    {
        var playerPostionX = this.Player.transform.position.x;
        var hiddenSpotX = this.HiddenSpot.transform.position.x;

            //Debug.Log(hiddenSpotX - playerPostionX);


        if (!HasSeen 
            || (hiddenSpotX - playerPostionX <= 0.7f && hiddenSpotX - playerPostionX >= -0.01f &&
            PlayerController.IsHidden) || this.IsBlind)
        {
            Invoke("Timer", 0.2f);
            if (this.Animate.GetInteger("animEnemy") != 0)
            {
                this.Animate.SetInteger("animEnemy", 0);
            }

          if(IsHit)
          {
              healthBar = 0;
          }

            //Debug.Log(TimeChangeDir);
            if (TimeChangeDir > 75)
            {
                transform.Translate(Vector3.left * lookSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 0);
            }

            else if (TimeChangeDir < 75)
            {
                transform.Translate(-Vector3.right * lookSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 180);
            }
        }

        else
        {
            //var range = Vector2.Distance(transform.position, Player.transform.position);
            this.Animate.SetInteger("animEnemy", 1);
            if (DoesFight)
            {
                //TODO: Facing Angle kinda randomly. It is strange for me as everythin' else
                Invoke("RandomValue", 3f);

                if(IsHit && healthBar > 0)
                {
                    healthBar -= 0.05f;
                    //Debug.Log(healthBar);
                }
            }
        }
    }

    public void Timer()
    {
        if (TimeChangeDir <= 0)
        {
            TimeChangeDir = 150;
        }
        TimeChangeDir--;
    }

    public void RandomValue()
    {
        var random = Random.Range(1, 4);

        if (random == 1)
        {
            this.Animate.SetInteger("animEnemy", 2);
        }

        if (random == 2)
        {
            //TODO: Fire 'im in da yair
            this.Animate.SetInteger("animEnemy", 3);
        }

        if (random == 3)
        {
            this.Animate.SetInteger("animEnemy", 4);
        }
    }
}
