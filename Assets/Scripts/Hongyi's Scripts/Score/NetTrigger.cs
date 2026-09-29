using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BallController ball = other.GetComponent<BallController>();

        if (ball != null)
        {
            ball.HitNet();
        }
    }
}