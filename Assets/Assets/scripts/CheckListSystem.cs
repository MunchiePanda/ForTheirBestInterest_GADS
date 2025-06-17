using UnityEngine;
using UnityEngine.UI;

public class ChecklistSystem : MonoBehaviour
{
    public static ChecklistSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    [Header("Toggles")]
    public Toggle ageCheck;
    public Toggle incomeCheck;
    public Toggle medicalCheck;
    public Toggle homeStudyCheck;
    public Toggle backgroundCheck;
    public Toggle matchCheck;

    [Header("Action Buttons")]
    public Button approveButton;
    public Button rejectButton;
    public Button interviewButton;

    private bool hasCheckedDocuments = false;
    private bool hasCheckedChildDocument = false;

    private void Start()
    {
        // Add listeners to all toggles
        ageCheck.onValueChanged.AddListener((value) => OnToggleChanged());
        incomeCheck.onValueChanged.AddListener((value) => OnToggleChanged());
        medicalCheck.onValueChanged.AddListener((value) => OnToggleChanged());
        homeStudyCheck.onValueChanged.AddListener((value) => OnToggleChanged());
        backgroundCheck.onValueChanged.AddListener((value) => OnToggleChanged());
        matchCheck.onValueChanged.AddListener((value) => OnToggleChanged());

        // Initially disable all buttons until documents are checked
        UpdateButtonStates();
    }

    public void OnDocumentsChecked()
    {
        hasCheckedDocuments = true;
        UpdateButtonStates();
    }

    public void OnChildDocumentChecked()
    {
        hasCheckedChildDocument = true;
        UpdateButtonStates();
    }

    private void OnToggleChanged()
    {
        int checkedCount = GetCheckedCount();
        
        // If 4 or more toggles are checked, lock all unchecked toggles
        if (checkedCount >= 4)
        {
            LockUncheckedToggles();
        }
        else
        {
            UnlockAllToggles();
        }

        UpdateButtonStates();
    }

    private int GetCheckedCount()
    {
        int count = 0;
        if (ageCheck.isOn) count++;
        if (incomeCheck.isOn) count++;
        if (medicalCheck.isOn) count++;
        if (homeStudyCheck.isOn) count++;
        if (backgroundCheck.isOn) count++;
        if (matchCheck.isOn) count++;
        return count;
    }

    private void LockUncheckedToggles()
    {
        if (!ageCheck.isOn) ageCheck.interactable = false;
        if (!incomeCheck.isOn) incomeCheck.interactable = false;
        if (!medicalCheck.isOn) medicalCheck.interactable = false;
        if (!homeStudyCheck.isOn) homeStudyCheck.interactable = false;
        if (!backgroundCheck.isOn) backgroundCheck.interactable = false;
        if (!matchCheck.isOn) matchCheck.interactable = false;
    }

    private void UnlockAllToggles()
    {
        ageCheck.interactable = true;
        incomeCheck.interactable = true;
        medicalCheck.interactable = true;
        homeStudyCheck.interactable = true;
        backgroundCheck.interactable = true;
        matchCheck.interactable = true;
    }

    private void UpdateButtonStates()
    {
        Debug.Log($"Updating button states - Documents checked: {hasCheckedDocuments}, Child doc checked: {hasCheckedChildDocument}");

        // Initially disable all buttons until documents are checked
        if (!hasCheckedDocuments || !hasCheckedChildDocument)
        {
            approveButton.interactable = false;
            rejectButton.interactable = false;
            interviewButton.interactable = false;
            return;
        }

        // Approve, interview, and reject are always enabled after docs
        approveButton.interactable = true;
        interviewButton.interactable = true;
        rejectButton.interactable = true;
    }

    private bool ValidateCredentials(CaseData caseData)
    {
        if (caseData == null)
        {
            Debug.LogError("Case data is null!");
            return false;
        }

        // Check if the applicant's credentials match the requirements
        bool ageValid = caseData.applicantAge >= 25;
        bool incomeValid = caseData.income > caseData.monthlyExpenses;
        bool medicalValid = caseData.hasMedicalClearance;

        Debug.Log($"Credential Check - Age: {ageValid}, Income: {incomeValid}, Medical: {medicalValid}");

        // Return true if at least 2 core requirements are met
        int validCount = (ageValid ? 1 : 0) + (incomeValid ? 1 : 0) + (medicalValid ? 1 : 0);
        bool isValid = validCount >= 2;
        
        Debug.Log($"Total valid credentials: {validCount}, Is valid: {isValid}");
        
        return isValid;
    }

    public void EvaluateChecklist()
    {
        var data = CaseManager.Instance.currentCase;
        var child = data.childProfile;

        ageCheck.isOn = data.applicantAge >= 25;
        incomeCheck.isOn = data.income > data.monthlyExpenses;
        medicalCheck.isOn = data.hasMedicalClearance;
        homeStudyCheck.isOn = data.hasHomeStudy;
        backgroundCheck.isOn = data.backgroundCheckNotes.ToLower().Contains("clean");

        // Basic compatibility (adjust logic as needed)
        int matchScore = MatchScoreSystem.Instance.CalculateRawMatch(data);
        matchCheck.isOn = matchScore >= 2;

        // Update button states after setting initial values
        OnToggleChanged();
    }
}