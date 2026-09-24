using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController controller;

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

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        controller.horizontalMoveDir = 0;

        if (Input.GetKey(left))
        {
            controller.horizontalMoveDir -= 1;
        }

        if (Input.GetKey(right))
        {
            controller.horizontalMoveDir += 1;
        }

        if (Input.GetKeyDown(jump))
        {
            controller.ifJump = true;
        }

        if (Input.GetKeyDown(softHit))
        {
            controller.ifHit = 1;
        }

        if (Input.GetKeyDown(hardHit))
        {
            controller.ifHit = 2;
        }
    }
}