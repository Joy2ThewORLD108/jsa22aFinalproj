using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // This detects when the ball hits something
    private void OnCollisionEnter(Collision collision)
    {
        // Check the "Tag" of the object we hit
        if (collision.gameObject.CompareTag("BouncySurface"))
        {
            Debug.Log("Hit a bouncy surface!");
            // Optional: Manual force boost
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("StickySurface"))
        {
            // Slow the ball down significantly
            rb.linearVelocity *= 0.1f;
        }
    }
}