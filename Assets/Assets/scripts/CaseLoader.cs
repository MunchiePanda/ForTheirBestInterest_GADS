using UnityEngine;

public class CaseLoader : MonoBehaviour
{
    public TextAsset[] caseJsonFiles; // Assign 9 case JSON files in Inspector
    private int currentCaseIndex = 0;

    private void Start()
    {
        // Reset case index when starting
        currentCaseIndex = 0;
        LoadNextCase();
    }

    public void LoadNextCase()
    {
        Debug.Log($"Loading case {currentCaseIndex + 1} of {caseJsonFiles.Length}");
        
        if (currentCaseIndex >= caseJsonFiles.Length)
        {
            Debug.Log("All cases completed, loading end scene");
            // Load end scene directly instead of going through next day
            UnityEngine.SceneManagement.SceneManager.LoadScene("DemoEND");
            return;
        }

        TextAsset jsonFile = caseJsonFiles[currentCaseIndex];
        if (jsonFile == null)
        {
            Debug.LogError($"Case file at index {currentCaseIndex} is null!");
            return;
        }

        CaseData caseData = JsonUtility.FromJson<CaseData>(jsonFile.text);
        if (caseData == null)
        {
            Debug.LogError($"Failed to parse case data from file at index {currentCaseIndex}");
            return;
        }

        CaseManager.Instance.currentCase = caseData;
        UIManager.Instance.DisplayCase(caseData);
        
        // Reset checklist and interview state for new case
        if (ChecklistSystem.Instance != null)
        {
            ChecklistSystem.Instance.EvaluateChecklist();
        }
        
        currentCaseIndex++;
    }

    public void ResetCaseIndex()
    {
        currentCaseIndex = 0;
    }
}
