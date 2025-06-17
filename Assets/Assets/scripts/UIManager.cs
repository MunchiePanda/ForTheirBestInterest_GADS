using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Applicant Info")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI notesText;
    public TextMeshProUGUI applicantAgeText;
    public TextMeshProUGUI maritalStatusText;
    public TextMeshProUGUI expensesText;
    public TextMeshProUGUI backgroundCheckText;
    
    [Header("Applicant Images")]
    public Image applicantImage1; // First applicant's image
    public Image applicantImage2; // Second applicant's image
    public Image applicantImage3; // Third applicant's image

    [Header("Child Info")]
    public TextMeshProUGUI childNameText;
    public TextMeshProUGUI childAgeText;
    public TextMeshProUGUI childLanguageText;
    public TextMeshProUGUI childCultureText;
    public TextMeshProUGUI childNeedsText;
    public TextMeshProUGUI childTraitsText;

    [Header("Match Info")]
    public TextMeshProUGUI matchScoreText;

    [Header("UI Panels")]
    public GameObject checklistPanel;
    private bool checklistVisible = false;

    public GameObject childDocument;
    private bool childDocumentVisible = false;

    [Header("Document Read Confirmation")]
    public Button confirmReadButton;
    public ScrollRect documentScrollView;
    private bool hasReadDocuments = false;
    private bool hasReadChildDocument = false;

    [Header("Interview UI")]
    public GameObject interviewPanel;
    public GameObject gameplayUIGroup; // group containing approve/reject/checklist buttons
    public TMP_Text questionText;
    public TMP_Text responseText;
    public Button question1Button;
    public Button question2Button;
    public Button question3Button;
    public Button finishInterviewButton;

    [HideInInspector]
    public InterviewEntry[] currentInterview;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        checklistPanel.SetActive(checklistVisible);
        childDocument.SetActive(childDocumentVisible);
        interviewPanel.SetActive(false);
        
        // Add listener to confirm read button
        if (confirmReadButton != null)
        {
            confirmReadButton.onClick.AddListener(OnConfirmReadClicked);
        }
    }

    public void OnConfirmReadClicked()
    {
        // Check which document is currently visible
        if (childDocument.activeSelf)
        {
            hasReadChildDocument = true;
            FindObjectOfType<ChecklistSystem>().OnChildDocumentChecked();
        }
        else
        {
            hasReadDocuments = true;
            FindObjectOfType<ChecklistSystem>().OnDocumentsChecked();
        }
    }

    public void DisplayCase(CaseData data)
    {
        // Applicant
        nameText.text = $"Name: {data.applicantName}";
        incomeText.text = $"Income: R{data.income}";
        notesText.text = $"Notes: {data.notes}";
        applicantAgeText.text = $"Age: {data.applicantAge}";
        maritalStatusText.text = $"Marital Status: {data.maritalStatus}";
        expensesText.text = $"Monthly Expenses: R{data.monthlyExpenses}";
        backgroundCheckText.text = $"Background: {data.backgroundCheckNotes}";

        // Handle applicant images - show only the one matching the current applicant
        if (applicantImage1 != null) applicantImage1.gameObject.SetActive(data.applicantName == "Applicant 1");
        if (applicantImage2 != null) applicantImage2.gameObject.SetActive(data.applicantName == "Applicant 2");
        if (applicantImage3 != null) applicantImage3.gameObject.SetActive(data.applicantName == "Applicant 3");

        // Child
        var child = data.childProfile;
        childNameText.text = $"Child: {child.childName}";
        childAgeText.text = $"Age: {child.childAge}";
        childLanguageText.text = $"Language: {child.language}";
        childCultureText.text = $"Culture: {child.culture}";
        childNeedsText.text = $"Needs: {string.Join(", ", child.needs)}";
        childTraitsText.text = $"Traits: {string.Join(", ", child.idealTraits)}";
    }

    public void DisplayMatchScore(int score)
    {
        matchScoreText.text = $"Match Score: {score}/3";
    }

    public void ToggleChecklistPanel()
    {
        checklistVisible = !checklistVisible;
        checklistPanel.SetActive(checklistVisible);
    }

    public void ToggleChildDocumentPanel()
    {
        childDocumentVisible = !childDocumentVisible;
        childDocument.SetActive(childDocumentVisible);
        
        // If we're closing the child document, notify ChecklistSystem
        if (!childDocumentVisible)
        {
            FindObjectOfType<ChecklistSystem>().OnChildDocumentChecked();
        }
    }

    public void StartInterview(InterviewEntry[] entries)
    {
        gameplayUIGroup.SetActive(false);
        interviewPanel.SetActive(true);

        currentInterview = entries;

        ShowInterviewQuestions();
    }

    private void ShowInterviewQuestions()
    {
        question1Button.gameObject.SetActive(true);
        question2Button.gameObject.SetActive(true);
        question3Button.gameObject.SetActive(true);

        question1Button.GetComponentInChildren<TMP_Text>().text = currentInterview[0].question;
        question2Button.GetComponentInChildren<TMP_Text>().text = currentInterview[1].question;
        question3Button.GetComponentInChildren<TMP_Text>().text = currentInterview[2].question;
    }

    public void SelectInterviewQuestion(int index)
    {
        if (index < 0 || index >= currentInterview.Length)
        {
            Debug.LogError($"Invalid question index: {index}");
            return;
        }

        Debug.Log($"Selected question {index + 1}: {currentInterview[index].question}");

        // Display the selected question
        questionText.text = currentInterview[index].question;

        // Get a random response from the possible responses
        string[] responses = currentInterview[index].possibleResponses;
        if (responses == null || responses.Length == 0)
        {
            Debug.LogError($"No responses available for question {index + 1}");
            return;
        }

        string response = responses[Random.Range(0, responses.Length)];
        Debug.Log($"Selected response: {response}");

        // Display the response
        responseText.text = response;

        // Hide all question buttons
        question1Button.gameObject.SetActive(false);
        question2Button.gameObject.SetActive(false);
        question3Button.gameObject.SetActive(false);

        // Show the finish interview button
        finishInterviewButton.gameObject.SetActive(true);
    }

    public void FinishInterview()
    {
        // Notify the decision manager that the interview is complete
        DecisionManager.Instance.OnInterviewCompleted();
        
        // Hide interview panel and show gameplay UI
        interviewPanel.SetActive(false);
        gameplayUIGroup.SetActive(true);
        
        // Reset interview state
        questionText.text = "";
        responseText.text = "";
        finishInterviewButton.gameObject.SetActive(false);
    }

    public void StartInterviewFromCurrentCase()
    {
        StartInterview(CaseManager.Instance.currentCase.interviewBranches);
    }


}
