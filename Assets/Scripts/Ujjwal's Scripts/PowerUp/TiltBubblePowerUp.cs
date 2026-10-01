using System.Collections;
using UnityEngine;

public class TiltBubblePowerUp : MonoBehaviour
{
    [SerializeField]
    private float tiltAngle = -10f;

    [SerializeField]
    private float tiltHoldDuration = 3f;

    [SerializeField]
    private float minimumSpawnDelay = 1f;

    [SerializeField]
    private float maximumSpawnDelay = 5f;

    private Collider2D pickupCollider;
    private SpriteRenderer pickupRenderer;
    private BubbleRandomMovement randomMovement;
    private bool isAvailable = true;

    [SerializeField] private FieldRotation playerOneField;
    [SerializeField] private FieldRotation playerTwoField;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider2D>();
        pickupRenderer = GetComponent<SpriteRenderer>();
        randomMovement = GetComponent<BubbleRandomMovement>();
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
        StartCoroutine(SpawnAfterRandomDelay());
    }

    private FieldRotation FindOpponentField(Player collector)
    {
        if (collector == Player.playerOne)
        {
            return playerTwoField;
        }
        return playerOneField;
    }

    private IEnumerator SpawnAfterRandomDelay()
    {
        float delay = Random.Range(minimumSpawnDelay, maximumSpawnDelay);

        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        ResetPickup();
    }

    public void ResetPowerUp()
    {
        StopAllCoroutines();
        HidePickup();
        StartCoroutine(SpawnAfterRandomDelay());

        playerOneField.ResetField();
        playerTwoField.ResetField();
    }

    public void ResetPickup()
    {
        isAvailable = true;
        randomMovement.SetMoving(true);
        pickupCollider.enabled = true;
        pickupRenderer.enabled = true;
    }

    private void HidePickup()
    {
        isAvailable = false;
        randomMovement.SetMoving(false);
        pickupCollider.enabled = false;
        pickupRenderer.enabled = false;
    }
}
