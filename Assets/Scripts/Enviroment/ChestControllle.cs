using UnityEngine;
using System.Collections;

public class ChestControllle : MonoBehaviour
{
    private bool IsOpenned;
    private Animator Animate;

    // Use this for initialization
   public void Start()
    {
        this.Animate = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(IsOpenned)
        {
            this.Animate.SetInteger("animeTransition", 1);
            Invoke("EndGame", 2f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            IsOpenned = true;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;
    }
}
