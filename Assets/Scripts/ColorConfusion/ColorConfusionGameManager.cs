using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorConfusionGameManager : MonoBehaviour
{
    public Button wordText; // Referencia al TextMeshProUGUI donde se mostrará la palabra
    public List<Button> answersButtons;
    private Color _correctColor;
    private Color[] colors = new Color[] { Color.yellow, Color.green, Color.red, Color.cyan, Color.magenta, Color.white }; // Array de colores disponibles
    private string[] words = new string[] { "Amarillo", "Verde", "Rojo", "Azul", "Magenta", "Blanco" }; // Array de palabras disponibles
    private Dictionary<string, Color> _wordColorDictionary = new Dictionary<string, Color>();

    [SerializeField] Text[] countdown;

    [SerializeField] private float countdownTime;

    [SerializeField] private string currentText;

    [SerializeField] private GameObject[] texts;

    [SerializeField] private GameObject[] vectorButtons;

    [SerializeField] private AudioClip correctClip;

    [SerializeField] private AudioClip incorrectClip;

    [SerializeField] private AudioClip countdownClip;

    [SerializeField] private AudioSource audioSource;

    private bool start;


    private void Start()
    {

        currentText = countdown[0].text;

        _wordColorDictionary = new Dictionary<string, Color>(){
            {words[0], colors[0]},
            {words[1], colors[1]},
            {words[2], colors[2]},
            {words[3], colors[3]},
            {words[4], colors[4]},
            {words[5], colors[5]}
        };
        wordText.interactable = false;
        // Actualizar la palabra y el color
        foreach (Button button in answersButtons)
        {
            button.onClick.AddListener(() => CheckAnswer(button));
        }

        UpdateWord();
        ChangeButtonsColor();
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

        if(countdownTime < 0 && !start)
        {
            countdown[0].gameObject.SetActive(false);
            countdown[1].gameObject.SetActive(false);
            start = true;

            texts[0].SetActive(false);
            texts[1].SetActive(false);

            for(int i = 0 ; i < vectorButtons.Length ; i++)
            {
                vectorButtons[i].SetActive(true);
            }
        }
    }


    private void UpdateWord()
    {
        // Seleccionar una palabra y un color aleatorio
        string randomWord = words[Random.Range(0, words.Length)];
        Color randomColor = colors[Random.Range(0, colors.Length)];

        // Mostrar la palabra y establecer el color del texto
        wordText.colors = new ColorBlock() { normalColor = randomColor, disabledColor = randomColor, highlightedColor = randomColor, pressedColor = randomColor, selectedColor = randomColor, colorMultiplier = 1 };
        _correctColor = _wordColorDictionary[randomWord];
        wordText.GetComponentInChildren<TextMeshProUGUI>().text = randomWord;
    }

    private void ChangeButtonsColor()
    {
        // Cambiar el color de los botones de respuesta
        List<Color> colorsCopy = new List<Color>(colors);
        List<string> wordsCopy = new List<string>(words);

        foreach (Button button in answersButtons)
        {
            int colorIndex = Random.Range(0, colorsCopy.Count);
            Color randomColor = colorsCopy[colorIndex];
            colorsCopy.RemoveAt(colorIndex);

            int wordIndex = Random.Range(0, wordsCopy.Count);
            string randomWord = wordsCopy[wordIndex];
            wordsCopy.RemoveAt(wordIndex);
            
            button.colors = new ColorBlock() { normalColor = randomColor, disabledColor = randomColor, highlightedColor = randomColor, pressedColor = randomColor, selectedColor = randomColor, colorMultiplier = 1 };
            button.GetComponentInChildren<TextMeshProUGUI>().text = randomWord;
        }
    }

    public void CheckAnswer(Button button)
    {
        // Comprobar si la respuesta es correcta
        if (button.colors.normalColor == _correctColor)
        {
            Debug.Log("Respuesta correcta");
            ColorConfusionStateManager.Win();
            audioSource.PlayOneShot(correctClip);
            UpdateWord();
            ChangeButtonsColor();
        }
        else
        {
            ColorConfusionStateManager.Lose();
            audioSource.PlayOneShot(incorrectClip);
            Debug.Log("Respuesta incorrecta");
        }
    }
}
