using UnityEngine;

public class TiltBubblePowerUp : MonoBehaviour
{
    [SerializeField]
    private float tiltAngle = 5f;
    [SerializeField]
    private float tiltHoldDuration = 3f;
    public float TiltHoldDuration { get { return tiltHoldDuration; } }

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
        //HidePickup();
        
        TileResetTimer resetTimer = TileResetTimer.Instance;
        if (resetTimer == null) return;

        if (collector == Player.playerOne)
        {
            resetTimer.SetTimerTwo(tiltHoldDuration);
        }
        else
        {
            resetTimer.SetTimerOne(tiltHoldDuration);
        }
        //StartCoroutine(SpawnAfterRandomDelay());
    }

    public void ResetEffect()
    {
        if (playerOneField != null) playerOneField.ResetField();
        if (playerTwoField != null) playerTwoField.ResetField();
    }
}
