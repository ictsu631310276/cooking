using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodServingTableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;

    private int itemOnTable;//ของบนโต้ะ
    private bool[] havePlate = new bool[] { false, false };
    public ShowModelScript showModel;
    public ingredientScript ingredient;
    public ChairCustomerScript chair;

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
    private void Start()
    {
        itemOnTable = 0;
        glowObject.SetActive(false);
    }

    private void Update()
    {
        showModel.ShowModel(itemOnTable, havePlate[0], havePlate[1]);
        if (InteractionPlayerScript.tableInteraction.Count != 0)//มีโต้ะที่มอง
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)//โต้ะตรง
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                   && InteractionPlayerScript.itemInHand != 0//ผู้เล่นมีอาหาร
                   && InteractionPlayerScript.havePlate[0]//ผู้เล่นมีจาน
                   && !havePlate[0])//โต้ะไม่มีจาน 
                {
                    itemOnTable = InteractionPlayerScript.itemInHand;
                    InteractionPlayerScript.itemInHand = 0;

                    InteractionPlayerScript.haveItem = false;
                    InteractionPlayerScript.havePlate[0] = false;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                    && InteractionPlayerScript.itemInHand == 0//ผู้เล่นไม่มีอาหาร
                    && !InteractionPlayerScript.havePlate[0]//ผู้เล่นไมีจาน
                    && havePlate[0])//โต้ะมีจาน
                {
                    InteractionPlayerScript.havePlate[0] = true;
                    InteractionPlayerScript.havePlate[1] = true;
                    havePlate[0] = false;
                    havePlate[1] = false;
                }
            }
            else
            {
                glowObject.SetActive(false);
            }
        }
        if (itemOnTable != 0 && !chair.willDestroy)
        {
            havePlate[0] = true;
            chair.itemGet = itemOnTable;
        }//วางอาหาร
        if (chair.willDestroy)
        {
            havePlate[0] = true;
            havePlate[1] = true;
            itemOnTable = 0;
            chair.willDestroy = false;
        }
    }
}
