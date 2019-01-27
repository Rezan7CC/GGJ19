using DefaultNamespace;
using UnityEngine;

public class shipManeuverController : MonoBehaviour, IResetable
{
    private Rigidbody rb;
    public float maxVelocity = 5;
    public float rotationSpeed = 3;
    public float acceleration = 3;
    public AudioSource FlightAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void HandleMovement()
    {
        float yAxis = Input.GetAxis("Vertical") * acceleration;
        if(yAxis > Mathf.Epsilon && !FlightAudioSource.isPlaying)
        {
            FlightAudioSource.Play();
        }
        else if(yAxis <= Mathf.Epsilon && FlightAudioSource.isPlaying)
        {
            FlightAudioSource.Stop();
        }

        float xAxis = Input.GetAxis("Horizontal") * rotationSpeed;
        ThrustForward(yAxis);
        Rotate(transform, xAxis);
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    #region Maneuvering API
    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    public void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, -amount);
    }
    #endregion

    public void Reset()
    {
        rb.velocity = Vector3.zero;
    }
}
