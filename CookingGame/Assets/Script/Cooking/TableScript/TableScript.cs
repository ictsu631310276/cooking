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
                if (!InteractionPlayerScript.haveItem)//ผู้นเล่นไม่มีอาหาร //หยิบ
                {
                    if (!InteractionPlayerScript.havePlate[0])
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            InteractionPlayerScript.havePlate[0] = havePlate[0];
                            InteractionPlayerScript.havePlate[1] = havePlate[1];
                            havePlate[0] = false;
                            havePlate[1] = false;

                            InteractionPlayerScript.itemInHand = itemOnTable;
                            itemOnTable = 0;
                        }
                    }//ผู้นเล่นไม่มีจาน โต้ะมีจาน จะมีอาหารหรือไม่ก็ได้ //เก็บหมด
                    else if (InteractionPlayerScript.havePlate[0] && !havePlate[0])//ผู้เล่นมีจาน โต้ะไม่มีจาน
                    {
                        if (ingredient.itemData[ingredient.FindNumOfArray(itemOnTable)].canOnPlate || itemOnTable == 0)//มีอาหาร(ที่ใส่จานได้)
                        {
                            glowObject.SetActive(true);
                            if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                            {
                                havePlate[0] = InteractionPlayerScript.havePlate[0];
                                havePlate[1] = InteractionPlayerScript.havePlate[1];
                                InteractionPlayerScript.havePlate[0] = false;
                                InteractionPlayerScript.havePlate[1] = false;
                            }
                        }
                    }//ผู้เล่นมีจาน แต่โต้ะไม่ สะอาดหรือไม่ก็ได้ //วาง
                }//ผู้เล่นไม่มีอาหาร
                else if (InteractionPlayerScript.haveItem)//ผู้เล่นมีอาหาร //วาง
                {
                    if (ingredient.itemData[ingredient.FindNumOfArray(InteractionPlayerScript.itemInHand)].canOnPlate)//อาหารในมือวางบนจานได้
                    {
                        bool canMix = false;
                        int i = 0;
                        for (i = 0; i < ingredient.mixFood.Length; i++)
                        {
                            if (InteractionPlayerScript.itemInHand == ingredient.mixFood[i].mixfood[0]
                                || InteractionPlayerScript.itemInHand == ingredient.mixFood[i].mixfood[1])
                            {
                                canMix = true;
                                break;
                            }
                            else
                            {
                                canMix = false;
                                break;
                            }
                        }//ดูว่าผสมกันได้ไหม
                        if (!canMix)//ผสมไม่ได้
                        {
                            if (!InteractionPlayerScript.havePlate[0]
                                && itemOnTable == 0 && !havePlate[1])
                            {
                                glowObject.SetActive(true);
                                if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                {
                                    itemOnTable = InteractionPlayerScript.itemInHand;
                                    InteractionPlayerScript.itemInHand = 0;
                                }
                            }//ผู้เล่นไม่มีจาน โต้ะจะมีจานหรือไม่ก็ได้ แต่ต้องสะอาด//วาง
                            else if (InteractionPlayerScript.havePlate[0])//ผู้เล่นมีจาน //วาง
                            {
                                if (!havePlate[0] && itemOnTable == 0 && !InteractionPlayerScript.havePlate[1])//จานสะอาด
                                {
                                    glowObject.SetActive(true);
                                    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                    {
                                        havePlate[0] = InteractionPlayerScript.havePlate[0];
                                        havePlate[1] = InteractionPlayerScript.havePlate[1];
                                        InteractionPlayerScript.havePlate[0] = false;
                                        InteractionPlayerScript.havePlate[1] = false;

                                        itemOnTable = InteractionPlayerScript.itemInHand;
                                        InteractionPlayerScript.itemInHand = 0;
                                    }
                                }
                            }//โต้ะไม่มีอาหาร ไม่มีจาน //ผู้เล่นจะมีจานหรือไม่ก็ได้
                        }
                        else if(canMix)//ผสมกันได้
                        {
                            if (!InteractionPlayerScript.havePlate[0])//ผู้เล่นไม่มีจาน
                            {
                                if (!havePlate[1] && itemOnTable == 0)//มีจานหรือไม่ก็ได้ แต่โต้ะต้องวาง
                                {
                                    glowObject.SetActive(true);
                                    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                    {
                                        itemOnTable = InteractionPlayerScript.itemInHand;
                                        InteractionPlayerScript.itemInHand = 0;
                                    }
                                }//โต้ะจะมีจานหรือไม่ก็ได้ แต่ต้องสะอาด และว่าง //วางอาหาร
                                else if (itemOnTable != 0)//มีของบนโต้ะ
                                {
                                    if (itemOnTable == ingredient.mixFood[i].mixfood[0]
                                        || itemOnTable == ingredient.mixFood[i].mixfood[1])
                                    {
                                        glowObject.SetActive(true);
                                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                        {
                                            itemOnTable = ingredient.mixFood[i].food;
                                            InteractionPlayerScript.itemInHand = 0;
                                        }
                                    }
                                    else
                                    {
                                        Debug.LogError("Can't Mix");
                                    }
                                }//ผสม
                            }//ไม่มีจาน
                            else if(InteractionPlayerScript.havePlate[0])
                            {
                                if (itemOnTable != 0 && !havePlate[0])
                                {
                                    if (itemOnTable == ingredient.mixFood[i].mixfood[0]
                                        || itemOnTable == ingredient.mixFood[i].mixfood[1])
                                    {
                                        glowObject.SetActive(true);
                                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                        {
                                            itemOnTable = ingredient.mixFood[i].food;
                                            InteractionPlayerScript.itemInHand = 0;

                                            havePlate[0] = true;
                                            InteractionPlayerScript.havePlate[0] = false;
                                        }
                                    }
                                    else
                                    {
                                        Debug.LogError("Can't Mix");
                                    }
                                }//โต้ะไม่มีจาน
                                else if (itemOnTable != 0 && havePlate[0])
                                {
                                    if (itemOnTable == ingredient.mixFood[i].mixfood[0]
                                        || itemOnTable == ingredient.mixFood[i].mixfood[1])
                                    {
                                        glowObject.SetActive(true);
                                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                        {
                                            itemOnTable = ingredient.mixFood[i].food;
                                            InteractionPlayerScript.itemInHand = 0;
                                        }
                                    }
                                }//โต้ะมีจาน
                                else if (itemOnTable == 0)
                                {
                                    glowObject.SetActive(true);
                                    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                                    {
                                        itemOnTable = InteractionPlayerScript.itemInHand;
                                        InteractionPlayerScript.itemInHand = 0;

                                        havePlate[0] = true;
                                        InteractionPlayerScript.havePlate[0] = false;
                                    }
                                }
                            }//มีจาน
                        }
                    }
                    else if (!havePlate[0] && itemOnTable == 0)
                    {
                        glowObject.SetActive(true);
                        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        {
                            itemOnTable = InteractionPlayerScript.itemInHand;
                            InteractionPlayerScript.itemInHand = 0;
                        }
                    }//อาหารวางบนจานไม่ได้ //ไม่มีจาน //วาง
                }
            }
            else
            {
                glowObject.SetActive(false);
            }
        }
    }
}
