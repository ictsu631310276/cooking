using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public int spareOrgan = 0;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player" && InteractionPlayerScript.itemInHand != 0)
    //    {
    //        glowObject.SetActive(true);
    //        InteractionPlayerScript.tableInteraction.Add(idTable);
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        glowObject.SetActive(false);
    //        InteractionPlayerScript.tableInteraction.Remove(idTable);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            glowObject.SetActive(true);
            ToolPlayerScript.idTable.Add(idTable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            glowObject.SetActive(false);
            ToolPlayerScript.idTable.Remove(idTable);
        }
    }
    private void Start()
    {
        glowObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable &&
                InteractionPlayerScript.itemInHand != 0)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")))
                {
                    InteractionPlayerScript.itemInHand = 0;
                    InteractionPlayerScript.haveItem = false;
                    glowObject.SetActive(false);
                }
            }
            else
            {
                glowObject.SetActive(false);
            }
        }
        if (ToolPlayerScript.idTable.Count > 0)
        {
            if (ToolPlayerScript.haveItem && ToolPlayerScript.idTable[0] == idTable)
            {
                glowObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ToolPlayerScript.haveItem = false;
                    ToolPlayerScript.PatientID[0].sicknessID = -1;
                    ToolPlayerScript.PatientID.RemoveAt(0);
                    spareOrgan++;
                }
            }
        }
    }
}
