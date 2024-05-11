using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuickCountGameManager
{
    public static bool IsFinished { get; private set; } = false;
    public static GameState GameState { get; private set; } = GameState.Playing;

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

    public static void Reset()
    {
        GameState = GameState.Playing;
        IsFinished = false;
    }
}
