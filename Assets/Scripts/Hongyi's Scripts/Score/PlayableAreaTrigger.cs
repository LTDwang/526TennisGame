using UnityEngine;

public class PlayableAreaTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        BallController ball = other.GetComponent<BallController>();

        if (ball != null)
        {
            ball.OutOfBounds();
        }
    }
}