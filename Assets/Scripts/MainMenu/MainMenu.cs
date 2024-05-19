using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClip;
    //[SerializeField] private AudioClip menuSong;

    [SerializeField] private AudioSource audioSource;


    public void PlayGame()
    {

        StartCoroutine(WaitForSound());

        
    }


    public IEnumerator WaitForSound()
    {
        audioSource.PlayOneShot(buttonClip);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        GameManager.Instance.StartMinigames();
    }
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
