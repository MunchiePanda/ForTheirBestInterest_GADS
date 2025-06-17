using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OutcomeManager : MonoBehaviour
{
    public TextMeshProUGUI outcomeText; // Text to display the outcome
    public Button continueButton; // Button to continue to the next scene
    public GameObject goodOutComeImage; // Image to show for good outcomes
    public GameObject badOutComeImage; // Image to show for bad outcomes
    void Start()
    {
        // Set the outcome text based on the last decision made
        string childName = DecisionManager.LastChildName;
        bool goodOutcome = DecisionManager.LastOutcomeGood;

        goodOutComeImage.SetActive(false);
        badOutComeImage.SetActive(false);

        if (goodOutcome) // If the outcome was good
        {
            outcomeText.text = $"{childName} is thriving with their new family!";
            outcomeText.color = Color.green;
            goodOutComeImage.SetActive(true);
        }
        else // If the outcome was bad (placement did not work out) 
        {
            outcomeText.text = $"{childName} was placed in foster care after the placement did not work out.";
            outcomeText.color = Color.red;
            badOutComeImage.SetActive(true);
        }
        continueButton.onClick.AddListener(() => SceneManager.LoadScene("DemoEND"));
    }
}