using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    [SerializeField] private Transform handPoint;
    private PatientDataScript patient;
    [SerializeField] private float timeEat;
    private float cooldownEat;
    private bool eatting;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patient" && !eatting)
        {
            patient = other.GetComponent<PatientDataScript>();
            patient.handPoint = handPoint;
            patient.onHand = true;
            EatBanda();
        }
    }
    private void EatBanda()
    {
        //animation
        cooldownEat = timeEat;
        eatting = true;
        Destroy(patient.gameObject, 2);
    }
    private void Start()
    {
        cooldownEat = 0;
        eatting = false;
    }
    private void Update()
    {
        if (eatting)
        {
            cooldownEat = cooldownEat - Time.deltaTime;
            if (cooldownEat <= 0)
            {
                eatting = false;
            }
        }
    }
}
