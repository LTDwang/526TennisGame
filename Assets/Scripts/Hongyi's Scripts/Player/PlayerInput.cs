using UnityEngine;

public enum MouseHitButton
{
    Left = 0,
    Right = 1,
    Middle = 2
}

public class PlayerInput : MonoBehaviour
{
    private PlayerController controller;

    [Header("Primary Controls")]
    [SerializeField]
    KeyCode left = KeyCode.A;

    [SerializeField]
    KeyCode right = KeyCode.D;

    [SerializeField]
    KeyCode jump = KeyCode.W;

    [SerializeField]
    KeyCode softHit = KeyCode.LeftShift;

    [SerializeField]
    KeyCode hardHit = KeyCode.LeftControl;

    [Header("Single Player Alternate Movement")]
    [SerializeField]
    private KeyCode alternateLeft = KeyCode.LeftArrow;

    [SerializeField]
    private KeyCode alternateRight = KeyCode.RightArrow;

    [SerializeField]
    private KeyCode alternateJump = KeyCode.UpArrow;

    [Header("Single Player Hit Controls")]
    [SerializeField]
    private KeyCode singlePlayerSoftHit = KeyCode.Space;

    [SerializeField]
    private KeyCode singlePlayerHardHit = KeyCode.C;

    [Header("Mouse Hit Controls")]
    [SerializeField]
    private bool enableMouseHits = true;

    [SerializeField]
    private MouseHitButton softHitMouseButton = MouseHitButton.Left;

    [SerializeField]
    private MouseHitButton hardHitMouseButton = MouseHitButton.Right;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        bool isSinglePlayerHuman = (controller.PlayerID == Player.playerOne) &&
            (ModeChoose.Instance != null) && (ModeChoose.Instance.gameType == GameType.PVE);

        controller.horizontalMoveDir = 0;

        if (Input.GetKey(left) || (isSinglePlayerHuman && Input.GetKey(alternateLeft)))
        {
            controller.horizontalMoveDir -= 1;
        }

        if (Input.GetKey(right) || (isSinglePlayerHuman && Input.GetKey(alternateRight)))
        {
            controller.horizontalMoveDir += 1;
        }

        if (Input.GetKeyDown(jump) || (isSinglePlayerHuman && Input.GetKeyDown(alternateJump)))
        {
            controller.ifJump = true;
        }

        bool useMouseHits = enableMouseHits && (controller.PlayerID == Player.playerTwo || isSinglePlayerHuman);

        bool softHitPressed = Input.GetKeyDown(softHit) ||
                              (isSinglePlayerHuman && Input.GetKeyDown(singlePlayerSoftHit));

        bool hardHitPressed = Input.GetKeyDown(hardHit) ||
                              (isSinglePlayerHuman && Input.GetKeyDown(singlePlayerHardHit));

        if (useMouseHits)
        {
            softHitPressed |= Input.GetMouseButtonDown((int)softHitMouseButton);
            hardHitPressed |= Input.GetMouseButtonDown((int)hardHitMouseButton);
        }

        if (softHitPressed)
        {
            controller.ifHit = 1;
        }

        if (hardHitPressed)
        {
            controller.ifHit = 2;
        }
    }
}
