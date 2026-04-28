using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    private Rigidbody rb;
    private float horizontalInput;

    void Start()
    {
        // Get the Rigidbody component from the figure
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Capture input in Update for responsiveness
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // 2. Apply movement in FixedUpdate for consistent physics
        // We keep the existing Y and Z velocity so gravity still works
        rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
    }
}