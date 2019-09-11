using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 100f;
    public float JumpSpeed = 100f;
    public float ClimbSpeed = 100f;

    public static float Health = 100f;
    public static float BombCapacity = 4;
    public static float HookCapacity = 1;
    public static float CodexPages = 1;

    private float JumpCount = 0;
    private float Timer = 100f;

    
    private Rigidbody2D Rb;
    private Animator animate;
    private SpriteRenderer SpriteRender;
    private BoxCollider2D BoxCollider;
    private BoxCollider2D ExplosionCollider;

    private bool DoesClimb;
    public static bool IsHidden;
    private bool IsCloseHit;
    private bool IsStrucked;
    private bool FightGround = true;
    private bool TresureFound;
    private bool IsTheft;
    private bool HasSMokeBomb = true;
    public static bool IsGuardTheft;

    public GameObject Explosion;
    public Rigidbody2D PrefabGrab;
    public Transform ViewStart;
    public Transform ViewEnd;
    public Texture2D ReloadButton;
    public GameObject SmokeBomb;

    //public float HealthBar
    //{
    //    get
    //    {
    //        return this.Health;
    //    }
    //}

    public Animator Animate
    {
        get
        {
            return this.animate;
        }
    }

    // Use this for initialization
    public void Start()
    {
        this.Rb = this.GetComponent<Rigidbody2D>();
        this.animate = this.GetComponent<Animator>();
        this.SpriteRender = this.GetComponent<SpriteRenderer>();
        this.BoxCollider = this.GetComponent<BoxCollider2D>();

        Health = 100;
        if(this.animate.GetInteger("animTransition") == 3)
        {
            this.animate.SetInteger("animTransition", 0);
        }
        SaveScore();
        LoadScore();
    }

    // Update is called once per frame
    public void Update()
    {
        if(IsHidden && Input.GetKeyDown(KeyCode.V))
        {
            this.SpriteRender.color = Color.white;
            IsHidden = false;
            this.BoxCollider.isTrigger = false;
            this.Rb.isKinematic = false;
        }
        

        if (!DoesClimb && !IsHidden)
        {
            //if(Health > 100)
            //{
            //    Debug.Log(Health);
            //}

            Move();

            if((Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt)) && Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Comb");
            }

            if (Input.GetButton("Fire1"))
            {
                if (FightGround)
                {
                    animate.SetInteger("animTransition", 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && BombCapacity != 0) 
            {
                BombCapacity--;
                var bombRigidBody = this.GetComponent<ExplosionCollider>();
                var boxCollider = GetComponent<Rigidbody2D>();
                
                var explosionX = Explosion.transform.position.x;

                if (transform.eulerAngles.y == 0)
                {
                    explosionX = gameObject.transform.position.x + 4f;
                }
                else if(transform.rotation.eulerAngles.y < 181 && transform.rotation.eulerAngles.y > 179)
                {
                    explosionX = gameObject.transform.position.x - 4f;
                }
                Explosion.transform.position = new Vector2(explosionX, gameObject.transform.position.y);
                
                //Instantiate(Explosion);
                Invoke("DestoryBomb", 4f);
                //raycast for the dead of the enemy
            }

            if(Input.GetKeyDown(KeyCode.G) && HasSMokeBomb)
            {
                this.HasSMokeBomb = false;
                this.SmokeBomb.transform.position = gameObject.transform.position;
                //Instantiate(SmokeBomb);
                //Destroy(SmokeBomb);
               // DestroyObject(SmokeBomb);
                Invoke("DestroySmokeBomb", 3f);
            }

            if (Input.GetKeyDown(KeyCode.X) && HookCapacity != 0)
            {
                HookCapacity--;
                this.animate.SetInteger("animTransition", 6);
                float prefabGrabPositionX = transform.position.x;
                float prefabGrabPositionY = transform.position.y;

                if (transform.eulerAngles.y == 0)
                {
                    prefabGrabPositionX += 1.02f;
                }

                else if(transform.rotation.eulerAngles.y < 181 && transform.rotation.eulerAngles.y > 179)
                {
                    prefabGrabPositionX -= 1.02f;
                }
                var prefabGrabPosition = PrefabGrab.transform.position;
                prefabGrabPosition = new Vector2(prefabGrabPositionX, prefabGrabPositionY);
                var prefabGrab = Instantiate(PrefabGrab, prefabGrabPosition, Quaternion.identity) as Rigidbody2D;

                if(transform.eulerAngles.y == 0)
                {
                    prefabGrab.AddForce(Vector3.right * 200);
                }

                else if(transform.rotation.eulerAngles.y < 181 && transform.rotation.eulerAngles.y > 179)
                {
                    prefabGrab.AddForce(Vector3.left * 200);
                }
            }

            RaycastRoberry();
            BehaviourRobbery();

            if (IsCloseHit)
            {
                Health -= 1.5f;
                this.IsCloseHit = false;
                Debug.Log(Health);
            }

            if(IsStrucked)
            {
                Health -= 25;
                this.IsStrucked = false;
                Debug.Log(Health);
            }

            if (Health <= 0)
            {
                this.animate.SetInteger("animTransition", 3);
                Invoke("EndTimer", 1);
                if(Timer <= 0)
                {
                    Time.timeScale = 0;
                }
                this.BoxCollider.size = new Vector2(0.3f, 0.1f);
                Debug.Log(CameraController.Score);
                SaveScore();

            }

            if(TresureFound)
            {
                this.animate.SetInteger("animTransition", 6);
                CodexPages = 1;
            }
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            DoesClimb = true;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.Rb.AddForce(new Vector2(0, ClimbSpeed));
            }
            else
            {
                DoesClimb = false;
            }
        }

        if (other.gameObject.CompareTag("HiddenSpot"))
        {
            var countTap = 0;
            if (Input.GetKeyDown(KeyCode.B) && countTap == 0)
            {
                this.SpriteRender.color = Color.black;
                Debug.Log("cannnot move");
                countTap++;
                IsHidden = true;
                this.BoxCollider.isTrigger = true;
                this.Rb.isKinematic = true;
            }
        }
        
    }

    public void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            //var enemyHealth = collider.gameObject.GetComponent<WarriorController>().HealthBar;
            var warriorAnimation = collider.gameObject.GetComponent<WarriorController>().AnimationValue;

            if(warriorAnimation.GetInteger("animEnemy") > 1)
            {
                this.IsCloseHit = true;
            }
        }

        if(collider.gameObject.CompareTag("Archer"))
        {
            var archerAnimation = collider.gameObject.GetComponent<ArcherController>().AnimateValue;

            if (archerAnimation.GetInteger("animeTransition") > 1)
            {
                this.IsCloseHit = true;
            }

            Debug.Log("Archers Draw Swords");
        }

        if(collider.gameObject.CompareTag("Tresure"))
        {
            this.TresureFound = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Arrow"))
        {
            this.IsStrucked = true;
        }
    }

    public void OnGUI()
    {
        var rectPosition = new Rect(50, 50, 260, 150);
        GUI.backgroundColor = Color.clear;
    //    GUI.DrawTexture(new Rect(rectPosition.left, rectPosition.top + 60f, rectPosition.width - 100f, rectPosition.height - 20)
    //, ReloadButton);
        if (Health <= 0)
        {

            GUI.Box(rectPosition, string.Format("Score: {0}", CameraController.Score));
            if (GUI.Button(new Rect(rectPosition.left + 50f, rectPosition.top + 20f, rectPosition.width - 100f, rectPosition.height - 20)
                ,ReloadButton))
            {
                this.animate.SetInteger("animTransition", 0);
                Application.LoadLevel(Application.loadedLevel);

            }
        }

        if(TresureFound)
        {
            var score = CameraController.Score;
            var deadWarriors = WarriorController.DeadGuards;
            var deadArchers = ArcherController.ArchersKilled;
            var hiddenDeaths = HiddenSpotController.HiddenKills;

            GUI.Box(rectPosition, string.Format("Your score is: {0} \n Warriors killed: {1} * 100: {2} \n Archers killed: {3} * 200: {4} \n Guards killed from hidden spots: {5} * 400: {6} \n  "
                , score, deadWarriors, deadWarriors * 100
                ,deadArchers, deadArchers * 200
                ,hiddenDeaths, hiddenDeaths * 400));

            if(GUI.Button(new Rect(rectPosition.left + 50f, rectPosition.top + 20f, rectPosition.width - 100f, rectPosition.height - 20f)
                ,ReloadButton))
            {
                this.animate.SetInteger("animTransition", 0);
                Application.LoadLevel(Application.loadedLevel);

            }
        }
    }

    public void RaycastRoberry()
    {
        if (transform.eulerAngles.y == 0)
        {
            this.ViewStart.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.4f);
            this.ViewEnd.position = new Vector2(gameObject.transform.position.x + 0.8f, gameObject.transform.position.y + 0.4f);
        }

        if (transform.rotation.eulerAngles.y > 179 && transform.rotation.eulerAngles.y < 181)
        {
            this.ViewStart.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.4f);
            this.ViewEnd.position = new Vector2(gameObject.transform.position.x - 0.8f, gameObject.transform.position.y + 0.4f);
        }

        var guardLayer = 1 << LayerMask.NameToLayer("Guard");
        this.IsTheft = Physics2D.Linecast(this.ViewStart.position, this.ViewEnd.position, guardLayer);
        Debug.DrawLine(ViewStart.position, ViewEnd.position, Color.white);
    }

    public void BehaviourRobbery()
    {
        if(Input.GetKeyDown(KeyCode.F) && IsTheft)
        {
            IsGuardTheft = true;
            var random = Random.Range(1, 5);
            CollectibleController.CoinsCollected += random;
            //potions, bombs, hook, something like crafting system
        }

        else
        {
            IsGuardTheft = false;
        }
    }


    public void DestroySmokeBomb()
    {
        SmokeBomb.transform.position = new Vector2(-7.36f, -10.39f);
    }

    public void DestoryBomb()
    {
        Explosion.transform.position = new Vector2(2.26f, -8.94f);
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LimitJump();

            if (JumpCount >= 1)
            {
                this.Rb.AddForce(new Vector2(0, JumpSpeed * Time.deltaTime));
                this.animate.SetInteger("animTransition", 4);
                if(animate.GetInteger("animTransition") == 4)
                {
                    Debug.Log("Jump");
                }
                this.FightGround = false;
                if (Input.GetButton("Fire1") && !FightGround)
                {
                    this.animate.SetInteger("animTransition", 5);
                    FightGround = true;
                }
                else
                {
                    FightGround = true;
                }
            }
        }

        //if(Input.GetKey(KeyCode.UpArrow))
        //{

        //    this.Rb.AddForce(new Vector2(0, 15));
        //}

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.Rb.AddForce(new Vector2(MoveSpeed * Time.deltaTime, 0));
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.Rb.AddForce(new Vector2(-MoveSpeed * Time.deltaTime, 0));
            transform.eulerAngles = new Vector2(0, 180);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            this.animate.SetInteger("animTransition", 2);
            this.BoxCollider.size = new Vector2(0.35f, 0.3f);
        }
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    this.animate.SetInteger("animTransition", 2);
        //    var boxCollider = GetComponent<BoxCollider2D>();
        //    boxCollider.size = new Vector2(0.35f, 0.3f);
        //}
        else
        {
            this.animate.SetInteger("animTransition", 0);
        }
    }

    private void LimitJump()
    {
        float distance = this.SpriteRender.bounds.size.y;
        var layerMask = 1 << LayerMask.NameToLayer("Player");
        var result = Physics2D.Raycast(this.Rb.position, -Vector2.up, distance, ~layerMask);

        if(result.collider != null)
        {
            JumpCount = 1;
        }

        else
        {
            JumpCount = 0;
        }
    }

    public static void SaveScore()
    {
        if(CameraController.Score >= PlayerPrefs.GetInt("Score", CameraController.Score))
        {
            PlayerPrefs.SetInt("Score", CameraController.Score);
        }
    }

    public static void LoadScore()
    {
        PlayerPrefs.GetInt("Score", CameraController.Score);
    }

}
