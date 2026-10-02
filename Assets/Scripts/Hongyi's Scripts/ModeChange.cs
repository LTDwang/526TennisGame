using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{
    [SerializeField]
    GameObject playerTwo;
    private void Awake()
    {
        if (ModeChoose.Instance.gameType == GameType.PVE)
        {
            playerTwo.GetComponent<PlayerAI>().enabled = true;
            playerTwo.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            playerTwo.GetComponent<PlayerAI>().enabled = false;
            playerTwo.GetComponent<PlayerInput>().enabled = true;
        }
    }
}
