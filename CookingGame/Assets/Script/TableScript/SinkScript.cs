using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public ingredientScript ingredientScript;//ใช้ดึงเวลาออกมา

    public GameObject handPoint;
    private float timeWash;
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
    
    private void Washing()
    {

    }
    private void Start()
    {
        glowObject.SetActive(false);
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
                if (holdButtom)
                {
                    timeWash = timeWash + Time.deltaTime;
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

