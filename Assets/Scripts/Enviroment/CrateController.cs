using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrateController : MonoBehaviour
{
    private bool isDamaged;
    public float Health = 100f;
    public static string peso = "pesho"; 

    public GameObject Collectible;

    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if(this.isDamaged)
        {
            
            this.Health -= 15;
        }

        if(Health <= 0)
        {
            var collectiblePosition = this.Collectible.transform.position;
            collectiblePosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Collectible.transform.position = collectiblePosition;
            Instantiate(Collectible);
            collectiblePosition = new Vector2(-3.05f, -8.51f);
            Collectible.transform.position = collectiblePosition;
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collider)
    {
        var playerState = collider.gameObject.GetComponent<PlayerController>().Animate;
        if (collider.gameObject.CompareTag("Player") && playerState.GetInteger("animTransition") >= 1)
        {
            this.isDamaged = true;
        }
    }
}
