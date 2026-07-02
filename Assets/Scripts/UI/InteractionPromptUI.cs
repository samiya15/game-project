using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    public static InteractionPromptUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void ShowTarget(IInteractable target)
    {
        if (target == null)
        {
            Hide();
            return;
        }

        panel.SetActive(true);
        promptText.text = target.GetPromptText();
    }

    public void Hide()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}