using UnityEngine;

public class FieldRotation : MonoBehaviour
{
    [SerializeField]
    private Player fieldOwner;

    public Player FieldOwner
    {
        get { return fieldOwner; }
    }

    private float resetRotation = 0f;
    
    [SerializeField]
    private float tiltSpeed = 60f;

    private float currentRotation;

    public bool IsTilted { get; private set; }

    private float targetRotation;
    private float holdTimer;
    private bool holding;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Field needs a Rigidbody2D.");
            return;
        }
    }

    private void Start() {
        currentRotation = resetRotation;
        rb.MoveRotation(resetRotation);
        IsTilted = false;
    }

    public void Tilt(float angle, float holdDuration)
    {
        targetRotation = angle;
        holdTimer = holdDuration;
        holding = true;
        IsTilted = true;
    }

    public void ResetTilt()
    {
        targetRotation = resetRotation;
        holdTimer = 0f;
        holding = false;
    }

    public void ResetField()
    {
        currentRotation = resetRotation;
        targetRotation = resetRotation;
        holdTimer = 0f;
        holding = false;
        IsTilted = false;
        rb.rotation = resetRotation;
    }

    private void FixedUpdate()
    {
        currentRotation = rb.rotation;
        if (!IsTilted) return;

        if (holding)
        {
            holdTimer -= Time.fixedDeltaTime;
            if (holdTimer <= 0f) ResetTilt();
        }

        currentRotation = Mathf.MoveTowardsAngle(
            currentRotation,
            targetRotation,
            tiltSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(currentRotation);

        if (!holding && Mathf.Abs(Mathf.DeltaAngle(currentRotation, resetRotation)) < 0.01f)
        {
            currentRotation = resetRotation;
            rb.MoveRotation(resetRotation);
            IsTilted = false;
        }
    }
}
