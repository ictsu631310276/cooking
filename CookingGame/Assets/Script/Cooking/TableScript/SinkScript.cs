using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public ingredientScript ingredientScript;//ใช้ดึงเวลาออกมา
    public ShowModelScript showModel;
    public pickupPlateScript pickupPlate;
    public TimeBarScript timeBar;
    public GameObject handPoint;
    private float timeWash = 0;
    private float timeUse;
    private bool holdButtom = false;
    private int numOfPlate = 0;

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
        pickupPlate.numOfPlate++;
        numOfPlate--;
    }
    private void Start()
    {
        timeUse = ingredientScript.timeUseManuel;
        glowObject.SetActive(false);
    }
    void Update()
    {
        if (numOfPlate > 0)
        {
            timeBar.walkingTime(timeWash, timeUse);//แสดงเวลา
            showModel.ShowModel(0, true, true);
        }//แสดงจาน
        else
        {
            showModel.ShowModel(0, false, false);
            timeBar.Showtime(false);
        }
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
                if (!InteractionPlayerScript.haveItem)
                {
                    glowObject.SetActive(true);
                    if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Jump"))
                        && InteractionPlayerScript.havePlate[0] && InteractionPlayerScript.havePlate[1])
                    {
                        InteractionPlayerScript.havePlate[0] = false;
                        InteractionPlayerScript.havePlate[1] = false;
                        numOfPlate++;
                    }
                    if (!InteractionPlayerScript.havePlate[0])
                    {
                        if (holdButtom && numOfPlate > 0)
                        {
                            timeWash = timeWash + Time.deltaTime;
                            if (timeWash >= timeUse)
                            {
                                timeWash = 0;
                                Washing();
                            }
                        }
                    }
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
                timeBar.Showtime(false);
            }
        }
    }
}

