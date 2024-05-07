using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    [SerializeField]
    public List<Minigame> availableMinigames;
    private Minigame currentMinigame = null;
    private Queue<Minigame> minigameQueue = new();
    public float delayBetweenMinigames = 10.0f;
    private bool timerStarted = false;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartMinigames();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (currentMinigame != null && !timerStarted)
        {
            StartCoroutine(StartMinigameTimer(delayBetweenMinigames));
        }
    }
    public void StartMinigames()
    {
        Debug.Log("Starting minigames");
        ShuffleMinigames();
        StartNextMinigame();
    }
    public void StartNextMinigame()
    {
        if (minigameQueue.Count == 0)
        {
            Debug.Log("No more minigames to play");
            return;
        }
        currentMinigame = minigameQueue.Dequeue();
        currentMinigame.Begin();
        Debug.Log("Starting minigame: " + currentMinigame);
    }

    public GameManager GetInstance()
    {
        return Instance;
    }

    public void ShuffleMinigames()
    {
        if (availableMinigames == null) return;
        Debug.Log("Shuffling minigames");
        for (int i = 0; i < availableMinigames.Count; i++)
        {
            Minigame temp = availableMinigames[i];
            int randomIndex = Random.Range(i, availableMinigames.Count);
            availableMinigames[i] = availableMinigames[randomIndex];
            availableMinigames[randomIndex] = temp;
        }
        foreach (Minigame minigame in availableMinigames)
        {
            minigameQueue.Enqueue(minigame);
        }
        Debug.Log("Minigames shuffled: " + minigameQueue.Count);
    }

    private IEnumerator StartMinigameTimer(float delay)
    {
        timerStarted = true;
        Debug.Log("Starting minigame timer");
        yield return new WaitForSeconds(delay);

        timerStarted = false;
        StartNextMinigame();
    }
}
