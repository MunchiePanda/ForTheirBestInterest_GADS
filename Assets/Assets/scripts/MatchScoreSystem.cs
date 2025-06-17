using UnityEngine;

public class MatchScoreSystem : MonoBehaviour
{
    public static MatchScoreSystem Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }



    public void EvaluateMatch()
    {
        var data = CaseManager.Instance.currentCase;
        var child = data.childProfile;

        int score = 0;

        if (data.notes.ToLower().Contains(child.language.ToLower()))
            score++;
        if (data.notes.ToLower().Contains(child.culture.ToLower()))
            score++;

        foreach (string need in child.needs)
        {
            if (data.notes.ToLower().Contains(need.ToLower()))
                score++;
        }

        Debug.Log($"Match Score: {score}/3");
        UIManager.Instance.DisplayMatchScore(score);
    }
    public int CalculateRawMatch(CaseData data)
    {
        int score = 0;
        var child = data.childProfile;

        if (data.notes.ToLower().Contains(child.language.ToLower()))
            score++;
        if (data.notes.ToLower().Contains(child.culture.ToLower()))
            score++;

        foreach (string need in child.needs)
        {
            if (data.notes.ToLower().Contains(need.ToLower()))
                score++;
        }

        return score;
    }
}