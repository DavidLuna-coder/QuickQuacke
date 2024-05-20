using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorConfusionStateManager
{
    public static GameState GameState { get; private set; }
    public static bool IsFinished { get; private set; }

    public static void Win()
    {
        GameState = GameState.Win;
        IsFinished = true;
    }

    public static void Lose()
    {
        GameState = GameState.Lose;
        IsFinished = true;
    }

    public static void ResetGame()
    {
        GameState = GameState.Playing;
        IsFinished = false;
    }
}
