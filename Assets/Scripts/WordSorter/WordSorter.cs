using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordSorter : MonoBehaviour
{
    private string solution;
    private TextMeshProUGUI lettersDisplayed;
    public TMP_InputField inputField;
    public List<string> sortedWords = new();

    public GameObject PanelAux;

    [SerializeField] Text[] countdown;

    [SerializeField] private float countdownTime;

    [SerializeField] private GameObject[] texts;


    [SerializeField] private AudioClip correctClip;

    [SerializeField] private AudioClip incorrectClip;

    [SerializeField] private AudioClip countdownClip;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private string currentText;

    private float currentTime;
    private bool start;
    private const int POINTS_TO_WIN = 3;
    private int points = 0;
    void Start()
    {
        currentText = countdown[0].text;
        lettersDisplayed = GameObject.Find("MixedWord").GetComponent<TextMeshProUGUI>();

        inputField.onEndEdit.AddListener(SubmitResult);
        StartNextRound();

        Debug.Log("Solution: " + solution + " Letters: " + lettersDisplayed.text);
    }
    void StartNextRound()
    {
        solution = SelectWord();
        inputField.text = string.Empty;
        inputField.ActivateInputField();
        lettersDisplayed.text = ShuffleWord(solution);
    }
    private string ShuffleWord(string word)
    {
        solution = word.ToLower();
        char[] chars = solution.ToCharArray();
        System.Random rng = new();
        int n = chars.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (chars[n], chars[k]) = (chars[k], chars[n]);
        }
        return new string(chars);
    }
    private string SelectWord()
    {
        if (sortedWords.Count == 0) return string.Empty;
        System.Random rng = new();
        solution = sortedWords[rng.Next(sortedWords.Count)];
        return solution;
    }
    public void SubmitResult(string answer)
    {
        if (answer.ToLower() == solution.ToLower())
        {
            Debug.Log("Correct!");
            audioSource.PlayOneShot(correctClip);
            points++;
            if (points == POINTS_TO_WIN)
                WordSorterGameManager.Win();
            else
                StartNextRound();
        }
        else
        {
            Debug.Log("Incorrect!");
            audioSource.PlayOneShot(incorrectClip);
            WordSorterGameManager.Lose();
        }
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;


        if (currentText != countdown[0].text && !start)
        {
            audioSource.PlayOneShot(countdownClip);
            currentText = countdown[0].text;
        }
        countdown[0].text = countdownTime.ToString("F0");
        countdown[1].text = countdownTime.ToString("F0");

        currentTime += Time.deltaTime;

        if (countdownTime < 0 && !start)
        {

            print("Empezando");

            start = true;
            countdown[0].gameObject.SetActive(false);

            texts[0].SetActive(false);
            texts[1].SetActive(false);
            PanelAux.SetActive(false);



            currentTime = 0;
        }


    }
}
