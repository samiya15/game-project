using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interaction")]
    public float interactRange = 3f;

    public LayerMask interactLayer;

    private IInteractable currentTarget;

    void Update()
    {
        FindTarget();

        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            currentTarget.Interact();
        }
    }

    void FindTarget()
    {
        currentTarget = null;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            interactRange,
            interactLayer
        );

        float closest = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();

            if (interactable != null)
            {
                float distance = Vector3.Distance(
                    transform.position,
                    hit.transform.position
                );

                if (distance < closest)
                {
                    closest = distance;
                    currentTarget = interactable;
                }
            }
        }

        // InteractionPromptUI will be added later by the teammate
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}