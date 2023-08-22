using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPlayerScript : MonoBehaviour
{
    public static List<PatientDataScript> PatientID = new List<PatientDataScript>();
    public static List<BedScript> bed = new List<BedScript>();
    public static List<int> idTable = new List<int>();
    public static int itemInHand = 0;//ของในมือ
    public static bool haveItem = false;
    [SerializeField] private GameObject[] tool;
    private void OffVisibility()
    {
        for (int i = 0; i < tool.Length; i++)
        {
            tool[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PatientID.Count >= 2)
        {
            PatientID.Add(PatientID[0]);
            PatientID.RemoveAt(0);
        }//สลับโต็ะที่เล็ง
        if (Input.GetKeyDown(KeyCode.Alpha1) && haveItem)
        {
            haveItem = false;
            itemInHand = 0;
            OffVisibility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !haveItem)
        {
            haveItem = true;
            itemInHand = 1;
            OffVisibility();
            tool[0].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !haveItem)
        {
            haveItem = true;
            itemInHand = 2;
            OffVisibility();
            tool[1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !haveItem)
        {
            haveItem = true;
            itemInHand = 3;
            OffVisibility();
            tool[2].SetActive(true);
        }
        for (int i = 0; i < PatientID.Count; i++)
        {
            Debug.Log("patient : " + PatientID[i]);
        }
        Debug.Log("haveItem : " + haveItem);
    }
}
