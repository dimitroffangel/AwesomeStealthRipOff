using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
