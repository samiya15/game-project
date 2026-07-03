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

    [Header("Status")]
    public bool isCompleted;

    public void StartTask()
    {
        if (isCompleted)
        {
            Debug.Log("Task already completed.");
            return;
        }

        Debug.Log("Started task: " + taskName);

        // Later Person 1 will add:
        // TimeManager.Instance.SpendTime(timeCost);
    }

    public void CompleteTask()
    {
        if (isCompleted)
            return;

        isCompleted = true;

        Debug.Log("Completed: " + taskName);

    }
    public void Interact()
    {
        Debug.Log("Interacting with " + taskName);

        StartTask();
    }

    public string GetPromptText()
    {
        if (isCompleted)
        {
            return $"{taskName}: Task already completed.";
        }

        return $"{taskName}\n{description}\nCost: {timeCost}h | Reward: {timeReward}h\nPress E to accept.";
    }
}