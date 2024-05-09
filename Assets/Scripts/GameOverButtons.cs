using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverButtons : MonoBehaviour
{
    [SerializeField] GameObject plane;
    [SerializeField] CameraController cc;
    //[SerializeField] TextMeshProUGUI hud;
    [SerializeField] Vector3 startingPos;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetPlane()
    {
        /*GameObject newPlane = Instantiate(planePrefab);
        PlaneController pc = newPlane.GetComponent<PlaneController>();
        newPlane.transform.position = startingPos;
        cc.audioListenerObj.transform.SetParent(cc.bombModeCam.transform);
        cc.planeController = pc;
        pc.gameOverScreen = gameObject;
        pc.hud = hud;
        cc.paused = false;
        gameObject.SetActive(false);*/
        plane.SetActive(true);
        plane.transform.position = startingPos;
        PlaneController pc = plane.GetComponent<PlaneController>();
        pc.smoke.gameObject.SetActive(true);
        pc.gameOver = false;
        pc.pause = false;
        plane.GetComponentInChildren<MeshRenderer>().enabled = true;
        plane.transform.rotation = Quaternion.identity;
        plane.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cc.paused = false;
        gameObject.SetActive(false);
    }
}
