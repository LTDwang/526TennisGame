using UnityEngine;

public class BallController : MonoBehaviour
{
    private Player lastHitPlayer;
    private bool hasLastHitPlayer = false;
    private bool roundEnded = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool TryHit(Player player)
    {
        if (roundEnded)
            return false;
        if (hasLastHitPlayer && lastHitPlayer == player)
        {
            GiveScoreToOpponent(player);
            return false;
        }
        lastHitPlayer = player;
        hasLastHitPlayer = true;
        return true;
    }

    public void HitGround(Player groundOwner)
    {
        if (roundEnded)
            return;
        GiveScoreToOpponent(groundOwner);
    }

    public void HitNet()
    {
        if (roundEnded)
            return;
        if (hasLastHitPlayer)
        {
            GiveScoreToOpponent(lastHitPlayer);
        }
    }

    public void OutOfBounds()
    {
        if (roundEnded)
            return;
        if (hasLastHitPlayer)
        {
            GiveScoreToOpponent(lastHitPlayer);
        }
    }

    private void GiveScoreToOpponent(Player player)
    {
        if (player == Player.playerOne)
        {
            EndRound(Player.playerTwo);
        }
        else
        {
            EndRound(Player.playerOne);
        }
    }
    private void EndRound(Player winner)
    {
        if (roundEnded)
            return;

        roundEnded = true;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        RoundSystem.Instance.EndRound(winner);
    }
    public void ResetBall()
    {
        hasLastHitPlayer = false;
        roundEnded = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}