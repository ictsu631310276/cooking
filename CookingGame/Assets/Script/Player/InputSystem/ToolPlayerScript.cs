using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolPlayerScript : MonoBehaviour
{
    public List<PatientDataScript> patientID = new List<PatientDataScript>();
    public List<BedScript> bed = new List<BedScript>();
    public bool havePatient;

    public List<ItemBoxScript> itemBox = new List<ItemBoxScript>();
    public bool haveItem;
    public int itemID;

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
        else if (other.gameObject.tag == "ItemBox")
        {
            itemBox.Add(other.gameObject.GetComponent<ItemBoxScript>());
        }
        else if (other.gameObject.tag == "Patient")
        {
            patientID.Add(other.gameObject.GetComponent<PatientDataScript>());
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
        else if (other.gameObject.tag == "ItemBox")
        {
            itemBox.Remove(other.gameObject.GetComponent<ItemBoxScript>());
        }
        else if (other.gameObject.tag == "Patient")
        {
            patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[0];
            patientID.Remove(other.gameObject.GetComponent<PatientDataScript>());
        }
    }
    public void ChangeTarget()
    {
        if (patientID.Count >= 2)
        {
            patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[1];
            patientID.Add(patientID[0]);
            patientID.RemoveAt(0);
        }//สลับโต็ะที่เล็ง
    }
    public void MovePatient(InputAction.CallbackContext obj)
    {
        if (patientID.Count > 0 && !haveItem && obj.started)
        {
            if (patientID[0] == null)
            {
                patientID.RemoveAt(0);
            }
            else if (!havePatient && !patientID[0].onHand)
            {
                patientID[0].handPoint = handPoint;
                patientID[0].onHand = true;
                havePatient = true;
            }//หยิบ
            else if (patientID[0].onHand && bed.Count == 0 && havePatient)
            {
                patientID[0].onHand = false;
                patientID[0].handPoint = null;
                havePatient = false;

            }//วางพื้น
            else if (patientID[0].onHand && bed.Count > 0 && !bed[0].haveSit && havePatient)
            {
                if (patientID[0].sicknessID == bed[0].treatTheSick || bed[0].treatTheSick < 0)
                {
                    patientID[0].handPoint = bed[0].handPoint;
                    havePatient = false;
                    bed[0].haveSit = true;
                    patientID[0].onBed = true;
                }
            }//วางบนเตียง
            else if (patientID[0].onHand && bed.Count > 0 && bed[0].haveSit && !havePatient && !patientID[0].willTreat)
            {
                patientID[0].handPoint = handPoint;
                havePatient = true;
                bed[0].haveSit = false;
                patientID[0].onBed = false;

            }//ยกออกจากเตียง
            else
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[0];
            }
        }
        else
        {
            PickUpItem(obj);
        }
    }
    public void PickUpItem(InputAction.CallbackContext obj)
    {
        if (itemBox.Count > 0 && !haveItem && !havePatient && obj.started)
        {
            haveItem = true;
            itemID = itemBox[0].itemID;
        }
        else if (itemBox.Count > 0 && haveItem && obj.started && itemID == itemBox[0].itemID)
        {
            haveItem = false;
            itemID = 0;
        }
    }
    public void AddArrow(InputAction.CallbackContext obj)
    {
        Vector2 sw = obj.ReadValue<Vector2>();
        if (obj.started)
        {
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
        }
    }
    private void Start()
    {
        patientID.Clear();
        bed.Clear();
        havePatient = false;
        itemBox.Clear();
        haveItem = false;
        itemID = 0;
    }
    private void Update()
    {
        if (patientID.Count > 0)
        {
            if (patientID[0] == null)
            {
                patientID.RemoveAt(0);
            }
            else if (!havePatient && !patientID[0].onHand)
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[1];
            }//หยิบ
            else if (patientID[0].onHand && bed.Count == 0 && havePatient)
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[1];
            }//วางพื้น
            else if (patientID[0].onHand && bed.Count > 0 && !bed[0].haveSit && havePatient)
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[1];
            }//วางบนเตียง
            else if (patientID[0].onHand && bed.Count > 0 && bed[0].haveSit && !havePatient && !patientID[0].willTreat)
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[1];
            }//ยกออกจากเตียง
            else
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[0];
            }
        }
        else if (patientID.Count == 0)
        {
            havePatient = false;
            patientID.Clear();
        }//บันดะเลืองแสง

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
