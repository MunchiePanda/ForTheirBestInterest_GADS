using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Day Tracking")]
    public int currentDay = 1;
    public int maxDays = 3;

    [Header("Case Flow")]
    public int currentCaseInDay = 0;
    public int casesPerDay = 3;

    [Header("Game State")]
    public bool gameWon = false;
    public int totalCases = 0;
    public int successfulPlacements = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartDay(currentDay);
    }

    public void StartDay(int day)
    {
        Debug.Log($"--- STARTING DAY {day} ---");
        currentCaseInDay = 0;
        LoadNextCase();
    }

    public void LoadNextCase()
    {
        FindObjectOfType<CaseLoader>().LoadNextCase();
    }

    public void OnCaseCompleted()
    {
        currentCaseInDay++;
        totalCases++;

        if (currentCaseInDay >= casesPerDay)
        {
            ProceedToNextDay();
        }
        else
        {
            LoadNextCase();
        }
    }

    public void ProceedToNextDay()
    {
        currentDay++;
        if (currentDay > maxDays)
            EndGame(DecisionSystem.Instance.currentScore >= 70); // Win if score is 70% or higher
        else
            StartDay(currentDay);
    }

    public void EndGame(bool won)
    {
        gameWon = won;
        Debug.Log($"GAME OVER - {(won ? "VICTORY" : "DEFEAT")}");
        Debug.Log($"Final Score: {DecisionSystem.Instance.currentScore}");
        Debug.Log($"Total Cases: {totalCases}");
        Debug.Log($"Successful Placements: {successfulPlacements}");
        
        // Load end scene
        SceneManager.LoadScene("DemoEND");
    }

    public void RecordSuccessfulPlacement()
    {
        successfulPlacements++;
    }
}
