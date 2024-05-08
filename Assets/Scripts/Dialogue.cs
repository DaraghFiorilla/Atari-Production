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
        //Debug Feature + Explanation

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            currentText++;
        }*/

        //For the first three lines of dialogue you just press Y to advance. The rest are triggered by bombing a site which plays the next line which can be closed with Y.
        //Currently missing is code that causes the int variable to increment upon bombing a site and code that increments int by 2 in the event the player never closes the box after bombing a site.

        if (Input.GetKeyDown(KeyCode.Y) && currentText <= 3)
        {
            currentText++;
        }

        if (currentText == 1)
        {
            Dialogue1.SetActive(true);
            ActivateBoxes();
        }

        else
        {
            Dialogue1.SetActive(false);
        }

        if (currentText == 2)
        {
            Dialogue2.SetActive(true);
            ActivateBoxes();
        }

        else
        {
            Dialogue2.SetActive(false);
        }

        if (currentText == 3)
        {
            Dialogue3.SetActive(true);
            ActivateBoxes();
        }

        else
        {
            Dialogue3.SetActive(false);
        }

        if (currentText == 4)
        {
            DeactivateBoxes();
        }

        if (currentText == 5)
        {
            Dialogue4.SetActive(true);
            ActivateBoxes();
            if (Input.GetKeyDown(KeyCode.Y))
            {
                currentText++;
            }
        }

        else
        {
            Dialogue4.SetActive(false);
        }

        if (currentText == 6)
        {
            DeactivateBoxes();
        }

        if (currentText == 7)
        {
            Dialogue5.SetActive(true);
            ActivateBoxes();
            if (Input.GetKeyDown(KeyCode.Y))
            {
                currentText++;
            }
        }

        else
        {
            Dialogue5.SetActive(false);
        }

        if (currentText == 8)
        {
            DeactivateBoxes();
        }

        if (currentText == 9)
        {
            Dialogue6.SetActive(true);
            ActivateBoxes();
            if (Input.GetKeyDown(KeyCode.Y))
            {
                currentText++;
            }
        }

        else
        {
            Dialogue6.SetActive(false);
        }

        if (currentText == 10)
        {
            DeactivateBoxes();
        }

        if (currentText == 11)
        {
            Dialogue7.SetActive(true);
            ActivateBoxes();
        }

        else
        {
            Dialogue7.SetActive(false);
        }

        if (currentText == 12)
        {
            DeactivateBoxes();
        }

        void ActivateBoxes()
        {
            characterBox.SetActive(true);
            characterboxText.SetActive(true);
            Radio.SetActive(true);
            textBox.SetActive(true);
        }

        void DeactivateBoxes()
        {
            characterBox.SetActive(false);
            characterboxText.SetActive(false);
            Radio.SetActive(false);
            textBox.SetActive(false);
        }
    }
}
