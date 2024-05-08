using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] public GameObject characterBox;
    [SerializeField] public GameObject characterboxText;
    [SerializeField] public GameObject Radio;
    [SerializeField] public GameObject textBox;
    [SerializeField] public GameObject Dialogue1;
    [SerializeField] public GameObject Dialogue2;
    [SerializeField] public GameObject Dialogue3;
    [SerializeField] public GameObject Dialogue4;
    [SerializeField] public GameObject Dialogue5;
    [SerializeField] public GameObject Dialogue6;
    [SerializeField] public GameObject Dialogue7;


    [SerializeField] int currentText;

    // Start is called before the first frame update
    void Start()
    {
        currentText = 0;
        characterBox.SetActive(false);
        characterboxText.SetActive(false);
        Radio.SetActive(false);
        textBox.SetActive(false);
        Dialogue1.SetActive (false);
        Dialogue2.SetActive (false);
        Dialogue3.SetActive (false);
        Dialogue4.SetActive (false);
        Dialogue5.SetActive (false);
        Dialogue6.SetActive (false);
        Dialogue7.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && currentText <= 3)
        {
            currentText++;
        }

        if (currentText == 1)
        {
            Dialogue1.SetActive(true);
            characterBox.SetActive(true);
            characterboxText.SetActive(true);
            Radio.SetActive(true);
            textBox.SetActive(true);
        }

        else
        {
            Dialogue1.SetActive(false);
        }

        if (currentText == 2)
        {
            Dialogue2.SetActive(true);
            characterBox.SetActive(true);
            characterboxText.SetActive(true);
            Radio.SetActive(true);
            textBox.SetActive(true);
        }

        else
        {
            Dialogue2.SetActive(false);
        }

        if (currentText == 3)
        {
            Dialogue3.SetActive(true);
            characterBox.SetActive(true);
            characterboxText.SetActive(true);
            Radio.SetActive(true);
            textBox.SetActive(true);
        }

        else
        {
            Dialogue3.SetActive(false);
            characterBox.SetActive(false);
            characterboxText.SetActive(false);
            Radio.SetActive(false);
            textBox.SetActive(false);
        }

    }
}
