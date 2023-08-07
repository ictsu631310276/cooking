using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairCustomerScript : MonoBehaviour
{
    public int idChair;
    public bool haveSit;
    public Transform positionChair;

    public NPCDataScript NPC;
    public int itemWant;
    public int itemGet = 0;
    public OrderUIScript order;

    public ShowMoodScript showMood;
    public bool finishedEating = false;
    public float timeEat;
    private float eat = 0;
    public float timeShowMood;
    private float mood = 0;
    private bool lookItem = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Customer")
        {
            haveSit = true;
            NPC = other.gameObject.GetComponent<NPCDataScript>();
            itemWant = NPC.itemNPCWant;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Customer")
        {
            NPC = null;
            haveSit = false;
            itemWant = 0;
        }
    }
    private void Start()
    {
        positionChair = gameObject.transform;
    }
    private void Update()
    {
        if (haveSit && itemGet == 0)
        {
            showMood.ShowItemWant(itemWant);
            if (NPC != null)
            {
                if (order.orderIDItem.Count <= NPC.iDNPC)
                {
                    order.orderIDItem.Add(itemWant);
                }
            }
        }
        else if (!haveSit)
        {
            showMood.ShowItemWant(0);
        }
        if (itemGet != 0)
        {
            showMood.ShowItemWant(0);
            order.CreateOrderUI();
            order.orderIDItem[NPC.iDNPC] = 0;
            finishedEating = false;
            if (!lookItem)
            {
                if (itemGet == itemWant)
                {
                    showMood.ShowMood(2);
                    UIManagerScript.money += 10;
                }
                else if (itemGet != itemWant)
                {
                    showMood.ShowMood(1);
                    UIManagerScript.money -= 10;
                }
                lookItem = true;
            }
            eat = eat + Time.deltaTime;
            if (eat >= timeEat)
            {
                eat = 0;
                itemWant = 0;
                itemGet = 0;
                lookItem = false;
                NPC.itemNPCWant = 0;
                NPC.finishedEating = true;
                finishedEating = true;
                mood = 0;
            }
            mood = mood + Time.deltaTime;
            if (mood >= timeShowMood)
            {
                showMood.CloseMood();
            }
        }
    }
}

