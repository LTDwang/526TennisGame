using UnityEngine;

public class GroundScoreTrigger : MonoBehaviour
{
    [SerializeField]
    private Player groundOwner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        BallController ball = other.GetComponent<BallController>();

        if (ball != null)
        {
            ball.HitGround(groundOwner);
        }
    }
}