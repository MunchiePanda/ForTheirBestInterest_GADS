using UnityEngine;
using System.Collections.Generic;

public class DecisionSystem : MonoBehaviour
{
    public static DecisionSystem Instance;

    [Header("Scoring")]
    public int currentScore = 0;
    public int maxScore = 100;
    public int minScore = 0;

    [Header("Decision History")]
    public List<DecisionRecord> decisionHistory = new List<DecisionRecord>();

    [Header("Scoring Parameters")]
    public int baseApprovalScore = 10;
    public int baseRejectionScore = -5;
    public int interviewBonusScore = 15;
    public int checklistBonusScore = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RecordDecision(CaseData caseData, bool approved, bool usedInterview, bool usedChecklist)
    {
        int scoreChange = approved ? baseApprovalScore : baseRejectionScore;
        
        // Add bonuses for using tools
        if (usedInterview) scoreChange += interviewBonusScore;
        if (usedChecklist) scoreChange += checklistBonusScore;

        // Calculate match score impact
        int matchScore = MatchScoreSystem.Instance.CalculateRawMatch(caseData);
        if (approved && matchScore < 2)
        {
            scoreChange -= 10; // Penalty for approving poor matches
        }
        else if (!approved && matchScore >= 2)
        {
            scoreChange += 5; // Bonus for rejecting poor matches
        }

        // Update score
        currentScore = Mathf.Clamp(currentScore + scoreChange, minScore, maxScore);

        // Record decision
        DecisionRecord record = new DecisionRecord
        {
            caseData = caseData,
            approved = approved,
            scoreChange = scoreChange,
            usedInterview = usedInterview,
            usedChecklist = usedChecklist,
            matchScore = matchScore
        };
        decisionHistory.Add(record);

        // Check win/lose conditions
        CheckGameState();
    }

    private void CheckGameState()
    {
        // Check for game over conditions
        if (currentScore <= minScore)
        {
            GameManager.Instance.EndGame(false); // Lost
        }
        else if (currentScore >= maxScore)
        {
            GameManager.Instance.EndGame(true); // Won
        }
    }

    public string GetDecisionFeedback(DecisionRecord record)
    {
        string feedback = "";
        
        if (record.approved)
        {
            if (record.matchScore >= 2)
            {
                feedback = "Good decision! The match was strong and the applicant met the requirements.";
            }
            else
            {
                feedback = "Consider reviewing the match score more carefully next time.";
            }
        }
        else
        {
            if (record.matchScore < 2)
            {
                feedback = "Good call! The match wasn't strong enough.";
            }
            else
            {
                feedback = "The match was actually quite good. Consider being more lenient next time.";
            }
        }

        return feedback;
    }
}

[System.Serializable]
public class DecisionRecord
{
    public CaseData caseData;
    public bool approved;
    public int scoreChange;
    public bool usedInterview;
    public bool usedChecklist;
    public int matchScore;
} 