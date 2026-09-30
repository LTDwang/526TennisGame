using UnityEngine;

public class TiltBubblePowerUp : MonoBehaviour
{
    [SerializeField]
    private float tiltAngle = -10f;

    [SerializeField]
    private float tiltHoldDuration = 3f;

    private Collider2D pickupCollider;
    private SpriteRenderer pickupRenderer;
    private bool isAvailable = true;

    [SerializeField] private FieldRotation playerOneField;
    [SerializeField] private FieldRotation playerTwoField;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider2D>();
        pickupRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAvailable) return;

        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            if (!other.isTrigger)
            {
                Activate(player.PlayerID);
            }
            return;
        }

        BallController ball = other.GetComponentInParent<BallController>();

        if (ball != null && ball.GetLastHitPlayer(out Player lastHitPlayer))
        {
            Activate(lastHitPlayer);
        }
    }

    private void Activate(Player collector)
    {
        FieldRotation opponentField = FindOpponentField(collector);

        if (opponentField == null)
        {
            Debug.LogWarning("Tilt Bubble could not find the opponent's field.", this);
            return;
        }

        opponentField.Tilt(tiltAngle, tiltHoldDuration);
        HidePickup();
    }

    private FieldRotation FindOpponentField(Player collector)
    {
        if (collector == Player.playerOne)
        {
            return playerTwoField;
        }
        return playerOneField;
    }

    public void ResetPowerUp()
    {
        HidePickup();
        playerOneField.ResetField();
        playerTwoField.ResetField();
    }

    private void HidePickup()
    {
        isAvailable = false;
        pickupCollider.enabled = false;
        pickupRenderer.enabled = false;
    }
}
