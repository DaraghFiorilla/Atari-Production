using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("An Array of transforms representing camera positions")]
    [SerializeField] private Transform[] povs;
    [Tooltip("The speed at which the camera follows the plane")]
    [SerializeField] private float speed;
    [Tooltip("The camera position of bomb mode")]
    [SerializeField] private Transform bombPov;

    private int index = 0;
    private Vector3 target;
    private PlaneController planeController;

    private void Awake()
    {
        planeController = FindObjectOfType<PlaneController>();
    }

    private void Update()
    {
        if (!planeController.bombMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                index = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                index = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                index = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                index = 3;
            }

            target = povs[index].position;
        }
        else
        {
            target = bombPov.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.forward = povs[index].forward;
    }
}
