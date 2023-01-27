using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public bool roundActive = false;
    public static GameManager gameManager;

    private void Awake()
    {
        gameManager = this;
    }

    public void OnPathFinished(PathHandler handler)
    {
        player.OnRoundStart(handler.path[0]);
        roundActive = true;
    }
}
