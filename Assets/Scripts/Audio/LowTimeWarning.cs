using UnityEngine;

public class LowTimeWarning : MonoBehaviour
{
    [SerializeField] private float warningThresholdHours = 12f;
    [SerializeField] private string warningSoundName = "LowTime";

    private bool warningPlayed;

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged += CheckTime;
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= CheckTime;
        }
    }

    private void CheckTime(float remainingHours)
    {
        if (warningPlayed) return;

        if (remainingHours <= warningThresholdHours)
        {
            warningPlayed = true;

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.Play(warningSoundName);
            }
        }
    }
}