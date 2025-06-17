using UnityEngine;

public class DocumentChecker : MonoBehaviour
{
    public void CheckDocuments()
    {
        CaseData data = CaseManager.Instance.currentCase;

        bool passed = data.hasMedicalClearance && data.hasHomeStudy;
        Debug.Log(passed ? "Documents Complete." : "Missing required documents!");

        // Notify ChecklistSystem that documents have been checked
        FindObjectOfType<ChecklistSystem>().OnDocumentsChecked();
    }
}