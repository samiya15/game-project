using UnityEngine;

public class DeliveryPoint : MonoBehaviour, IInteractable
{
    [Header("Linked Task")]
    [Tooltip("The NPCTask that must be in-progress before this delivery can be completed")]
    public NPCTask linkedTask;

    [Header("Prompt")]
    public string deliveryLabel = "Deliver medicine here";

    public void Interact()
    {
        if (linkedTask == null)
        {
            Debug.LogWarning("No linked task set on this DeliveryPoint.");
            return;
        }

        linkedTask.CompleteDelivery();
    }

    public string GetPromptText()
    {
        if (linkedTask == null) return "Nothing to deliver here.";

        if (linkedTask.isCompleted)
        {
            return $"{deliveryLabel}\nAlready delivered.";
        }

        if (linkedTask.isInProgress)
        {
            return $"{deliveryLabel}\nPress E to deliver.";
        }

        return $"{deliveryLabel}\nNothing to deliver yet — accept the task first.";
    }
}