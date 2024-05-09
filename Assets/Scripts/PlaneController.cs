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
    [Tooltip("Maximum engine thrust when at 100% throttle")]
    [SerializeField] private float maxThrust = 200f;
    [Tooltip("Maximum engine thrust when in bomb mode")]
    [SerializeField] private float bmMaxThrust = 100f;
    [Tooltip("How responsive the plane is when rolling, pitching, and yawing")]
    [SerializeField] private float responsiveness = 10f;
    [SerializeField] private float lift = 135f;
    [Tooltip("How fast the plane will correct its rotation when entering Bomb Mode in seconds")]
    [SerializeField] private float bmCorrectionTime;

    [Header("Bombing Stats")]
    [Tooltip("Max bombs")]
    [SerializeField] private int maxBombs = 5;
    private int currentBombs;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombCooldown = 5;
    private float bombTimer;

    private float throttle; // Percentage of maximum engine thrust currently being used
    private float roll; // Tilting left to right
    private float pitch; // Tilting front to back
    private float yaw; // Turning left to right
    private Rigidbody rb;

    [Header("GameObject references")]
    [SerializeField] private TextMeshProUGUI hud;
    [SerializeField] private CameraController cameraController;

    public bool bombMode;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBombs = maxBombs;
        bombTimer = bombCooldown;
        Debug.Log(cameraController);
    }

    private void Update()
    {
        if (!bombMode) { HandleInputs(); }
        else { BombModeInputs(); }
        UpdateHUD();

        //propeller.Rotate(Vector3.right * throttle);

        if (bombTimer > 0) { bombTimer -= Time.deltaTime; }
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
        else
        {
            rb.AddForce(transform.forward * bmMaxThrust * throttle);
        }
    }

    private void OnCollisionEnter(Collision collision) // Use this to detect when plane colliders at high speeds, causing explosion
    {
        /*if (rb.velocity.y < -0.5f)
        {
            ExplodePlane();
        }*/
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

    private bool rotationCorrected;

    private void BombModeInputs()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        /*if (gameObject.transform.rotation != new Quaternion(0, 0, 0, 0)) // CORRECT ROTATION
        {
            Debug.Log("Rotation is not 0");
            Vector3 targetRotation = Vector3.zero;
            float correctionSpeed = bmCorrectionSpeed * Time.deltaTime;
            Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetRotation, correctionSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newRotation);
        }*/

        if (!rotationCorrected)
        {
            rotationCorrected = true;
            StartCoroutine(ResetRotation(gameObject.transform, Quaternion.identity, bmCorrectionTime));
        }

        // SET MAX THRUST SPEED


        if (Input.GetKeyDown(KeyCode.B)) // TRANSITION FROM BOMB MODE
        {
            rb.constraints = RigidbodyConstraints.None;
            rotationCorrected = false;
            bombMode = false;
            cameraController.SwitchToMainCam();
            
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            LaunchBomb();
        }
    }

    private void UpdateHUD()
    {
        hud.text = "Throttle: " + throttle.ToString("F1") + "%\n";
        hud.text += "Airspeed: " + (rb.velocity.magnitude * 3.6f).ToString("F1") + "km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F1") + "m\n";
        hud.text += "Current bombs: " + currentBombs;
    }

    private void LaunchBomb()
    {
        Debug.Log("bombTimer = " + bombTimer);
        if (currentBombs > 0 && bombTimer <= 0)
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2, gameObject.transform.position.z);
            currentBombs--;
            bombTimer = bombCooldown; 
        }
    }

    private void ExplodePlane()
    {
        // EXPLODE THE MF PLANE
    }

    static public IEnumerator ResetRotation(Transform target, Quaternion rot, float dur)
    {
        float t = 0f;
        Quaternion start = target.rotation;
        while (t < dur)
        {
            target.rotation = Quaternion.Slerp(start, rot, t / dur);
            yield return null;
            t += Time.deltaTime;
        }
        target.rotation = rot;
    }
}
