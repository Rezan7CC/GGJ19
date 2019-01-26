using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipManeuverController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxVelocity = 5;
    public float rotationSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        ThrustForward(yAxis);
        Rotate(transform, xAxis * rotationSpeed);
    }

    #region Maneuvering API
    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(amount, 0, 0);
    }
    #endregion
}
