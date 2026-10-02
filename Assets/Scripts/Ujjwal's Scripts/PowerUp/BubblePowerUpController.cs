using System.Collections;
using UnityEngine;

public class BubblePowerUpController : MonoBehaviour
{
    private enum BubblePowerUpType
    {
        TiltOpponent,
        GrowPlayer,
        ShrinkOpponent
    }

    [SerializeField]
    private float minimumSpawnDelay = 1f;

    [SerializeField]
    private float maximumSpawnDelay = 5f;

    [Header("Bubble Colors")]
    [SerializeField] private Color tiltBubbleColor = Color.cyan;
    [SerializeField] private Color growBubbleColor = Color.green;
    [SerializeField] private Color shrinkBubbleColor = Color.yellow;

    private Collider2D pickupCollider;
    private SpriteRenderer pickupRenderer;
    private BubbleRandomMovement randomMovement;

    private TiltBubblePowerUp tiltPowerUp;
    private PlayerSizeBubblePowerUp sizePowerUp;

    private BubblePowerUpType currentPowerUp = BubblePowerUpType.TiltOpponent;
    private bool isAvailable = true;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider2D>();
        pickupRenderer = GetComponent<SpriteRenderer>();
        randomMovement = GetComponent<BubbleRandomMovement>();
        tiltPowerUp = GetComponent<TiltBubblePowerUp>();
        sizePowerUp = GetComponent<PlayerSizeBubblePowerUp>();
        UpdateBubbleColor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAvailable) return;

        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            if (!other.isTrigger) ActivatePowerUp(player.PlayerID);
            return;
        }

        BallController ball = other.GetComponentInParent<BallController>();
        if (ball != null && ball.GetLastHitPlayer(out Player lastHitPlayer))
        {
            ActivatePowerUp(lastHitPlayer);
        }
    }

    private void ActivatePowerUp(Player collector)
    {
        switch (currentPowerUp)
        {
            case BubblePowerUpType.TiltOpponent:
                tiltPowerUp.Activate(collector);
                break;

            case BubblePowerUpType.GrowPlayer:
                sizePowerUp.ActivateGrow(collector);
                break;

            case BubblePowerUpType.ShrinkOpponent:
                sizePowerUp.ActivateShrink(collector);
                break;
        }

        HidePickup();
        
        if (currentPowerUp == BubblePowerUpType.TiltOpponent)
        {
            currentPowerUp = BubblePowerUpType.GrowPlayer;
        }
        else if (currentPowerUp == BubblePowerUpType.GrowPlayer)
        {
            currentPowerUp = BubblePowerUpType.ShrinkOpponent;
        }
        else
        {
            currentPowerUp = BubblePowerUpType.TiltOpponent;
        }

        StartCoroutine(SpawnAfterRandomDelay());
    }

    private IEnumerator SpawnAfterRandomDelay()
    {
        float delay = Random.Range(minimumSpawnDelay, maximumSpawnDelay);
        if (delay > 0f) yield return new WaitForSeconds(delay);
        ShowPickup();
    }

    public void ResetPowerUp()
    {
        StopAllCoroutines();
        // tiltPowerUp.ResetEffect();
        sizePowerUp.ResetEffect();
        HidePickup();
        StartCoroutine(SpawnAfterRandomDelay());
    }

    private void ShowPickup()
    {
        isAvailable = true;
        UpdateBubbleColor();
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

    private void UpdateBubbleColor()
    {
        switch (currentPowerUp)
        {
            case BubblePowerUpType.GrowPlayer:
                pickupRenderer.color = growBubbleColor;
                break;
            case BubblePowerUpType.ShrinkOpponent:
                pickupRenderer.color = shrinkBubbleColor;
                break;
            default:
                pickupRenderer.color = tiltBubbleColor;
                break;
        }
    }
}
