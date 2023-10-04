using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPlayerScript_2 : MonoBehaviour
{
    public List<PatientDataScript> PatientID = new List<PatientDataScript>();
    public List<BedScript> bed = new List<BedScript>();
    private bool havePatient;
    [SerializeField] private Transform[] handPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Table")
        {
            bed.Add(other.gameObject.GetComponent<BedScript>());
            if (bed.Count != 0)
            {
                if (!bed[0].bedDirty)
                {
                    bed[0].glowObj.SetActive(true);
                }
            }
        }
        else if (other.gameObject.tag == "Patient")
        {
            PatientID.Add(other.gameObject.GetComponent<PatientDataScript>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Table")
        {
            if (bed.Count != 0)
            {
                bed[0].glowObj.SetActive(false);
                bed.RemoveAt(0);
            }
        }
        else if (other.gameObject.tag == "Patient")
        {
            PatientID[0].Obj.SetActive(true);
            PatientID[0].glowObj.SetActive(false);
            PatientID.Remove(other.gameObject.GetComponent<PatientDataScript>());
        }
    }
    public void ChangeTarget()
    {
        if (PatientID.Count >= 2)
        {
            PatientID[0].glowObj.SetActive(false);
            PatientID[0].Obj.SetActive(true);
            PatientID.Add(PatientID[0]);
            PatientID.RemoveAt(0);
        }//สลับโต็ะที่เล็ง
    }
    private void Start()
    {
        PatientID.Clear();
        bed.Clear();
        havePatient = false;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && PatientID.Count >= 2)
        //{
        //    PatientID.Add(PatientID[0]);
        //    PatientID.RemoveAt(0);
        //}//สลับโต็ะที่เล็ง
        if (PatientID.Count > 0)
        {
            if (PatientID[0] == null)
            {
                PatientID.RemoveAt(0);
            }
            else if (!havePatient && !PatientID[0].onHand)
            {
                PatientID[0].Obj.SetActive(false);
                PatientID[0].glowObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PatientID[0].handPoint = handPoint[0];
                    PatientID[0].onHand = true;
                    havePatient = true;
                }
            }//หยิบ
            else if (PatientID[0].onHand && bed.Count == 0 && havePatient)
            {
                PatientID[0].Obj.SetActive(false);
                PatientID[0].glowObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PatientID[0].onHand = false;
                    PatientID[0].transform.position = handPoint[1].transform.position;
                    PatientID[0].handPoint = null;
                    havePatient = false;
                }
            }//วางพื้น
            else if (PatientID[0].onHand && bed.Count > 0 && !bed[0].haveSit && havePatient)
            {
                PatientID[0].Obj.SetActive(false);
                PatientID[0].glowObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PatientID[0].handPoint = bed[0].handPoint;
                    havePatient = false;
                    bed[0].haveSit = true;
                    PatientID[0].onBed = true;
                }
            }//วางบนเตียง
            else if (PatientID[0].onHand && bed.Count > 0 && bed[0].haveSit && !havePatient && !PatientID[0].willTreat)
            {
                PatientID[0].Obj.SetActive(false);
                PatientID[0].glowObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PatientID[0].handPoint = handPoint[0];
                    havePatient = true;
                    bed[0].haveSit = false;
                    PatientID[0].onBed = false;
                }
            }//ยกออกจากเตียง
            else
            {
                PatientID[0].Obj.SetActive(true);
                PatientID[0].glowObj.SetActive(false);
            }
        }
        else if (PatientID.Count == 0)
        {
            havePatient = false;
            PatientID.Clear();
        }

        if (bed.Count > 0)
        {
            if (bed[0].NPCData != null && !havePatient)
            {
                bed[0].glowObj.SetActive(true);
                if (bed[0].haveSit)
                {
                    bed[0].PlayMinigame();
                }
            }
        }
        //Debug.Log("bed.Count : " + bed.Count);
        //Debug.Log("PatientID.Count : " + PatientID.Count);
    }
}
