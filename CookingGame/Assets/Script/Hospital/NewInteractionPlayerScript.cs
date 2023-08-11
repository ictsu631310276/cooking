using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInteractionPlayerScript : MonoBehaviour
{
    public static List<int> tableInteraction = new List<int>();//สำหรับโต้ะและเครื่องต่างๆ
    public static List<BedScript> bed = new List<BedScript>();
    public static int itemInHand = 0;//ของในมือ
    public static bool haveItem = false;
    public Transform handPoint;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && tableInteraction.Count >= 2)
        {
            tableInteraction.Add(tableInteraction[0]);
            tableInteraction.RemoveAt(0); 
        }//สลับโต็ะที่เล็ง
        Debug.Log(bed.Count);
    }
}
