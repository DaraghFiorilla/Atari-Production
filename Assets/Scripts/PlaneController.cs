using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private float lift = 135f;

    private float throttle; // Percentage of maximum engine thrust currently being used
    private float roll; // Tilting left to right
    private float pitch; // Tilting front to back
    private float yaw; // Turning left to right
    private Rigidbody rb;

    [SerializeField] private TextMeshProUGUI hud;
    [SerializeField] private Transform propeller;

    [SerializeField] private bool bombMode;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!bombMode)
        {
            HandleInputs();
        }
        UpdateHUD();

        propeller.Rotate(Vector3.right * throttle);
    }

    private void FixedUpdate()
    {
        if (!bombMode)
        {
            rb.AddForce(transform.forward * maxThrust * throttle);
            rb.AddTorque(transform.up * yaw * responseModifier);
            rb.AddTorque(transform.right * pitch * responseModifier);
            rb.AddTorque(transform.forward * roll * responseModifier);

            rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
        }
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

    private void UpdateHUD()
    {
        hud.text = "Throttle: " + throttle.ToString("F0") + "%\n";
        hud.text += "Airspeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + "m";
    }
}
