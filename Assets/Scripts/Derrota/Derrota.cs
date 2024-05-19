using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Derrota : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private TextMeshProUGUI fadeInText1;
    [SerializeField] private TextMeshProUGUI fadeInText2;

    [SerializeField]private  float alphaValue;
   
    [SerializeField]private float fadeAwayPerSecond;

    public AudioClip loseSound;
    public AudioSource audioSource;

    private bool StartText;
    void Start()
    {
        fadeAwayPerSecond=1/fadeTime;
        alphaValue=fadeInText1.color.a;
        StartText=false;
        

        StartCoroutine(MostrarTexto());
        
        
    }

    public  IEnumerator MostrarTexto()
    {
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(loseSound);
        yield return new WaitForSeconds(0.75f);
        StartText=true;
        
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
        
    }

    void Update()
    {
        if(StartText)
        {
            if(fadeTime>0)
            {
                alphaValue+= fadeAwayPerSecond * Time.deltaTime;
                fadeInText1.color=new Color(fadeInText1.color.r,fadeInText1.color.g, fadeInText1.color.b, alphaValue);
                fadeInText2.color=new Color(fadeInText2.color.r,fadeInText2.color.g, fadeInText2.color.b, alphaValue);
                fadeTime-=Time.deltaTime;
            }
        }
        
    }
}
