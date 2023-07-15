using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlayerScript : MonoBehaviour
{
    public static List<int> tableInteraction = new List<int>();//สำหรับโต้ะและเครื่องต่างๆ

    public static int itemInHand = 0;//ของในมือ
    public static bool haveItem = false;
    public static bool havePlate = false;
    public ShowModelScript showModel;

    private void Update()
    {
        showModel.ShowModel(itemInHand, havePlate);
        if (Input.GetKeyDown(KeyCode.E) && tableInteraction.Count >= 2)
        {
            tableInteraction.Add(tableInteraction[0]);
            tableInteraction.RemoveAt(0); 
        }//สลับโต็ะที่เล็ง
    }
}
