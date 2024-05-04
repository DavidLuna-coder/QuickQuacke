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

    public CanvasGroup buttons;
    public GameObject startButton;


    void Awake()
    {
        buttonColors.Add(new List<Color32>{new Color32(255, 100, 100,255),new Color32(255, 0, 0,255)});//red
        buttonColors.Add(new List<Color32>{new Color32(255, 187, 109,255),new Color32(255, 136, 0,255)});//orange
        buttonColors.Add(new List<Color32>{new Color32(162, 255, 124,255),new Color32(72, 248, 0,255)});//green
        buttonColors.Add(new List<Color32>{new Color32(57, 111, 255,255),new Color32(0, 70, 255,255)});//blue
        
        for(int i=0; i<4;i++)
        {
            clickableButton[i].GetComponent<Image>().color=buttonColors[i][0];
        }
        
        /*clickableButton[0].GetComponent<Image>().color=buttonColors[0][0];
        clickableButton[1].GetComponent<Image>().color=buttonColors[1][0];
        clickableButton[2].GetComponent<Image>().color=buttonColors[2][0];
        clickableButton[3].GetComponent<Image>().color=buttonColors[3][0];*/
        
        
        PlayerTaskList=new List<int>();
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
                
                return;
            }
        }

        if(PlayerTaskList.Count==PlayerSequenceList.Count)
        {
            Debug.Log("NextRound");
            StartCoroutine(StartNextRound());
        }
    }
    public void StartGame()
    {
        StartCoroutine (StartNextRound());
        startButton.SetActive(false);
    }

    public IEnumerator PlayerLost()
    {
        audioSource.PlayOneShot(loseSound);
        PlayerTaskList.Clear();
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);
    }

    public IEnumerator StartNextRound()
    {
        PlayerSequenceList.Clear();
        buttons.interactable=false;
        yield return new WaitForSeconds(1f);
        PlayerTaskList.Add(Random.Range(0,4));
        foreach(int index in PlayerTaskList)
        {
            yield return StartCoroutine(HighlightButton(index));
        }
        buttons.interactable=true;
        yield return null;
    }

    public IEnumerator HighlightButton(int buttonId)
    {
        clickableButton[buttonId].GetComponent<Image>().color=buttonColors[buttonId][1];
        audioSource.PlayOneShot(ButtonAudioClipList[buttonId]);
        yield return new WaitForSeconds(0.5f);
        clickableButton[buttonId].GetComponent<Image>().color=buttonColors[buttonId][0];
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
