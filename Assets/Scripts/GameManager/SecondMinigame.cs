using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondMinigame : Minigame
{
    public override void Begin()
    {
        //Change Scene to SecondMinigame
        SceneManager.LoadScene("SecondMinigame");
        Debug.Log("Second minigame started");
    }
}
