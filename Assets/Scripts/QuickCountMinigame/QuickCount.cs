using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickCount : Minigame
{
    public override void Begin()
    {
        //Change Scene to QuickCount
        SceneManager.LoadScene("QuickCountMinigame");
        Debug.Log("Quick Count Minigame Scene - Loaded");
    }

    public override GameState GetGameState()
    {
        return QuickCountGameManager.GameState;
    }

    public override bool IsFinished()
    {
        return QuickCountGameManager.IsFinished;
    }
}
