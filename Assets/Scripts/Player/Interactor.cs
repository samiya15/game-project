using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableLayers;

    private IInteractable currentTarget;

    private void Update()
    {
        FindTarget();

        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            currentTarget.Interact();
        }
    }

    private void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, interactableLayers);

        currentTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IInteractable interactable))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    currentTarget = interactable;
                }
            }
        }

        if (InteractionPromptUI.Instance != null)
        {
            InteractionPromptUI.Instance.ShowTarget(currentTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}