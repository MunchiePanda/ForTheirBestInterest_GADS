using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance;
    public CaseData currentCase;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
