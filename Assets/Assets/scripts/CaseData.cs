using UnityEngine;

[System.Serializable]
public class CaseData
{
    public string applicantName;
    public int income;
    public bool hasMedicalClearance;
    public bool hasHomeStudy;
    public string notes;
    public string[] interviewResponses;
    public int applicantAge;
    public string maritalStatus;
    public float monthlyExpenses;
    public string backgroundCheckNotes;

    public ChildData childProfile; // NEW
    public InterviewEntry[] interviewBranches;
}
