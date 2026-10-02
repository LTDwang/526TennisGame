using System.Collections;
using System.Drawing;
using UnityEngine;

public class PlayerSizeBubblePowerUp : MonoBehaviour
{
    [SerializeField] private float growMultiplier = 1f;
    [SerializeField] private float shrinkMultiplier = 0.5f;

    private Vector3 resetSize = new Vector3(0.75f, 0.75f, 1f);

    [SerializeField] PlayerController playerOne;
    [SerializeField] PlayerController playerTwo;

    public void ActivateGrow(Player collector) 
    {
        if (collector == Player.playerOne)
        {
            playerOne.transform.localScale = new Vector3(growMultiplier, growMultiplier, 1f);
        }
        else
        {
            playerTwo.transform.localScale = new Vector3(growMultiplier, growMultiplier, 1f);
        }
    }

    public void ActivateShrink(Player collector) 
    {
        if (collector == Player.playerOne)
        {
            playerTwo.transform.localScale = new Vector3(shrinkMultiplier, shrinkMultiplier, 1f);
        }
        else
        {
            playerOne.transform.localScale = new Vector3(shrinkMultiplier, shrinkMultiplier, 1f);
        }
    }
    public void ResetEffect()
    {
        playerOne.transform.localScale = resetSize;
        playerTwo.transform.localScale = resetSize;
    }
}