using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    [Tooltip("How much the throttle ramps up or down")]
    [SerializeField] private float throttleIncrement = 0.1f;
    [Tooltip("Maximus engine thrust when at 100% throttle")]
    [SerializeField] private float maxThrust = 200f;
    [Tooltip("How responsive the plane is when rolling, pitching, and yawing")]
    [SerializeField] private float responsiveness = 10f;

    private float throttle; // Percentage of maximum engine thrust currently being used
    private float roll; // Tilting left to right
    private float pitch; // Tilting front to back
    private float yaw; // Turning left to right
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrust * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);
    }

    private float responseModifier // Value used to tweak responsiveness to suit plane's mass
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))
        {
            throttle += throttleIncrement;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            throttle -= throttleIncrement;
        }

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }
}
