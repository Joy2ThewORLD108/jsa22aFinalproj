using UnityEngine;

public class RatController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Perimeter Boundaries")]
    // Adjust these values in the Unity Inspector to match your floor size
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    void Update()
    {
        // 1. Get input from Arrow Keys or WASD
        // "Horizontal" = Left/Right (A/D or Left/Right Arrows)
        // "Vertical" = Forward/Backward (W/S or Up/Down Arrows)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 2. Calculate the movement vector
        // We move on X (side-to-side) and Z (forward/backward). Y is 0 because it stays on the ground.
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Calculate the desired new position based on speed and frame rate (Time.deltaTime)
        Vector3 newPosition = transform.position + (movement * speed * Time.deltaTime);

        // 3. Keep the Rat inside the perimeter using Mathf.Clamp
        // This stops the X and Z coordinates from going past your min and max values.
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // 4. Apply the new clamped position to the object
        transform.position = newPosition;
    }
}