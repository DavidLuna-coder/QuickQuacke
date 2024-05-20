using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplainerSimon : MonoBehaviour
{
    public  List<int> PlayerTaskList = new List<int>();
    public  List<int> PlayerSequenceList = new List<int>();

    public List<AudioClip> ButtonAudioClipList = new List<AudioClip>();
    public List<List<Color32>> buttonColors=new List<List<Color32>>();
    public List<Button> clickableButton;

    public AudioClip loseSound;
    public AudioSource audioSource;

    [SerializeField] private AudioClip correctClip;

    [SerializeField] private AudioClip countdownClip;


    [SerializeField] private string currentText;

    public CanvasGroup buttons;
    public GameObject startButton;
    public GameObject PanelAux;

    [SerializeField] Text[] countdown;

    [SerializeField] private float countdownTime;

    [SerializeField] private GameObject[] texts;

    private float currentTime;
    private bool start;


    void Awake()
    {
        buttonColors.Add(new List<Color32>{new Color32(255, 100, 100,255),new Color32(255, 255, 255,255)});//red
        buttonColors.Add(new List<Color32>{new Color32(255, 187, 109,255),new Color32(255, 255, 255,255)});//orange
        buttonColors.Add(new List<Color32>{new Color32(162, 255, 124,255),new Color32(255, 255, 255,255)});//green
        buttonColors.Add(new List<Color32>{new Color32(57, 111, 255,255),new Color32(255, 255, 255,255)});//blue
        
        for(int i=0; i<4;i++)
        {
            clickableButton[i].GetComponent<Image>().color=buttonColors[i][0];
        }
        
        /*clickableButton[0].GetComponent<Image>().color=buttonColors[0][0];
        clickableButton[1].GetComponent<Image>().color=buttonColors[1][0];
        clickableButton[2].GetComponent<Image>().color=buttonColors[2][0];
        clickableButton[3].GetComponent<Image>().color=buttonColors[3][0];*/
        
        
        PlayerTaskList=new List<int>();
        SimonSaysStateManager.Reset();
    }


    public void AddToPlayerSequenceList(int buttonId)
    {
        PlayerSequenceList.Add(buttonId);
        StartCoroutine(HighlightButton(buttonId));
        for(int i=0;i<PlayerSequenceList.Count;i++)
        {
            if(PlayerTaskList[i]!=PlayerSequenceList[i])
            {
                StartCoroutine(PlayerLost());
                Debug.Log("lost");
                SimonSaysStateManager.Lose();
                return;
            }
        }

        if(PlayerTaskList.Count==PlayerSequenceList.Count)
        {
            Debug.Log("Victory");
            audioSource.PlayOneShot(correctClip);
            PlayerTaskList.Clear();
            buttons.interactable=false;
            SimonSaysStateManager.Win();
            //en lugar de volver a empezar que se bloqueen los botones por ejemplo y el usuario espere al paso a siguiente juego
            //StartCoroutine(StartNextRound());
        }
    }
    public void StartGame()
    {
        
        StartCoroutine (StartNextRound());
        startButton.SetActive(false);
        
    }

    public IEnumerator PlayerLost()
    {
        buttons.interactable=false;
        audioSource.PlayOneShot(loseSound);
        PlayerTaskList.Clear();
        yield return new WaitForSeconds(2f);
        //esto siguiente es para repetir asi que nada se quitaría y se haría lo que tocara cuando pierdes
        /*startButton.SetActive(true);
        buttons.interactable=true;*/
    }

    public IEnumerator StartNextRound()
    {
        
        PlayerSequenceList.Clear();
        buttons.interactable=false;
        yield return new WaitForSeconds(0.5f);
        PlayerTaskList.Add(Random.Range(0,4));
        PlayerTaskList.Add(Random.Range(0,4));
        PlayerTaskList.Add(Random.Range(0,4));
        PlayerTaskList.Add(Random.Range(0,4));
        //PlayerTaskList.Add(Random.Range(0,4));
        foreach(int index in PlayerTaskList)
        {
            yield return StartCoroutine(HighlightButton(index));
            yield return new WaitForSeconds(0.3f);
        }
        buttons.interactable=true;
        yield return null;
    }

    public IEnumerator HighlightButton(int buttonId)
    {
        clickableButton[buttonId].GetComponent<Image>().color=buttonColors[buttonId][1];
        audioSource.PlayOneShot(ButtonAudioClipList[buttonId]);
        yield return new WaitForSeconds(0.3f);
        clickableButton[buttonId].GetComponent<Image>().color=buttonColors[buttonId][0];
    }

    void Start()
    {
        startButton.SetActive(false);
        //StartGame();
    }

    // Update is called once per frame
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

         if(countdownTime < 0 && !start){

            print("Empezando");
            StartGame();
            start = true;
            countdown[0].gameObject.SetActive(false);          
            
            texts[0].SetActive(false);
            texts[1].SetActive(false);
            PanelAux.SetActive(false);
            


            currentTime = 0;
        }  


    }
}
