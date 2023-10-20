using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    private PlayerMoveScript moveScript;
    private ToolPlayerScript toolPlayer;
    private GameObject patient;

    public GameObject directionShow;

    public void ThrowMethid()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            directionShow.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            directionShow.SetActive(false);
            if (toolPlayer.PatientID.Count != 0 && toolPlayer.havePatient)
            {
                patient.GetComponent<PatientDataScript>().onHand = false;
                toolPlayer.havePatient = false;
                patient.GetComponent<Rigidbody>().AddForce(Vector3.up * 150f, ForceMode.Impulse);
                switch (moveScript.gameObject.GetComponent<Transform>().rotation.y)
                {
                    case (0f):
                        patient.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100f, ForceMode.Impulse);
                        break;
                    case (0.7071068f):
                        patient.GetComponent<Rigidbody>().AddForce(Vector3.right * 100f, ForceMode.Impulse);
                        break;
                    case (1f):
                        patient.GetComponent<Rigidbody>().AddForce(Vector3.back * 100f, ForceMode.Impulse);
                        break;
                    case (-0.7071068f):
                        patient.GetComponent<Rigidbody>().AddForce(Vector3.left * 100f, ForceMode.Impulse);
                        break;
                }
            }
        }
    }
    private void Start()
    {
        moveScript = GetComponent<PlayerMoveScript>();
        toolPlayer = GetComponent<ToolPlayerScript>();
        directionShow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (toolPlayer.PatientID.Count != 0)
        {
            patient = toolPlayer.PatientID[0].gameObject;
        }

        ThrowMethid();
    }
}
