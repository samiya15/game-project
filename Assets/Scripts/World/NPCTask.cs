using UnityEngine;

public class NPCTask : MonoBehaviour
{
    [Header("Task Information")]
    public string taskName;

    [TextArea]
    public string description;

    public float timeCost = 2f;
    public float timeReward = 4f;

    public bool completed = false;

    public void CompleteTask()
    {
        if (completed)
            return;

        completed = true;

        Debug.Log(taskName + " completed!");

        
    }
}