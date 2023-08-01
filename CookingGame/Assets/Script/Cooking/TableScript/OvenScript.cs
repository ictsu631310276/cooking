using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public ingredientScript ingredientScript;
    public ShowModelScript showModel;
    public TimeBarScript timeBar;
    public procesIngredientScript processing;

    private int itemInOven = 0;//ของบนโต้ะ
    private bool haveItem = false;

    private float timeOven;
    private float timeUse;
    private bool finished = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObject.SetActive(true);
            InteractionPlayerScript.tableInteraction.Add(idTable);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObject.SetActive(false);
            InteractionPlayerScript.tableInteraction.Remove(idTable);
        }
    }
    private void CanBaking()
    {
        int i;
        for (i = 0; i < processing.proces.Length; i++)
        {
            if (itemInOven == processing.proces[i].substrate)
            {
                finished = false;
                break;
            }
        }
        ChangeItem(i);
    }
    private void ChangeItem(int i)
    {
        if (haveItem && !finished)
        {
            timeOven = timeOven + Time.deltaTime;
            timeBar.walkingTime(timeOven, timeUse);
            if (timeOven >= timeUse)
            {
                finished = true;
                itemInOven = processing.proces[i].result;
            }
        }
    }
    private void Start()
    {
        glowObject.SetActive(false);
        timeUse = ingredientScript.timeUseManuel;
    }
    void Update()
    {
        showModel.ShowModel(itemInOven,false,false);
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && !haveItem &&
                    InteractionPlayerScript.haveItem)
                {
                    itemInOven = InteractionPlayerScript.itemInHand;
                    InteractionPlayerScript.itemInHand = 0;
                    InteractionPlayerScript.haveItem = false;
                    haveItem = true;

                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && haveItem &&
                    !InteractionPlayerScript.haveItem &&
                    !InteractionPlayerScript.havePlate[1])
                {
                    InteractionPlayerScript.itemInHand = itemInOven;
                    itemInOven = 0;
                    InteractionPlayerScript.haveItem = true;
                    haveItem = false;

                    timeBar.Showtime(false);
                    timeOven = 0;//เอา item ออก = สับใหม่
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
        if (haveItem)
        {
            CanBaking();
        }
    }
}

