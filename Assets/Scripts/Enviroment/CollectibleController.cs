using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour
{
    private bool IsCollected;
    public static float CoinsCollected = 0;

    // Update is called once per frame

    //public void SaveValue()
    //{
    //    var myPersistentDataObject = new GameObject("Coin");
    //    var dataClass = myPersistentDataObject.AddComponent<CollectibleController>();
    //    DontDestroyOnLoad(gameObject);
    //}

    public void Update()
    {
        Debug.Log(CoinsCollected);

        if (IsCollected)
        {
            Debug.Log(CoinsCollected);
            CoinsCollected++;

            var gameObjectPositon = this.gameObject.transform.position;
            gameObjectPositon = new Vector2(-3.05f, -8.51f);
            //this.gameObject.transform.position = gameObjectPositon;
            gameObjectPositon = this.gameObject.transform.position;
            Destroy(gameObject);
            IsCollected = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            this.IsCollected = true;
        }
        else
        {
            this.IsCollected = false;
        }
    }
}
