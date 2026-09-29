using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    playerOne,
    playerTwo
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float movingSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 5f;
    [SerializeField]
    float softHitForce = 2.5f;
    [SerializeField]
    float hardHitForce = 5f;
    [SerializeField]
    private Player player;
    public Player PlayerID
    {
        get { return player; }
    }
    public int horizontalMoveDir = 0; // 1 for right, 0 not moving, -1 for left
    public int ifHit = 0; // 1 for soft, 2 for hard, 0 for no
    public bool ifJump = false;
    [SerializeField]
    private Rigidbody2D ball = null;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleServe();
    }

    private void FixedUpdate()
    {
        Move();
        HitBall();
    }

    private void HandleServe()
    {
        if (RoundSystem.Instance == null)
            return;
        if (!RoundSystem.Instance.WaitingForServe)
            return;
        if (ifHit == 0)
            return;
        if (ifHit != 1)
        {
            ifHit = 0;
            return;
        }
        if (!RoundSystem.Instance.TryResumeRound(player))
        {
            ifHit = 0;
            return;
        }
        ifHit = 0;
    }

    private void Move()
    {
        float horizontalSpeed = movingSpeed * horizontalMoveDir;
        Vector2 newSpeed = rb.velocity;
        newSpeed.x = horizontalSpeed;
        if (ifJump)
        {
            newSpeed.y = jumpSpeed;
            ifJump = false;
        }
        rb.velocity = newSpeed;
    }

    private void HitBall()
    {
        if (ifHit == 0)
            return;
        if (ball == null)
        {
            ifHit = 0;
            return;
        }
        BallController ballController = ball.GetComponent<BallController>();
        if (ballController != null)
        {
            if (!ballController.TryHit(player))
            {
                ifHit = 0;
                return;
            }
        }
        Vector2 hitDir = (ball.transform.position - transform.position).normalized;
        if (ifHit == 1)
        {
            ball.velocity = softHitForce * hitDir;
        }
        else if (ifHit == 2)
        {
            ball.velocity = hardHitForce * hitDir;
        }
        ifHit = 0;
    }

    public void BallEnterHitRange(Rigidbody2D ball)
    {
        this.ball = ball;
    }

    public void BallExitHitRange(Rigidbody2D ball)
    {
        if (this.ball == ball)
        {
            this.ball = null;
        }
    }

    public void ResetPlayer(Vector2 position)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = position;
        rb.position = position;
        horizontalMoveDir = 0;
        ifJump = false;
        ifHit = 0;
        ball = null;
    }
}