using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickCountHandler : MonoBehaviour
{

    [SerializeField] private List<GameObject> list;
    [SerializeField] private int numSquares;

    [SerializeField] private float time;
    private float currentTime;

    //PROVISIONAL
    private bool fail;

    void Start()
    {
        paintSquares();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

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
                    
                    }else{

                        Debug.Log("Derrota");

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

            auxList[randomSquare].GetComponent<Image>().color = Color.red;
            auxList.RemoveAt(randomSquare);
        }
    }

    private void resetSquares()
    {

        for(int i = 0 ; i < list.Count ; i++)
        {

            list[i].GetComponent<Image>().color = Color.white;
        }
    }
}


