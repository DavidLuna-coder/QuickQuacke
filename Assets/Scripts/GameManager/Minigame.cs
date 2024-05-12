using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public abstract void Begin();
    public abstract bool IsFinished();
    public abstract GameState GetGameState();
}

public enum GameState{
    Playing,
    Win,
    Lose
}
