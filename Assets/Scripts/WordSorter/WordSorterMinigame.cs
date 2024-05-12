using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordSorterMinigame : Minigame
{
    public override void Begin()
    {
        //Change Scene to WordSorter
        SceneManager.LoadScene("WordSorter");
        Debug.Log("Word Sorter Minigame Scene - Loaded");
    }

    public override GameState GetGameState()
    {
        return WordSorterGameManager.GameState;
    }

    public override bool IsFinished()
    {
        return WordSorterGameManager.IsFinished;
    }
}
