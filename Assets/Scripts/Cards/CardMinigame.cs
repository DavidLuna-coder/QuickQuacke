using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardMinigame : Minigame
{
    public override void Begin()
    {
        //Change Scene to ColorConfusion
        SceneManager.LoadScene("MemoryCardsInitial");
        Debug.Log("Cards - Loaded");
    }

    public override GameState GetGameState()
    {
        return CardsGameManager.GameState;
    }

    public override bool IsFinished()
    {
        return CardsGameManager.IsFinished;
    }
}
