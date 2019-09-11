using UnityEngine;
using System.Collections;

public class MerchantController : MonoBehaviour
{
    private float AnimationTransition = 250f;
    private Animator Animate;
    // Use this for initialization
    public void Start()
    {
        this.Animate = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        this.AnimationTransition--;
        //Debug.Log(AnimationTransition);
        if (AnimationTransition < 125  && AnimationTransition >= 0)
        {
            this.Animate.SetInteger("animTransition", 2);
        }

        else if (AnimationTransition < 0 && AnimationTransition > -125)
        {
            this.Animate.SetInteger("animTransition", 3);
        }

        if(AnimationTransition == -125)
        {
            this.Animate.SetInteger("animTransition", 1);
            AnimationTransition = 250;
        }
    }
}
