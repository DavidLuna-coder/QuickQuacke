using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class WordSorter : MonoBehaviour
{
    private string solution;
    private TextMeshProUGUI lettersDisplayed;
    private TMP_InputField inputField;
    public List<string> sortedWords = new();

    void Start()
    {
        lettersDisplayed = GameObject.Find("MixedWord").GetComponent<TextMeshProUGUI>();
        inputField = GameObject.Find("Answer").GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(SubmitResult);
        solution = SelectWord();
        lettersDisplayed.text = ShuffleWord(solution);

        Debug.Log("Solution: " + solution + " Letters: " + lettersDisplayed.text);
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
        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }
}
