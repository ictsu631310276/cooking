using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolPlayerScript : MonoBehaviour
{
    public List<PatientDataScript> patientID = new List<PatientDataScript>();
    public List<BedScript> bed = new List<BedScript>();
    public List<ItemBoxScript> itemBox = new List<ItemBoxScript>();
    [SerializeField] private Transform handPoint;
    [HideInInspector] public int itemID;
    public bool havePatient;
    private GameObject modelItem;
    public PotionDataScript potionData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Table")
        {
            bed.Add(other.gameObject.GetComponent<BedScript>());
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
            if (itemBox.Count > 0 && itemBox[0]== null)
            {
                itemBox.RemoveAt(0);
            }
        }
        else if (other.gameObject.tag == "Patient")
        {
            if (patientID.Count > 0)
            {
                patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[0];
                patientID.Remove(other.gameObject.GetComponent<PatientDataScript>());
            }
        }
    }
    public void ChangeTarget()
    {
        if (patientID.Count >= 2)
        {
            patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[0];
            patientID.RemoveAt(0);
            patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[1];
        }//สลับโต็ะที่เล็ง
    }
    public void MovePatient(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            if (patientID.Count > 0 && itemID == 0 && itemBox.Count == 0)
            {
                if (patientID[0] == null)
                {
                    patientID.RemoveAt(0);
                }
                else if (!havePatient && !patientID[0].onHand)
                {
                    potionData.sound.PlaySoundPick();
                    patientID[0].handPoint = handPoint;
                    patientID[0].onHand = true;
                    havePatient = true;
                    if (TextScript.textStart == 0)
                    {
                        TextScript.textStart = 1;
                    }
                }//หยิบ
                else if (patientID[0].onHand && bed.Count == 0 && havePatient)
                {
                    potionData.sound.PlaySoundPick();
                    patientID[0].onHand = false;
                    patientID[0].handPoint = null;
                    havePatient = false;
                }//วางพื้น
                else if (patientID[0].onHand && bed.Count > 0 && !bed[0].haveSit && havePatient && !patientID[0].dead)
                {
                    if (patientID[0].sicknessID == bed[0].treatTheSick || bed[0].treatTheSick < 0)
                    {
                        potionData.sound.PlaySoundPick();
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
                    potionData.sound.PlaySoundPick();
                }//ยกออกจากเตียง
                else
                {
                    patientID[0].modelBunda.GetComponent<Renderer>().material = patientID[0].materialBunda[0];
                }
            }
            //else if (bed.Count > 0 && itemID != 0)
            //{
            //    if (bed[0].itemId == 0)
            //    {
            //        bed[0].itemId = itemID;
            //        itemID = 0;
            //        modelItem.transform.parent = bed[0].handPoint;
            //    }
            //}//วาง item ใส่เตียง
            //else if (bed.Count > 0 && itemID == 0)
            //{
            //    if (bed[0].itemId != 0)
            //    {
            //        itemID = bed[0].itemId;
            //        bed[0].itemId = 0;
            //        modelItem = bed[0].handPoint.transform.GetChild(0).gameObject;
            //        modelItem.transform.parent = handPoint;
            //    }
            //}//หยิบ item ออกจากเตียง
            else if (itemBox.Count > 0)
            {
                if (itemBox[0].itemID == 99)
                {
                    if (havePatient && patientID[0].dead)
                    {
                        itemBox[0].numOfRequired++;
                        potionData.sound.PlaySoundPick();
                        Destroy(patientID[0].gameObject);
                        havePatient = false;
                        if (TextScript.textStart == 12)
                        {
                            TextScript.textStart++;
                        }
                    }//ใส่ศพ
                    else
                    {
                        potionData.sound.PlaySoundPick();
                        PickUpItem(obj);
                    }//หยิบ
                }//กล่องยาวิเศษ
                else
                {
                    potionData.sound.PlaySoundPick();
                    PickUpItem(obj);
                }//หยิบปกติ
            }
            else if (itemID == 99 && patientID.Count > 0)
            {
                if (!patientID[0].dead)
                {
                    if (TextScript.textStart == 15 || TextScript.textStart == 16)
                    {
                        TextScript.textStart++;
                    }
                    potionData.sound.PlaySoundPick();
                    itemID = 0;
                    Destroy(modelItem);
                    patientID[0].sicknessLevel = 1;
                    patientID[0].sicknessID = -1;
                    patientID.RemoveAt(0);
                }
            }
        }
    }
    private void PickUpItem(InputAction.CallbackContext obj)
    {
        if (itemBox.Count > 0 && itemID == 0 && !havePatient && obj.started)
        {
            if (itemBox[0].numOfItem > 0)
            {
                itemID = itemBox[0].itemID;
                itemBox[0].numOfItem--;
                modelItem = Instantiate(itemBox[0].modelItem, handPoint, false);
                modelItem.transform.position = handPoint.transform.position;
            }
        }
        else if (itemBox.Count > 0 && obj.started && itemID == itemBox[0].itemID)
        {
            itemID = 0;
            itemBox[0].numOfItem++;
            Destroy(modelItem, 0);
        }
    }
    public void AddArrow(InputAction.CallbackContext obj)
    {
        Vector2 sw = obj.ReadValue<Vector2>();
        if (obj.started && bed.Count > 0)
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
            else if (patientID[0].onBed)
            {
                havePatient = false;
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
            if (bed[0].NPCData != null && !havePatient && bed[0].haveSit && patientID.Count == 1)
            {
                //if (bed[0].treatTheSick < -1)
                //{
                    bed[0].glowObj.SetActive(true);
                    bed[0].PlayMinigame();
                //}
                //else if ((bed[0].NPCData.sicknessID == 1 && bed[0].itemId == 1) ||
                //    (bed[0].NPCData.sicknessID == 2 && bed[0].itemId == 2))
                //{
                //    bed[0].glowObj.SetActive(true);
                //    bed[0].PlayMinigame();
                //}
                //else if (bed[0].NPCData.sicknessID != 1 && bed[0].NPCData.sicknessID != 2)
                //{
                //    bed[0].glowObj.SetActive(true);
                //    bed[0].PlayMinigame();
                //}
            }
        }


    }
}
