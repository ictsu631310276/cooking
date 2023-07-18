using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairCustomerScript : MonoBehaviour
{
    public int idChair;
    public bool haveSit;

    public NPCDataScript NPC;
    private int foodWant;

    public int itemGet = 0;
    public ShowMoodScript showMood;
    public bool finishedEating = false;
    private float timeEat = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Customer")
        {
            haveSit = true;
            NPC = other.gameObject.GetComponent<NPCDataScript>();
            foodWant = NPC.itemNPCWant;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Customer")
        {
            NPC = null;
            haveSit = false;
            foodWant = 0;
        }
    }
    private void Update()
    {
        if (itemGet != 0)
        {
            finishedEating = false;
            if (itemGet == foodWant)
            {
                showMood.ShowMood(1);
            }
            else if (itemGet != foodWant)
            {
                showMood.ShowMood(-1);
            }
            timeEat = timeEat + Time.deltaTime;
            if (timeEat >= showMood.timeEat)
            {
                timeEat = 0;
                finishedEating = true;
                itemGet = 0;
            }
        }
    }
}
