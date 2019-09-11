using UnityEngine;
using System.Collections;

public class HookController : MonoBehaviour
{
    public GameObject Grabber;
    private bool IsGrabbed;

    // Use this for initialization
  public  void Start()
    {

    }

    // Update is called once per frame
   public void Update()
    {
       if(IsGrabbed)
       {
           IsGrabbed = false;
           var grabber = transform.position;
           Grabber.transform.position = grabber;
           Destroy(gameObject);
       }
    }

    public void OnCollisionEnter2D(Collision2D collider)
   {
        if(collider.gameObject.CompareTag("Enemy") 
            || collider.gameObject.CompareTag("Ladder") 
            || collider.gameObject.CompareTag("Block")
            || collider.gameObject.CompareTag("Door"))
        {
            this.IsGrabbed = true;
        }
            
   }
}
