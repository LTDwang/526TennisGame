using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }

    [SerializeField]
    private TMP_Text scoreTxt;

    [SerializeField]
    private int playerOne = 0;

    [SerializeField]
    private int playerTwo = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateScore();
    }

    public void AddScore(Player player)
    {
        if (player == Player.playerOne)
        {
            playerOne++;
        }
        else
        {
            playerTwo++;
        }

        UpdateScore();
    }

    public void ResetScore()
    {
        playerOne = 0;
        playerTwo = 0;

        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreTxt == null)
        {
            return;
        }

        scoreTxt.text = $"{playerOne} : {playerTwo}";
    }
}