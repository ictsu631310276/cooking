using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodServingTableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;

    private int itemOnTable;//ของบนโต้ะ
    private bool havePlate;
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
        showModel.ShowModel(itemOnTable, havePlate);
        if (InteractionPlayerScript.tableInteraction.Count != 0)//มีโต้ะที่มอง
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)//โต้ะตรง
            {
                if (InteractionPlayerScript.havePlate)
                {
                    glowObject.SetActive(true);
                    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                    {
                        itemOnTable = InteractionPlayerScript.itemInHand;
                        InteractionPlayerScript.itemInHand = 0;

                        InteractionPlayerScript.haveItem = false;
                        InteractionPlayerScript.havePlate = false;
                    }
                }
            }
            else
            {
                glowObject.SetActive(false);
            }
        }
        if (itemOnTable != 0 && !chair.willDestroy)
        {
            havePlate = true;
            chair.itemGet = itemOnTable;
        }//วางอาหาร
        if (chair.willDestroy)
        {
            itemOnTable = 0;
            havePlate = false;
            chair.willDestroy = false;
        }
    }
}
