using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairCustomerScript : MonoBehaviour
{
    public int idChair;
    public bool haveSit;

    public NPCDataScript NPC;
    public int foodWant;    

    public int itemGet = 0;
    public ShowMoodScript showMood;
    public bool willDestroy = false;

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
    private IEnumerator SetDestroyitem(float i)
    {
        yield return new WaitForSeconds(i);
        itemGet = 0;
        willDestroy = true;
    }
    private void Update()
    {
        if (itemGet != 0)
        {
            if (itemGet == foodWant)
            {
                showMood.ShowMood(true);
            }
            else if (itemGet != foodWant)
            {
                showMood.ShowMood(false);
            }
            StartCoroutine(SetDestroyitem(showMood.timeShowMood));
        }
    }
}
