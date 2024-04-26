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

    [Header("Bombing Stats")]
    [Tooltip("Max bombs")]
    [SerializeField] private int maxBombs = 5;
    private int currentBombs;
    [Tooltip("How much force the bombs are launched with")]
    [SerializeField] private float bombForce;
    [SerializeField] private GameObject bombPrefab;


    private float throttle; // Percentage of maximum engine thrust currently being used
    private float roll; // Tilting left to right
    private float pitch; // Tilting front to back
    private float yaw; // Turning left to right
    private Rigidbody rb;

    [Header("GameObject references")]
    [SerializeField] private TextMeshProUGUI hud;
    [SerializeField] private Transform propeller;
    [Tooltip("The visual aid to show the player where the bomb will land")]
    [SerializeField] private GameObject aimGuide;

    public bool bombMode;

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
        else
        {
            BombModeInputs();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.y < -0.5f)
        {
            ExplodePlane();
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

        if (Input.GetKeyDown(KeyCode.B)) // TRANSITION TO BOMB MODE
        {
            bombMode = true;
        }

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void BombModeInputs()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        if (Input.GetKeyDown(KeyCode.B)) // TRANSITION FROM BOMB MODE
        {
            rb.constraints = RigidbodyConstraints.None;
            bombMode = false;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            LaunchBomb();
        }
    }

    private void UpdateHUD()
    {
        hud.text = "Throttle: " + throttle.ToString("F0") + "%\n";
        hud.text += "Airspeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + "m";
    }

    private void LaunchBomb()
    {
        if (currentBombs > 0)
        {
            // BOMB AWAY
            GameObject bomb = Instantiate(bombPrefab);
            // LAUNCH BOMB
            currentBombs--;
        }
    }

    private void ExplodePlane()
    {
        // EXPLODE THE MF PLANE
    }
}
