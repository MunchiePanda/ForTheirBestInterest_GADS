using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterviewSystem : MonoBehaviour
{
    public static InterviewSystem Instance;

    public GameObject interviewPanel;
    public GameObject uiToHideDuringInterview;

    public TMP_Text questionText;
    public TMP_Text responseText;

    public Button question1Button;
    public Button question2Button;
    public Button question3Button;

    private InterviewEntry[] interviewEntries;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartInterview()
    {
        uiToHideDuringInterview.SetActive(false);
        interviewPanel.SetActive(true);
        interviewEntries = CaseManager.Instance.currentCase.interviewBranches;

        ShowQuestionButtons();
    }

    public void ShowQuestionButtons()
    {
        question1Button.gameObject.SetActive(true);
        question2Button.gameObject.SetActive(true);
        question3Button.gameObject.SetActive(true);

        question1Button.GetComponentInChildren<TMP_Text>().text = interviewEntries[0].question;
        question2Button.GetComponentInChildren<TMP_Text>().text = interviewEntries[1].question;
        question3Button.GetComponentInChildren<TMP_Text>().text = interviewEntries[2].question;
    }

    public void SelectQuestion(int index)
    {
        var entry = interviewEntries[index];
        questionText.text = entry.question;

        // Randomly pick one of the two responses
        string reply = entry.possibleResponses[Random.Range(0, entry.possibleResponses.Length)];
        responseText.text = reply;

        // Hide the buttons after one question is asked
        question1Button.gameObject.SetActive(false);
        question2Button.gameObject.SetActive(false);
        question3Button.gameObject.SetActive(false);
    }

    public void FinishInterview()
    {
        interviewPanel.SetActive(false);
        uiToHideDuringInterview.SetActive(true); // Reveal buttons again
       // UIStateController.Instance.EndInterview(); // optional: show decision panel
    }
}
