using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null)
        {
            ball.HitNet();
        }
    }
}