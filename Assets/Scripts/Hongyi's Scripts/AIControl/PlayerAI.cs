using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private Transform ball;
    [Header("Movement")]
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float horizontalDeadZone = 0.3f;
    [SerializeField]
    private float homeX;
    [Header("Jump")]
    [SerializeField]
    private float jumpHeightThreshold = 1.5f;
    [SerializeField]
    private float jumpHorizontalRange = 2f;
    [Header("Hit")]
    [SerializeField]
    private float hitCooldown = 0.4f;
    [Range(0f, 1f)]
    [SerializeField]
    private float hardHitChance = 0.5f;
    private float nextHitTime = 0f;
    private bool wasBallInHitRange = false;
    private void Awake()
    {
        if (controller == null)
        {
            controller = GetComponent<PlayerController>();
        }
    }
    private void Update()
    {
        if (controller == null || ball == null)
            return;
        HandleServe();
        if (RoundSystem.Instance != null && RoundSystem.Instance.WaitingForServe)
            return;
        HandleMovement();
        HandleJump();
        HandleHit();
    }
    private void HandleServe()
    {
        if (RoundSystem.Instance == null)
            return;

        if (!RoundSystem.Instance.WaitingForServe)
            return;
        controller.ifHit = 1;
    }

    private void HandleMovement()
    {
        float targetX;
        if (ball.position.x >= minX && ball.position.x <= maxX)
        {
            targetX = Mathf.Clamp(ball.position.x, minX, maxX);
        }
        else
        {
            targetX = homeX;
        }
        float difference = targetX - transform.position.x;
        if (Mathf.Abs(difference) <= horizontalDeadZone)
        {
            controller.horizontalMoveDir = 0;
        }
        else if (difference > 0f)
        {
            controller.horizontalMoveDir = 1;
        }
        else
        {
            controller.horizontalMoveDir = -1;
        }
    }

    private void HandleJump()
    {
        float heightDifference = ball.position.y - transform.position.y;
        float horizontalDistance = Mathf.Abs(ball.position.x - transform.position.x);
        if (heightDifference > jumpHeightThreshold &&
            horizontalDistance < jumpHorizontalRange)
        {
            controller.ifJump = true;
        }
    }

    private void HandleHit()
    {
        bool ballInHitRange = controller.BallInHitRange;
        if (ballInHitRange && !wasBallInHitRange && Time.time >= nextHitTime)
        {
            if (Random.value < hardHitChance)
            {
                controller.ifHit = 2;
            }
            else
            {
                controller.ifHit = 1;
            }
            nextHitTime = Time.time + hitCooldown;
        }
        wasBallInHitRange = ballInHitRange;
    }
}