using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    [SerializeField]
    private int itemOnTable;//ของบนโต้ะ   
    [SerializeField]
    private bool[] havePlate = new bool[] { false, false };

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
        glowObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        showModel.ShowModel(itemOnTable, havePlate[0], havePlate[1]);
        if (InteractionPlayerScript.tableInteraction.Count != 0)//มีโต้ะที่มอง
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)//โต้ะตรง
            {
                if (ingredient.allIngredient[findNumArray(itemOnTable)].canOnPlate
                   && ingredient.allIngredient[findNumArray(InteractionPlayerScript.itemInHand)].canOnPlate)//มีจานมาเกี่ยว
                {
                    if ((InteractionPlayerScript.haveItem && InteractionPlayerScript.havePlate[0])
                        && (!havePlate[0] && itemOnTable == 0))//ผู้เล่น มีจาน และ อาหาร โต้ะ ไมีมีทั้งคู่
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            havePlate[0] = true;
                            InteractionPlayerScript.havePlate[0] = false;

                            itemOnTable = InteractionPlayerScript.itemInHand;
                            InteractionPlayerScript.itemInHand = 0;
                            InteractionPlayerScript.haveItem = false;
                        }
                    }
                    else if (InteractionPlayerScript.havePlate[0]
                        && !havePlate[0] && !InteractionPlayerScript.haveItem)//ผู้เล่นมีแต่จาน ไม่มีอาหาร โต้ะ อาจมีอาหาร หรือไม่
                    {
                        glowObject.SetActive(true);
                        if (!InteractionPlayerScript.havePlate[1])//จานสะอาด 
                        {
                            if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                            {
                                if (itemOnTable == 0)//วางจาน
                                {
                                    havePlate[0] = InteractionPlayerScript.havePlate[0];
                                    havePlate[1] = InteractionPlayerScript.havePlate[1];
                                    InteractionPlayerScript.havePlate[0] = false;
                                    InteractionPlayerScript.havePlate[1] = false;
                                }
                                else if (itemOnTable != 0)//อาหารขึ้นมือ
                                {
                                    InteractionPlayerScript.itemInHand = itemOnTable;
                                    itemOnTable = 0;
                                    InteractionPlayerScript.haveItem = true;
                                }
                            }
                        }
                        else if (InteractionPlayerScript.havePlate[1])//จานสกปรก //จะไม่มีอาหารอยู่แล้ว
                        {
                            if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                            {
                                havePlate[0] = InteractionPlayerScript.havePlate[0];
                                havePlate[1] = InteractionPlayerScript.havePlate[1];
                                InteractionPlayerScript.havePlate[0] = false;
                                InteractionPlayerScript.havePlate[1] = false;
                            }
                        }
                    }
                    else if (InteractionPlayerScript.haveItem
                        && itemOnTable == 0 && !InteractionPlayerScript.havePlate[0])//ผู้เล่นมีอาหาร ไม่มีจาน โต้ะไม่มีอาหาร
                    {
                        if (!havePlate[1])//จานบนโต้ะสะอาด
                        {
                            glowObject.SetActive(true);
                            if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                            {
                                itemOnTable = InteractionPlayerScript.itemInHand;
                                InteractionPlayerScript.itemInHand = 0;
                                InteractionPlayerScript.haveItem = false;
                            }
                        }
                    }
                    else if (!InteractionPlayerScript.haveItem && !InteractionPlayerScript.havePlate[0])//ผู้เล่นไม่มีอาหาร และจาน(มือเปล่า)
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            if (havePlate[0] && itemOnTable != 0)//บนโต้ะมีจาน มีอาหาร
                            {
                                havePlate[0] = false;
                                InteractionPlayerScript.havePlate[0] = true;

                                InteractionPlayerScript.itemInHand = itemOnTable;
                                itemOnTable = 0;
                                InteractionPlayerScript.haveItem = true;
                            }
                            else if (havePlate[0])//บนโต้ะมีจาน
                            {
                                InteractionPlayerScript.havePlate[0] = havePlate[0];
                                InteractionPlayerScript.havePlate[1] = havePlate[1];
                                havePlate[0] = false;
                                havePlate[1] = false;
                            }
                            else if (itemOnTable != 0)//บนโต้ะมีอาหาร
                            {
                                InteractionPlayerScript.itemInHand = itemOnTable;
                                itemOnTable = 0;
                                InteractionPlayerScript.haveItem = true;
                            }
                        }
                    }
                }
                else//ไม่มีจานมาเกี่ยว
                {
                    if (!havePlate[0] && !InteractionPlayerScript.havePlate[0])
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
