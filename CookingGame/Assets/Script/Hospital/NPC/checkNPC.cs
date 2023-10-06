using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class checkNPC : MonoBehaviour
{
    public bool canSpawn;
    private PatientDataScript patient;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patient")
        {
            canSpawn = false;
            patient = other.GetComponent<PatientDataScript>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Patient")
        {
            canSpawn = true;
            patient = null;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        canSpawn = true;
        if (patient == null)
        {
            patient = null;
        }
    }
}
