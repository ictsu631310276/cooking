using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPlayerScript : MonoBehaviour
{
    public static List<PatientDataScript> PatientID = new List<PatientDataScript>();
    public static List<BedScript> bed = new List<BedScript>();
    public static List<int> idBin = new List<int>();
    public static bool havePatient;
    private void Start()
    {
        PatientID.Clear();
        bed.Clear();
        idBin.Clear();
        havePatient = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PatientID.Count >= 2)
        {
            PatientID.Add(PatientID[0]);
            PatientID.RemoveAt(0);
        }//สลับโต็ะที่เล็ง
    }
}
