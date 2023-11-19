using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowScript : MonoBehaviour
{
    private ToolPlayerScript toolPlayer;
    private GameObject patient;

    public GameObject directionShow;
    private GameObject launchPointShow;
    public Transform launchPoint;
    public float height;
    public float distance;

    public void ThrowMethid(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            directionShow.SetActive(true);
            launchPointShow.SetActive(true);
        }
        else if (obj.canceled)
        {
            directionShow.SetActive(false);
            launchPointShow.SetActive(false);
            if (toolPlayer.patientID.Count != 0 && toolPlayer.havePatient)
            {
                patient.GetComponent<PatientDataScript>().onHand = false;
                toolPlayer.havePatient = false;

                patient.GetComponent<Rigidbody>().velocity = height * launchPoint.up;
                patient.GetComponent<Rigidbody>().velocity = distance * transform.forward;
            }
        }
    }
    private void Start()
    {
        toolPlayer = GetComponent<ToolPlayerScript>();
        directionShow.SetActive(false);
        launchPointShow = launchPoint.transform.GetChild(0).gameObject;
        launchPointShow.SetActive(false);
    }
    void Update()
    {
        if (toolPlayer.patientID.Count != 0)
        {
            if (!toolPlayer.patientID[0].onBed)
            {
                patient = toolPlayer.patientID[0].gameObject;
            }
        }
    }
}
