using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolPlayerScript_2 : MonoBehaviour
{
    public List<PatientDataScript> PatientID = new List<PatientDataScript>();
    public List<BedScript> bed = new List<BedScript>();
    public bool havePatient;
    [SerializeField] private Transform handPoint;

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
            PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[0];
            PatientID.Remove(other.gameObject.GetComponent<PatientDataScript>());
        }
    }
    public void ChangeTarget()
    {
        if (PatientID.Count >= 2)
        {
            PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[1];
            PatientID.Add(PatientID[0]);
            PatientID.RemoveAt(0);
        }//สลับโต็ะที่เล็ง
    }
    public void MovePatient()
    {
        if (PatientID.Count > 0)
        {
            //if (PatientID[0] == null)
            //{
            //    PatientID.RemoveAt(0);
            //}
            /*else*/
            if (!havePatient && !PatientID[0].onHand)
            {
                PatientID[0].handPoint = handPoint;
                PatientID[0].onHand = true;
                havePatient = true;

            }//หยิบ
            else if (PatientID[0].onHand && bed.Count == 0 && havePatient)
            {
                PatientID[0].onHand = false;
                PatientID[0].handPoint = null;
                havePatient = false;

            }//วางพื้น
            else if (PatientID[0].onHand && bed.Count > 0 && !bed[0].haveSit && havePatient)
            {
                PatientID[0].handPoint = bed[0].handPoint;
                havePatient = false;
                bed[0].haveSit = true;
                PatientID[0].onBed = true;

            }//วางบนเตียง
            else if (PatientID[0].onHand && bed.Count > 0 && bed[0].haveSit && !havePatient && !PatientID[0].willTreat)
            {
                PatientID[0].handPoint = handPoint;
                havePatient = true;
                bed[0].haveSit = false;
                PatientID[0].onBed = false;

            }//ยกออกจากเตียง
            else
            {
                PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[0];
            }
        }
    }
    public void AddArrow(InputAction.CallbackContext obj)
    {
        Vector2 sw = obj.ReadValue<Vector2>();
        switch (sw.x, sw.y)
        {
            case (0f, 1f):
                bed[0].arrowAdd = 0;
                break;
            case (0f, -1f):
                bed[0].arrowAdd = 1;
                break;
            case (-1f, 0f):
                bed[0].arrowAdd = 2;
                break;
            case (1f, 0f):
                bed[0].arrowAdd = 3;
                break;
            default:
                bed[0].arrowAdd = 5;
                break;
        }
        sw = new (0, 0);
    }
    private void Start()
    {
        PatientID.Clear();
        bed.Clear();
        havePatient = false;
    }
    private void Update()
    {
        if (PatientID.Count > 0)
        {
            if (PatientID[0] == null)
            {
                PatientID.RemoveAt(0);
            }
            else if (!havePatient && !PatientID[0].onHand)
            {
                PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[1];
            }//หยิบ
            else if (PatientID[0].onHand && bed.Count == 0 && havePatient)
            {
                PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[1];
            }//วางพื้น
            else if (PatientID[0].onHand && bed.Count > 0 && !bed[0].haveSit && havePatient)
            {
                PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[1];
            }//วางบนเตียง
            else if (PatientID[0].onHand && bed.Count > 0 && bed[0].haveSit && !havePatient && !PatientID[0].willTreat)
            {
                PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[1];
            }//ยกออกจากเตียง
            else
            {
                PatientID[0].Obj.GetComponent<Renderer>().material = PatientID[0].materialBunda[0];
            }
        }
        else if (PatientID.Count == 0)
        {
            havePatient = false;
            PatientID.Clear();
        }//แสดงตัวที่เล็ง

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
    }
}
