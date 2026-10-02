using UnityEngine;

public class GroundScoreTrigger : MonoBehaviour
{
    [SerializeField]
    private Player groundOwner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null)
        {
            ball.HitGround(groundOwner);
        }
    }
}