using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged += UpdateTimeText;
            UpdateTimeText(TimeManager.Instance.RemainingHours);
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= UpdateTimeText;
        }
    }

    private void UpdateTimeText(float remainingHours)
    {
        if (timeText == null) return;
        timeText.text = TimeManager.Instance.GetFormattedTime();
    }
}