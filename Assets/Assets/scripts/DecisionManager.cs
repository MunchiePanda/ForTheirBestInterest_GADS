using UnityEngine;
using System.Collections;

public class DecisionManager : MonoBehaviour
{
    private bool usedInterview = false;
    private bool usedChecklist = false;
    private bool hasInterviewed = false;

    public static DecisionManager Instance { get; private set; }

    // Add these static variables at the top of the class
    public static string LastChildName;
    public static bool LastOutcomeGood;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Approve()
    {
        var caseData = CaseManager.Instance.currentCase;
        LastChildName = caseData.childProfile.childName;
        LastOutcomeGood = Random.value > 0.5f; // Random outcome for now

        // Load the outcome scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("OutcomeScene");
    }

    public void Reject()
    {
        var caseData = CaseManager.Instance.currentCase;
        DecisionSystem.Instance.RecordDecision(caseData, false, usedInterview, usedChecklist);
        
        // Show rejection feedback
        ShowFeedback(false);
        
        // Reset tools used flags
        usedInterview = false;
        usedChecklist = false;
        hasInterviewed = false;
        
        // Move to next case
        GameManager.Instance.OnCaseCompleted();
    }

    public void OnInterviewCompleted()
    {
        usedInterview = true;
        hasInterviewed = true;
    }

    public void OnChecklistCompleted()
    {
        usedChecklist = true;
    }

    public bool HasInterviewed()
    {
        return hasInterviewed;
    }

    private void ShowFeedback(bool approved)
    {
        // Create feedback GameObject
        GameObject feedbackObj = new GameObject("Feedback");
        feedbackObj.transform.SetParent(transform);
        
        // Add Image component
        UnityEngine.UI.Image feedbackImage = feedbackObj.AddComponent<UnityEngine.UI.Image>();
        
        // Set the feedback image
        feedbackImage.sprite = approved ? 
            Resources.Load<Sprite>("UI/Approved") : 
            Resources.Load<Sprite>("UI/Rejected");
        
        // Set up RectTransform
        RectTransform rectTransform = feedbackObj.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(200, 100);
        rectTransform.anchoredPosition = Vector2.zero;
        
        // Add fade in/out animation
        StartCoroutine(FadeFeedback(feedbackImage));
    }

    private System.Collections.IEnumerator FadeFeedback(UnityEngine.UI.Image feedbackImage)
    {
        // Fade in
        float duration = 1f;
        float elapsed = 0f;
        Color color = feedbackImage.color;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            feedbackImage.color = color;
            yield return null;
        }
        
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        
        // Fade out
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            feedbackImage.color = color;
            yield return null;
        }
        
        // Destroy the feedback object
        Destroy(feedbackImage.gameObject);
    }
}