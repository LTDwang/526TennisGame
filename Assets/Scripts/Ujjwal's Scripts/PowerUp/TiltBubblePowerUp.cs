using UnityEngine;

public class TiltBubblePowerUp : MonoBehaviour
{
    [SerializeField]
    private float tiltAngle = 5f;
    [SerializeField]
    private float tiltHoldDuration = 3f;

    [SerializeField] private FieldRotation playerOneField;
    [SerializeField] private FieldRotation playerTwoField;

    private FieldRotation opponentField;
    private int sign;

    public void Activate(Player collector)
    {
        if (collector == Player.playerOne) {
            opponentField = playerTwoField;
            sign = -1;
        }
        else {
            opponentField = playerOneField;
            sign = 1;
        }

        if (opponentField == null)
        {
            Debug.LogWarning("Tilt Bubble could not find the opponent's field.", this);
            return;
        }

        float signedTiltAngle = tiltAngle * sign;
        opponentField.Tilt(signedTiltAngle, tiltHoldDuration);
    }

    public void ResetEffect()
    {
        playerOneField.ResetField();
        playerTwoField.ResetField();
    }
}
