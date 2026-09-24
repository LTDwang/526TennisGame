using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.attachedRigidbody;

            if (ballRb != null)
            {
                playerController.BallEnterHitRange(ballRb);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.attachedRigidbody;

            if (ballRb != null)
            {
                playerController.BallExitHitRange(ballRb);
            }
        }
    }
}