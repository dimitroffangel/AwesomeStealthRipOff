using UnityEngine;
using System.Collections;

public class ExplosionCollider : MonoBehaviour
{
    private bool IsBombDestroyed;
    public void Update()
    {
    }

    public void OnCollisionStay2D(Collision2D collider)
   {
        if(collider.gameObject.CompareTag("Enemy") ||
           collider.gameObject.CompareTag("Archer"))
        {
            Invoke("DestroyExplosion", 0.2f);
            if(IsBombDestroyed)
            {
                Destroy(collider.gameObject);
            }
        }
   }

    public void DestroyExplosion()
    {
        this.IsBombDestroyed = true;
    }
}
