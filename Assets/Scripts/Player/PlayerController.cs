using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement = new Vector3(h, 0f, v).normalized;

        bool moving = movement.magnitude > 0.1f;

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.SpendMovementTime(moving);
        }
    }

    void FixedUpdate()
    {
        float speed = Input.GetKey(KeyCode.LeftShift)
            ? runSpeed
            : walkSpeed;

        Vector3 velocity = movement * speed;

#if UNITY_6000_0_OR_NEWER
        rb.linearVelocity = new Vector3(
            velocity.x,
            rb.linearVelocity.y,
            velocity.z
        );
#else
        rb.velocity = new Vector3(
            velocity.x,
            rb.velocity.y,
            velocity.z
        );
#endif

        if (movement != Vector3.zero)
        {
            Quaternion rotation =
                Quaternion.LookRotation(movement);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    rotation,
                    rotationSpeed * Time.fixedDeltaTime
                );
        }
    }
}