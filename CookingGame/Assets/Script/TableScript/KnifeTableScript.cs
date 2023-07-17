using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeTableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public ingredientScript ingredientScript;
    public ShowModelScript showModel;
    public TimeBarScript timeBar;

    private int itemOnTable = 0;//ของบนโต้ะ
    private bool haveItem = false;

    private float timeChopped;
    private float timeUse;
    private bool holdButtom = false;

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
    private void CanChopped()
    {
        switch (itemOnTable)
        {
            case 11:
                Chopped();
                break;
            default:
                Debug.Log("Can't Chopped");
                break;
        }
    }
    private void Chopped()
    {
        timeChopped = timeChopped + Time.deltaTime;
        timeBar.walkingTime(timeChopped, timeUse);
        if (timeChopped >= timeUse)
        {
            switch (itemOnTable)
            {
                case 11:
                    itemOnTable = 12;
                    break;
                default:
                    Debug.Log("Not Have");
                    break;
            }
            timeChopped = timeUse;
            timeBar.Showtime(false);
            timeChopped = 0;//เอา item ออก = สับใหม่
        }        
    }
    private void Start()
    {
        glowObject.SetActive(false);
        timeUse = ingredientScript.timeUseManuel;
    }
    void Update()
    {
        showModel.ShowModel(itemOnTable,false, false);
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
        {
            holdButtom = true;
        }//กดค้าง
        else if (Input.GetKeyUp(KeyCode.R) || Input.GetButtonUp("Fire2"))
        {
            holdButtom = false;
        }
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable &&
                !InteractionPlayerScript.havePlate[0])
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && !haveItem &&
                    InteractionPlayerScript.haveItem)
                {
                    itemOnTable = InteractionPlayerScript.itemInHand;
                    InteractionPlayerScript.itemInHand = 0;
                    InteractionPlayerScript.haveItem = false;
                    haveItem = true;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && haveItem &&
                    !InteractionPlayerScript.haveItem)
                {
                    InteractionPlayerScript.itemInHand = itemOnTable;
                    itemOnTable = 0;
                    haveItem = false;
                    InteractionPlayerScript.haveItem = true;

                    timeBar.Showtime(false);
                }
            }
            else
            {
                glowObject.SetActive(false);
            }
        }
        if (holdButtom && haveItem)
        {
            CanChopped();
        }
    }
}

