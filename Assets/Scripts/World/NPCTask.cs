using UnityEngine;

public class NPCTask : MonoBehaviour
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
}