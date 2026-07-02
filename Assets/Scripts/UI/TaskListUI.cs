using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskListUI : MonoBehaviour
{
    public static TaskListUI Instance { get; private set; }

    [SerializeField] private TMP_Text taskListText;

    private readonly List<string> completedTasks = new();

    private void Awake()
    {
        Instance = this;
        Refresh();
    }

    public void AddCompletedTask(string taskName)
    {
        completedTasks.Add(taskName);
        Refresh();
    }

    private void Refresh()
    {
        if (taskListText == null) return;

        if (completedTasks.Count == 0)
        {
            taskListText.text = "Completed Tasks:\nNone";
            return;
        }

        taskListText.text = "Completed Tasks:\n";

        foreach (string task in completedTasks)
        {
            taskListText.text += $"- {task}\n";
        }
    }
}