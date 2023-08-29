using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPlayerScript : MonoBehaviour
{
    public static List<PatientDataScript> PatientID = new List<PatientDataScript>();
    public static List<BedScript> bed = new List<BedScript>();
    public static List<int> idBin = new List<int>();
    public static int itemInHand;//ของในมือ
    public static bool havePatient;
    public static bool haveTool;
    [SerializeField] private GameObject tool;
    private void Start()
    {
        itemInHand = 0;
        havePatient = false;
        haveTool = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PatientID.Count >= 2)
        {
            PatientID.Add(PatientID[0]);
            PatientID.RemoveAt(0);
        }//สลับโต็ะที่เล็ง
        if (Input.GetKeyDown(KeyCode.Alpha1) && haveTool)
        {
            haveTool = false;
            itemInHand = 0;
            tool.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !havePatient)
        {
            haveTool = true;
            itemInHand = 1;
            tool.SetActive(true);
        }
        Debug.Log("haveTool : " + haveTool);
        Debug.Log("havePatient : " + havePatient);
        ////Debug.Log("bed.Count : " + bed.Count);
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    for (int i = 0; i < bed.Count; i++)
        //    {
        //        Debug.Log("bed ID : " + bed[i].id);
        //    }
        //}
        Debug.Log(PatientID.Count);
    }
}
