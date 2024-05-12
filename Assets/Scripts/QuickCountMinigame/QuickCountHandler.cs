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

    private float currentTime;

    private bool start;

    //PROVISIONAL
    private bool fail;

    void Awake()
    {
        QuickCountGameManager.Reset();
    }
    void Start()
    {
        //paintSquares();
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;

        countdown[0].text = countdownTime.ToString("F0");
        countdown[1].text = countdownTime.ToString("F0");

        currentTime += Time.deltaTime;

        if(countdownTime < 0 && !start){

            print("Empezando");
            paintSquares();
            start = true;
            countdown[0].gameObject.SetActive(false);          
            
            texts[0].SetActive(false);
            texts[1].SetActive(false);

            currentTime = 0;
        }  

        if(time < currentTime && !fail)
        {
            resetSquares();
            paintSquares();
            currentTime = 0;
        }

        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if(keyCode.ToString() == "Alpha" + numSquares)
                    {
                        Debug.Log("Acierto");
                        currentTime = 0;
                        resetSquares();
                        paintSquares();

                        QuickCountGameManager.Win();
                    
                    }else{

                        Debug.Log("Derrota");
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

        numSquares = Random.Range(1, 10);   

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


