using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Starting Time")]
    [SerializeField] private float startingHours = 72f;

    [Header("Movement Drain")]
    [SerializeField] private bool drainTimeWhileMoving = true;
    [SerializeField] private float movingDrainPerSecond = 0.01f;

    public float RemainingHours { get; private set; }
    public bool IsTimeExpired => RemainingHours <= 0f;

    public event Action<float> OnTimeChanged;
    public event Action OnTimeExpired;

    private bool hasExpired;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        RemainingHours = startingHours;
    }

    private void Start()
    {
        OnTimeChanged?.Invoke(RemainingHours);
    }

    public void SpendTime(float hours)
    {
        if (hasExpired || hours <= 0f) return;

        RemainingHours -= hours;
        RemainingHours = Mathf.Max(0f, RemainingHours);
        OnTimeChanged?.Invoke(RemainingHours);

        if (RemainingHours <= 0f)
        {
            hasExpired = true;
            OnTimeExpired?.Invoke();
        }
    }

    public void EarnTime(float hours)
    {
        if (hasExpired || hours <= 0f) return;

        RemainingHours += hours;
        OnTimeChanged?.Invoke(RemainingHours);
    }

    public void SpendMovementTime(bool isMoving)
    {
        if (!drainTimeWhileMoving || !isMoving) return;
        SpendTime(movingDrainPerSecond * UnityEngine.Time.deltaTime);
    }

    public string GetFormattedTime()
    {
        int totalHours = Mathf.CeilToInt(RemainingHours);
        int days = totalHours / 24;
        int hours = totalHours % 24;

        return $"{days} Days, {hours} Hours Remaining";
    }
}