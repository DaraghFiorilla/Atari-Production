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
    [SerializeField] private GameObject mainCam, bombModeCam;

    private int index = 0;
    private Transform target;
    private PlaneController planeController;

    private void Awake()
    {
        planeController = FindObjectOfType<PlaneController>();
        mainCam.SetActive(true);
        bombModeCam.SetActive(false);
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

            target = povs[index];
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, target.position, Time.deltaTime * speed);
            mainCam.transform.forward = povs[index].forward;
            mainCam.transform.rotation = povs[index].rotation;
            mainCam.SetActive(true);
            bombModeCam.SetActive(false);
        }
        else
        {
            mainCam.SetActive(false);
            bombModeCam.SetActive(true);
        }

    }
}
