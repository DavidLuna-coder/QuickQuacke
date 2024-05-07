using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimonSaysMinigame : Minigame
{   
    public override void Begin()
    {
        //Change Scene to SimonSays
        SceneManager.LoadScene("JuegoPruebaAle");
        Debug.Log("Simon says minigame started");
    }
}
