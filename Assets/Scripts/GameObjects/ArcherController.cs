using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public Transform ViewStart;
    public Transform ViewEnd;
    public Transform CloseView;

    private bool Spotted;
    private bool IsCloseRange;
    private bool IsHit;
    private bool IsHiddenTextField;
    private Animator Animate;

    public Rigidbody2D ArrowPrefab;
    public float AttackSpeed;
    public float Cooldown;
    public float Health = 100f;
    public static float ArchersKilled = 0;

    public Animator AnimateValue
    {
        get
        {
            return this.Animate;
        }
    }

    // Use this for initialization
    public void Start()
    {
        this.Animate = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        Raycast();
        Behaviours();

        if(Health <= 0)
        {
            Destroy(gameObject);
            ArchersKilled++;
        }

        if(ArchersKilled != 0 && Health < 100 + (ArchersKilled * 40))
        {
            Health += 40;
        }

    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        var playerAnimation = collider.gameObject.GetComponent<PlayerController>().Animate;
        if (collider.gameObject.CompareTag("Player"))
        {
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

    public void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.textField);
        style.alignment = TextAnchor.MiddleCenter;

        if(Health <= 0 && !IsHiddenTextField)
        {
            CameraController.Score += 200;
            GUI.TextField(new Rect(50, 120, 50, 40), "+200", style);
            Invoke("HideTextField", 4);
        }
    }

    public void HideTextField()
    {
        this.IsHiddenTextField = true;
    }

    public void Raycast()
    {
        var playerLayer = 1 << LayerMask.NameToLayer("Player");
        var viewStartPosition = this.ViewStart.transform.position;
        viewStartPosition = new Vector2(gameObject.transform.position.x + 1.95f, gameObject.transform.position.y + 0.5f);
        var viewEndPosition = this.ViewEnd.transform.position;
        viewEndPosition = new Vector2(gameObject.transform.position.x - 2.73f, gameObject.transform.position.y + 0.20f);
        var closeViewPosition = this.CloseView.transform.position;
        closeViewPosition = new Vector2(gameObject.transform.position.x - 1.05f, gameObject.transform.position.y - -0.20f);
        //initiliaze another vector for the stealth kill


        Debug.DrawLine(viewStartPosition, viewEndPosition, Color.white);
        Debug.DrawLine(viewStartPosition, closeViewPosition, Color.yellow);
        this.Spotted = Physics2D.Linecast(viewStartPosition, viewEndPosition, playerLayer);
        this.IsCloseRange = Physics2D.Linecast(viewStartPosition, closeViewPosition, playerLayer);

    }

    public void Behaviours()
    {
        if (Spotted && !IsCloseRange)
        {
            if(Time.time >= Cooldown)
            {
                if (this.Animate.GetInteger("animeTransition") != 1)
                {
                    this.Animate.SetInteger("animeTransition", 1);
                }
                else
                {
                    this.Animate.SetInteger("animeTransition", 1);
                }
                Fire();

            }
        }

        if (IsCloseRange)
        {
            RandomValue();
            Debug.Log("Draw Swords");
            if (IsHit)
            {
                Health -= 0.05f;
                Debug.Log("Sliced");
                Debug.Log(Health);
            }
        }

        else if(!Spotted && !IsCloseRange)
        {
            if(this.Animate.GetInteger("animeTransition") >= 1)
            {
                this.Animate.SetInteger("animeTransition", 0);
            }

            if(IsHit)
            {
                Destroy(gameObject);
                ArchersKilled++;
            }
        }
    }

    public void Fire()
    {
        var arrowPrefabPositionX = transform.position.x + -1;
        var arrowPosition = new Vector2(arrowPrefabPositionX, transform.position.y);
        var arrowPrefab = Instantiate(ArrowPrefab, arrowPosition, Quaternion.identity) as Rigidbody2D;

        arrowPrefab.AddForce(Vector3.left * 200);

        this.Cooldown = Time.time + AttackSpeed;
    }

    public void RandomValue()
    {
        var random = Random.Range(1, 3);

        if (random == 1)
        {
            this.Animate.SetInteger("animeTransition", 2);
        }

        if (random == 2)
        {
            this.Animate.SetInteger("animeTransition", 3);
        }
    }
}

