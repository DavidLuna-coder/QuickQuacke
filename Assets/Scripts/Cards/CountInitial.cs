using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountInitial : MonoBehaviour
{
        public TMP_Text count;
        private int mostrar;
        private bool contar;
        private CardGroup cardGroup;
        [SerializeField] public AudioClip countdownClip;
        [SerializeField] public AudioSource audioSource;

        void Start()
        {
            cardGroup = new CardGroup();
            contar = true;
            mostrar = 3;
        }
     void FixedUpdate()
    {
        
        if(contar == true)
        {
            switch(mostrar)
            {
                case 0: StartCoroutine(Empezar()); break;
                case 1: audioSource.PlayOneShot(countdownClip); count.text = "1"; StartCoroutine(Esperar());break;
                case 2: audioSource.PlayOneShot(countdownClip); count.text = "2"; StartCoroutine(Esperar());break;
                case 3: audioSource.PlayOneShot(countdownClip); count.text = "3"; StartCoroutine(Esperar());break;
                default : break;
            }
            
            contar = false;
            mostrar--;
        }
  
    }
    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1);
        contar  = true;
    }
    IEnumerator Empezar()
    {
        var a = SceneManager.LoadSceneAsync("MemoryCards");
        while(!a.isDone)
        {
            yield return null;
        }
    }

  
}
