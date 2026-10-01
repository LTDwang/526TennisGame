using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BubbleRandomMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 movementAreaCenter = new Vector2(0f, 1f);

    [SerializeField]
    private Vector2 movementAreaSize = new Vector2(16f, 4f);

    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private float directionChangeInterval = 2f;

    private Rigidbody2D rb;
    private Vector2 movementTarget;
    private float directionChangeTimer;
    private bool isMoving = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MoveToTopCenter();
        ChooseMovementTarget();
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            return;
        }

        directionChangeTimer -= Time.fixedDeltaTime;

        if (directionChangeTimer <= 0f ||
            Vector2.Distance(rb.position, movementTarget) < 0.1f)
        {
            ChooseMovementTarget();
        }

        Vector2 nextPosition = Vector2.MoveTowards(
            rb.position,
            movementTarget,
            movementSpeed * Time.fixedDeltaTime);

        rb.MovePosition(nextPosition);
    }

    public void SetMoving(bool shouldMove)
    {
        isMoving = shouldMove;

        if (isMoving)
        {
            MoveToTopCenter();
            ChooseMovementTarget();
        }
    }

    private void ChooseMovementTarget()
    {
        movementTarget = GetRandomPointInMovementArea();
        directionChangeTimer = directionChangeInterval;
    }

    private void MoveToTopCenter()
    {
        rb.position = new Vector2(
            movementAreaCenter.x,
            movementAreaCenter.y + movementAreaSize.y * 0.5f);
    }

    private Vector2 GetRandomPointInMovementArea()
    {
        Vector2 halfSize = movementAreaSize * 0.5f;

        return new Vector2(
            Random.Range(movementAreaCenter.x - halfSize.x, movementAreaCenter.x + halfSize.x),
            Random.Range(movementAreaCenter.y - halfSize.y, movementAreaCenter.y + halfSize.y));
    }
}
