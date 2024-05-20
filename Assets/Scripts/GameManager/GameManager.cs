using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    [SerializeField]
    public List<Minigame> availableMinigames;
    private Minigame currentMinigame = null;
    private Queue<Minigame> minigameQueue = new Queue<Minigame>();
    public float delayBetweenMinigames = 10.0f;
    private float timer = 0f;
    private bool timerStarted = false;
    private bool resetTimer = false;
    private bool IsFinished = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (IsFinished) return;

        if (currentMinigame != null && !currentMinigame.IsFinished() && !timerStarted && minigameQueue.Count > 0)
        {
            timerStarted = true;
            timer = delayBetweenMinigames;
        }

        if (timerStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timerStarted = false;
                if (resetTimer)
                {
                    resetTimer = false;
                    return;
                }
                else if (currentMinigame != null && !currentMinigame.IsFinished())
                {
                    StartCoroutine(Lose());
                }
            }
        }

        if (currentMinigame != null && currentMinigame.IsFinished())
        {
            Debug.Log(currentMinigame.GetGameState().ToString());
            if (currentMinigame.GetGameState() == GameState.Win)
            {
                timerStarted = false;
                Debug.Log("Minigame won");
                StartNextMinigame();
            }
            else
            {
                timerStarted = false;
                StartCoroutine(Lose());
                minigameQueue.Clear();
                return;
            }
        }
    }

    public void StartMinigames()
    {
        IsFinished = false;
        ColorConfusionStateManager.ResetGame();
        WordSorterGameManager.Reset();
        CardsGameManager.Reset();
        SimonSaysStateManager.Reset();
        MathProblemGameManager.Reset();
        QuickCountGameManager.Reset();

        minigameQueue.Clear();
        Debug.Log("Starting minigames");
        ShuffleMinigames();
        StartNextMinigame();
    }

    public void StartNextMinigame()
    {
        if (minigameQueue.Count == 0)
        {
            Debug.Log("No more minigames to play");
            SceneManager.LoadScene("Victoria");
            IsFinished = true;
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

    public IEnumerator Lose()
    {
        Debug.Log("Minigame lost");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Derrota");
        IsFinished = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
