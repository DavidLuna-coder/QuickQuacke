using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickCountHandler : MonoBehaviour
{

    [SerializeField] private List<GameObject> list;
    [SerializeField] private int numSquares;

    [SerializeField] private float time;

    [SerializeField] Text[] countdown;

    [SerializeField] private float countdownTime;

    [SerializeField] private GameObject[] texts;

    [SerializeField] private AudioClip correctClip;

    [SerializeField] private AudioClip incorrectClip;

    [SerializeField] private AudioClip countdownClip;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private string currentText;

    private float currentTime;

    private const int POINTS_TO_WIN = 3;
    private int _correctAnswers = 0;

    private bool start;

    //PROVISIONAL
    private bool fail;

    void Awake()
    {
        QuickCountGameManager.Reset();
    }
    void Start()
    {
        currentText = countdown[0].text;
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;

        if(currentText != countdown[0].text && !start)
        {
            audioSource.PlayOneShot(countdownClip);
            currentText = countdown[0].text;
        }

        countdown[0].text = countdownTime.ToString("F0");
        countdown[1].text = countdownTime.ToString("F0");

        currentTime += Time.deltaTime;

        if(countdownTime < 0 && !start)
        {

            print("Empezando");
            paintSquares();
            start = true;
            countdown[0].gameObject.SetActive(false);          
            
            texts[0].SetActive(false);
            texts[1].SetActive(false);

            currentTime = 0;
        }  

        

        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && keyCode.ToString().StartsWith("Alpha"))
                {
                    if(keyCode.ToString() == "Alpha" + numSquares)
                    {
                        Debug.Log("Acierto");
                        audioSource.PlayOneShot(correctClip);
                        currentTime = 0;
                        resetSquares();
                        paintSquares();
                        _correctAnswers++;
                        if(_correctAnswers == POINTS_TO_WIN)
                        {
                            QuickCountGameManager.Win();
                            return;
                        }
                    
                    }else{

                        Debug.Log("Derrota");
                        audioSource.PlayOneShot(incorrectClip);
                        QuickCountGameManager.Lose();
                        //PROVISIONAL. Objetivo: Llevar al jugador a la escena principal por si quiere seguir intentandolo o directamente Game Over.
                        resetSquares();
                        fail = true;
                    }
                }
            }
        }         
    }

    private void paintSquares()
    {
        List<GameObject> auxList = new List<GameObject>(list);

        numSquares = Random.Range(5, 10);   

        for(int i = 0 ; i < numSquares ; i++)
        {
            int randomSquare = Random.Range(0, auxList.Count);

            auxList[randomSquare].SetActive(true);
            auxList.RemoveAt(randomSquare);
        }
    }

    private void resetSquares()
    {

        for(int i = 0 ; i < list.Count ; i++)
        {

            list[i].SetActive(false);
        }
    }
}


