using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MathProblemMinigame : Minigame
{
    public override void Begin()
    {
        //Change Scene to ColorConfusion
        SceneManager.LoadScene("Matematicas");
        Debug.Log("Maths Scene - Loaded");
    }

    public override GameState GetGameState()
    {
        return MathProblemGameManager.GameState;
    }

    public override bool IsFinished()
    {
        return MathProblemGameManager.IsFinished;
    }
}
