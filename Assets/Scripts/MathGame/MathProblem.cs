using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathProblem : MonoBehaviour
{
    private const int POINTS_TO_WIN = 3;
    public TMP_Text firstNumber;
    public TMP_Text secondNumber;
    public TMP_Text answer1;
    public TMP_Text answer2;
    public TMP_Text operatorText;
    public TMP_Text Instrucciones;

    public List<int> easyMathsList = new List<int> ();
    public int randomFirstNumber;
    public int randomSecondNumber;

    int firstNumberInProblem;
    int secondNumberInProblem;
    char operatorInProblem;
    int randomOperator;
    int answer1InProblem;
    int answer2InProblem;
    int displayRandomAnswer;
    int randomAnswerPlacement;
    int difficultyLevel;
    public int currentAnswer;
    public TMP_Text rightOrWrongText;
    int points = 0;

    public TMP_Text count;
    public TMP_Text title;
    public TMP_Text title2;
    public Image imgBtn1;
    public Image imgBtn2;
    private int mostrar;
    private bool contar;
    private int _correctAnswers = 0;
    [SerializeField] private AudioClip correctClip;

    [SerializeField] private AudioClip incorrectClip;

    [SerializeField] private AudioClip countdownClip;

    [SerializeField] private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = Random.Range(0, 3); // dificultad. 3 niveles.
        Debug.Log("Dificultad: " + difficultyLevel);
        DisplayMathProblem(difficultyLevel); 
        contar = true;
        mostrar = 3;
        firstNumber.enabled = false;
        secondNumber.enabled = false;
        answer1.enabled = false;
        answer2.enabled = false;
        operatorText.enabled = false;
        rightOrWrongText.enabled = false;
        imgBtn1.enabled = false;
        imgBtn2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        if(contar == true)
        {
            switch(mostrar)
            {
                case 0: Empezar(); break;
                case 1: audioSource.PlayOneShot(countdownClip); count.text = "1"; StartCoroutine(Esperar()); break;
                case 2: audioSource.PlayOneShot(countdownClip); count.text = "2"; StartCoroutine(Esperar());break;
                case 3: audioSource.PlayOneShot(countdownClip); count.text = "3"; StartCoroutine(Esperar());break;
                default : break;
            }
            
            contar = false;
            mostrar--;
        }
  
    }

    void Empezar()
    {
        count.enabled = false;
        Instrucciones.enabled = false;
        title.enabled = false;
        title2.enabled = false;
        firstNumber.enabled = true;
        secondNumber.enabled = true;
        answer1.enabled = true;
        answer2.enabled = true;
        operatorText.enabled = true;
        imgBtn1.enabled = true;
        imgBtn2.enabled = true;
        
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1);
        contar  = true;
    }

    public void DisplayMathProblem(int difficultyLevel)
    {
        randomFirstNumber = Random.Range(0, easyMathsList.Count + 1);
        randomSecondNumber = Random.Range(1, easyMathsList.Count + 1);
        randomOperator = Random.Range(0, 3); // operadores
        
        firstNumberInProblem = randomFirstNumber;
        secondNumberInProblem = randomSecondNumber;

        switch(randomOperator)
        {
            case 0:
                operatorInProblem = '+';
                answer1InProblem = firstNumberInProblem + secondNumberInProblem;
                break;
            case 1:
                operatorInProblem = '-';
                answer1InProblem = firstNumberInProblem - secondNumberInProblem;
                break;
            case 2:
            if(difficultyLevel == 0)
            {
                randomFirstNumber = Random.Range(1, 10);
                randomSecondNumber = Random.Range(1, 10);
                firstNumberInProblem = randomFirstNumber;
                secondNumberInProblem = randomSecondNumber;
            }
            else if (difficultyLevel == 1)
            {
                randomFirstNumber = Random.Range(1, 15);
                randomSecondNumber = Random.Range(1, 10);
                firstNumberInProblem = randomFirstNumber;
                secondNumberInProblem = randomSecondNumber;
            }
            else if (difficultyLevel == 2)
            {
                randomFirstNumber = Random.Range(1, 20);
                randomSecondNumber = Random.Range(1, 20);
                firstNumberInProblem = randomFirstNumber;
                secondNumberInProblem = randomSecondNumber;
            }
                operatorInProblem = '*';
                answer1InProblem = firstNumberInProblem * secondNumberInProblem;
                break;
            default: break;
        }

        
        displayRandomAnswer = Random.Range(0,2);

        if(displayRandomAnswer == 0)
        {
            answer2InProblem = answer1InProblem + Random.Range(1,5);
        }
        else
        {
            answer2InProblem = answer1InProblem - Random.Range(1,5);
        }

        firstNumber.text = "" + firstNumberInProblem;
        secondNumber.text = "" + secondNumberInProblem;
        operatorText.text = "" + operatorInProblem;
        randomAnswerPlacement = Random.Range(0,2);
        if(randomAnswerPlacement == 0)
        {
            answer1.text = "" + answer1InProblem;
            answer2.text = "" + answer2InProblem;
            currentAnswer = 0;
        }
        else
        {
            answer1.text = "" + answer2InProblem;
            answer2.text = "" + answer1InProblem;
            currentAnswer = 1;
        }


    }

    public void ButtonAnswer1()
    {
        if (currentAnswer == 0)
        {
            audioSource.PlayOneShot(correctClip);
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.magenta;
            rightOrWrongText.text = "Puedes continuar";
            _correctAnswers++;
            if(_correctAnswers == POINTS_TO_WIN)
            {
                Debug.Log("HAS GANADO");
                MathProblemGameManager.Win();
                return;
            }
            Invoke(nameof(TurnOffText), 1);
        }
        else
        {
            audioSource.PlayOneShot(incorrectClip);
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.black;
            rightOrWrongText.text = "Se acabo tu prueba";
            MathProblemGameManager.Lose();
            Debug.Log("HAS PERDIDOO ");
        }
      
    }

    public void ButtonAnswer2()
    {
        if (currentAnswer == 1)
        {
            audioSource.PlayOneShot(correctClip);
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.magenta;
            rightOrWrongText.text = "Puedes continuar";
            _correctAnswers++;
            if(_correctAnswers == POINTS_TO_WIN)
            {
                Debug.Log("HAS GANADO");
                MathProblemGameManager.Win();
                return;
            }
            Invoke("TurnOffText", 1);
        }
        else{
            audioSource.PlayOneShot(incorrectClip);
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.black;
            rightOrWrongText.text = "Se acabo tu prueba!";
            MathProblemGameManager.Lose();
            Debug.Log("HAS PERDIDOO ");

        }
    
    }
    public void TurnOffText()
    {
        rightOrWrongText.enabled = false;
        DisplayMathProblem(difficultyLevel);
        points++;
        if(points == 10)
        {
            Debug.Log("HAS GANADO");
        }
    }

}
