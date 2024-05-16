using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathProblem : MonoBehaviour
{

    public Text firstNumber;
    public Text secondNumber;
    public Text answer1;
    public Text answer2;
    public Text operatorText;

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
    public int currentAnswer;
    public Text rightOrWrongText;
    int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisplayMathProblem(); 
        rightOrWrongText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayMathProblem()
    {
        randomFirstNumber = Random.Range(0, easyMathsList.Count + 1);
        randomSecondNumber = Random.Range(1, easyMathsList.Count + 1);
        randomOperator = Random.Range(0, 3); // dificultad

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
                randomFirstNumber = Random.Range(1, 20);
                randomSecondNumber = Random.Range(1, 20);
                firstNumberInProblem = randomFirstNumber;
                secondNumberInProblem = randomSecondNumber;
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
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.green;
            rightOrWrongText.text = "Correct!";
            Invoke("TurnOffText", 1);
        }
        else
        {
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.red;
            rightOrWrongText.text = "Incorrect!";
            Debug.Log("HAS PERDIDOO ");
        }
      
    }

    public void ButtonAnswer2()
    {
        if (currentAnswer == 1)
        {
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.green;
            rightOrWrongText.text = "Correct!";
            Invoke("TurnOffText", 1);
        }
        else{
            rightOrWrongText.enabled = true;
            rightOrWrongText.color = Color.red;
            rightOrWrongText.text = "Incorrect!";
            Debug.Log("HAS PERDIDOO ");

        }
    
    }
    public void TurnOffText()
    {
        rightOrWrongText.enabled = false;
        DisplayMathProblem();
        points++;
        if(points == 10)
        {
            Debug.Log("HAS GANADO");
        }
    }

}
