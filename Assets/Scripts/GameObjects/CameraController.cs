using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public static int Score = 0;

    private float OffsetX;

    // Use this for initialization
   public void Start()
    {
        this.OffsetX = this.transform.position.x -
            this.Player.transform.position.x;
    }

    // Update is called once per frame
   public void Update()
    {
        var position = this.transform.position;
        position.x = Player.transform.position.x + OffsetX;
        this.transform.position = position;
    }
}
