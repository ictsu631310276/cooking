using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDataScript : MonoBehaviour
{
    public int iDNPC;
    public int itemNPCWant;
    public bool finishedEating = false;
    private void Update()
    {
        if (finishedEating)
        {
            itemNPCWant = 0;
        }
    }
}
