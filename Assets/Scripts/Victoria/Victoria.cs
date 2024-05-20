using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    public AudioClip victorySound;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(esperarTexto());

    }

    public IEnumerator esperarTexto()
    {
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(victorySound);
        yield return new WaitForSeconds(3.3f);
        SceneManager.LoadScene("MainMenu");

    }
    
    void Update()
    {
        
    }
}
