using UnityEngine;

public class NPCTask : MonoBehaviour, IInteractable
{
    [Header("Task Details")]
    public string taskName;
    [TextArea]
    public string description;

    [Header("Time")]
    public float timeCost;
    public float timeReward;

    [Header("Ending")]
    public bool isMainTask = false;

    [Header("Requires Delivery")]
    public bool requiresDelivery = false;

    [Header("Status")]
    public bool isCompleted;
    public bool isInProgress;

    public void StartTask()
    {
        if (isCompleted)
        {
            Debug.Log("Task already completed.");
            return;
        }

        if (isInProgress)
        {
            Debug.Log("Already working on this task.");
            return;
        }

        if (TimeManager.Instance == null) return;

        if (TimeManager.Instance.RemainingHours < timeCost)
        {
            Debug.Log("Not enough time to start this task.");
            return;
        }

        TimeManager.Instance.SpendTime(timeCost);
        isInProgress = true;

        if (!requiresDelivery)
        {
            CompleteTask();
        }
        else
        {
            Debug.Log(taskName + " accepted — now go deliver it.");
        }
    }

    // Called by DeliveryPoint once the player reaches the delivery spot
    public void CompleteDelivery()
    {
        if (!isInProgress || isCompleted) return;
        CompleteTask();
    }

    private void CompleteTask()
    {
        isCompleted = true;
        isInProgress = false;

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.EarnTime(timeReward);
        }

        Debug.Log("Completed: " + taskName);

        if (TaskListUI.Instance != null)
        {
            TaskListUI.Instance.AddCompletedTask(taskName);
        }

        if (EndingManager.Instance != null)
        {
            EndingManager.Instance.RegisterTaskCompleted(isMainTask);
        }
    }

    public void Interact()
    {
        StartTask();
    }

    public string GetPromptText()
    {
        if (isCompleted) return $"{taskName}: Task already completed.";
        if (isInProgress) return $"{taskName}: In progress — go deliver it!";
        return $"{taskName}\n{description}\nCost: {timeCost}h | Reward: {timeReward}h\nPress E to accept.";
    }
}