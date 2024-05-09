using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

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
    [SerializeField] private GameObject audioListenerObj;
    [SerializeField] private GameObject planeHUD;

    private void Awake()
    {
        planeController = FindObjectOfType<PlaneController>();
    }

    private void Update()
    {
        if (!planeController.bombMode) // FLYING MODE
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
            audioListenerObj.transform.SetParent(mainCam.transform);
            if (!mainCam.activeSelf) // failsafe if the cameras get messed up
            {
                mainCam.SetActive(true);
                bombModeCam.SetActive(false);
            }
            if (!planeHUD.activeSelf) { planeHUD.SetActive(true); }
        }
        else // BOMB MODE
        {
            audioListenerObj.transform.SetParent(bombModeCam.transform);
            if (!bombModeCam.activeSelf) // failsafe if the cameras get messed up
            {
                mainCam.SetActive(false);
                bombModeCam.SetActive(true);
            }
            if (planeHUD.activeSelf) { planeHUD.SetActive(false); }
        }

    }

    public void SwitchToMainCam()
    {
        mainCam.SetActive(true);
        bombModeCam.SetActive(false);
        mainCam.transform.position = povs[0].position;
        planeController.StopAllCoroutines();
    }
}
