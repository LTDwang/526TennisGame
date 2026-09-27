using UnityEngine;

public class FieldRotation : MonoBehaviour
{
    [SerializeField]
    private float resetRotation = 0f;
    private float tiltSpeed = 180f;

    private float currentRotation;

    public bool IsTilted { get; private set; }

    private Rigidbody2D rb;
    private float targetRotation;
    private float holdTimer = 5f;
    private bool holding;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Field needs a Rigidbody2D.");
            return;
        }

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

    private void FixedUpdate()
    {
        if (rb == null) return;

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
