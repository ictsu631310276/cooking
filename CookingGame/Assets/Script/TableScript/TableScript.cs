using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;

    private int itemOnTable;//ของบนโต้ะ   
    private bool havePlate;

    public ShowModelScript showModel;
    public ingredientScript ingredient;

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
    private int findNumArray(int idItem)
    {
        int j = 0;
        bool x = true;
        if (idItem != 0)
        {
            do
            {
                if (j >= ingredient.allIngredient.Length)
                {
                    x = false;
                    Debug.LogError("Have Problem in findNumArray");
                }
                else if (ingredient.allIngredient[j].id == idItem)
                {
                    x = false;
                }
                else
                {
                    j++;
                }
            } while (x);
        }
        return j;
    }
    private void Start()
    {
        itemOnTable = 0;
        glowObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        showModel.ShowModel(itemOnTable, havePlate);
        if (InteractionPlayerScript.tableInteraction.Count != 0)//มีโต้ะที่มอง
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)//โต้ะตรง
            {
                if (ingredient.allIngredient[findNumArray(itemOnTable)].canOnPlate
                   && ingredient.allIngredient[findNumArray(InteractionPlayerScript.itemInHand)].canOnPlate)//มีจานมาเกี่ยว
                {
                    if (InteractionPlayerScript.haveItem && itemOnTable == 0)
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            itemOnTable = InteractionPlayerScript.itemInHand;
                            InteractionPlayerScript.itemInHand = 0;
                            InteractionPlayerScript.haveItem = false;
                        }
                    }
                    else if(InteractionPlayerScript.havePlate && !havePlate)
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            havePlate = true;
                            InteractionPlayerScript.havePlate = false;
                        }
                    }
                    else if ((InteractionPlayerScript.haveItem && itemOnTable == 0)
                        && (InteractionPlayerScript.havePlate && !havePlate))
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            itemOnTable = InteractionPlayerScript.itemInHand;
                            InteractionPlayerScript.itemInHand = 0;
                            InteractionPlayerScript.haveItem = false;

                            havePlate = true;
                            InteractionPlayerScript.havePlate = false;
                        }
                    }
                    //else
                    //{
                    //    Debug.Log("else");
                    //    glowObject.SetActive(true);
                    //    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                    //    {
                    //        if (itemOnTable == 0 && !havePlate)
                    //        {
                    //            Debug.Log("itemOnTable");
                    //        }
                    //        else if (itemOnTable == 0 && havePlate)
                    //        {
                    //            Debug.Log("havePlate");
                    //        }
                    //        else if (itemOnTable != 0 && havePlate)
                    //        {
                    //            InteractionPlayerScript.itemInHand = itemOnTable;
                    //            itemOnTable = 0;
                    //            InteractionPlayerScript.haveItem = true;

                    //            InteractionPlayerScript.havePlate = true;
                    //            havePlate = false;
                    //        }
                    //        else
                    //        {
                    //            Debug.Log("else else");
                    //        }
                    //    }
                    //}

                    //if ((InteractionPlayerScript.haveItem && itemOnTable == 0)
                    //   && (InteractionPlayerScript.havePlate && !havePlate))//มี item และจานอยู่ในมือผู้เล่น
                    //{
                    //    glowObject.SetActive(true);
                    //    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                    //    {
                    //        if (InteractionPlayerScript.havePlate)
                    //        {
                    //            havePlate = true;
                    //            InteractionPlayerScript.havePlate = false;
                    //        }
                    //        if (InteractionPlayerScript.haveItem)
                    //        {
                    //            itemOnTable = InteractionPlayerScript.itemInHand;
                    //            InteractionPlayerScript.itemInHand = 0;
                    //            InteractionPlayerScript.haveItem = false;
                    //        }
                    //    }
                    //}
                    //else if ((!InteractionPlayerScript.haveItem)
                    //   || (!InteractionPlayerScript.havePlate && havePlate))//ไม่มี item และจานอยู่ในมือผู้เล่น
                    //{
                    //    glowObject.SetActive(true);
                    //    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                    //    {
                    //        if (havePlate)
                    //        {
                    //            havePlate = false;
                    //            InteractionPlayerScript.havePlate = true;
                    //        }
                    //        if (itemOnTable != 0)
                    //        {
                    //            InteractionPlayerScript.itemInHand = itemOnTable;
                    //            itemOnTable = 0;
                    //            InteractionPlayerScript.haveItem = true;
                    //        }
                    //    }
                    //}
                }
                else
                {
                    if (!havePlate && !InteractionPlayerScript.havePlate)
                    {
                        glowObject.SetActive(true);
                        if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) &&
                            itemOnTable == 0)
                        {
                            itemOnTable = InteractionPlayerScript.itemInHand;
                            InteractionPlayerScript.itemInHand = 0;
                            InteractionPlayerScript.haveItem = false;
                        }
                        else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) &&
                            !InteractionPlayerScript.haveItem)
                        {
                            InteractionPlayerScript.itemInHand = itemOnTable;
                            itemOnTable = 0;
                            InteractionPlayerScript.haveItem = true;
                        }
                    }
                }//item ที่อยู่้ในมือ และ บนโต้ะ ใส่จานไม่ได้มาลูปนี้
            }
            else
            {
                glowObject.SetActive(false);
            }
        }
    }
}
