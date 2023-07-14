using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public ingredientScript ingredientScript;//ใช้ดึงเวลาออกมา

    private int itemInHand = 0;//ของบนโต้ะ
    public GameObject handPoint;
    private GameObject itemModel;
    private float timeWash;
    private float timeUse;
    private bool holdButtom = false;

    public Slider timeBar;
    public GameObject timeBarUI;

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
    
    private void Washing()
    {
        switch (itemInHand)
        {
            case 1 :
                itemInHand = 2;
                timeWash = timeUse;
                break;
            default:
                break;
        }
        Destroy(itemModel, 0);
    }
    private void Showtime(float i)
    {
        timeBarUI.SetActive(true);
        timeBar.value = i;
        if (i >= timeUse)
        {
            timeBarUI.SetActive(false);
        }
    }
    private void Start()
    {
        glowObject.SetActive(false);
        timeBarUI.SetActive(false);
        timeUse = ingredientScript.timeUseManuel;
    }
    void Update()
    {
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
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) )
                {
                    itemInHand = InteractionPlayerScript.itemInHand;

                    InteractionPlayerScript.itemInHand = 0;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) )
                {
                    InteractionPlayerScript.itemInHand = itemInHand;
                    itemInHand = 0;
                    timeBarUI.SetActive(false);
                    timeWash = 0;//เอา item ออก = สับใหม่
                }
                if (holdButtom)
                {
                    timeWash = timeWash + Time.deltaTime;
                    Showtime(timeWash);
                    if (timeWash >= timeUse)
                    {
                        Washing();
                    }
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
    }
}

