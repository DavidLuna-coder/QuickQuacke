using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorConfusionMinigame : Minigame
{
    public override void Begin()
    {
        //Change Scene to ColorConfusion
        SceneManager.LoadScene("ColorConfusion");
        Debug.Log("Color Confusion Minigame Scene - Loaded");
    }

    public override GameState GetGameState()
    {
        return ColorConfusionStateManager.GameState;
    }

    public override bool IsFinished()
    {
        return ColorConfusionStateManager.IsFinished;
    }
}
