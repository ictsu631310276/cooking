using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public int spareOrgan;
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
            ToolPlayerScript.idBin.Add(idTable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            glowObject.SetActive(false);
            ToolPlayerScript.idBin.Remove(idTable);
        }
    }
    private void Start()
    {
        glowObject.SetActive(false);
        spareOrgan = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (InteractionPlayerScript.tableInteraction.Count != 0)
        //{
        //    if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable &&
        //        InteractionPlayerScript.itemInHand != 0)
        //    {
        //        glowObject.SetActive(true);
        //        if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")))
        //        {
        //            InteractionPlayerScript.itemInHand = 0;
        //            InteractionPlayerScript.haveItem = false;
        //            glowObject.SetActive(false);
        //        }
        //    }
        //    else
        //    {
        //        glowObject.SetActive(false);
        //    }
        //}//game1
        if (ToolPlayerScript.idBin.Count > 0)
        {
            if (ToolPlayerScript.havePatient && ToolPlayerScript.idBin[0] == idTable)
            {
                glowObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ToolPlayerScript.havePatient = false;
                    ToolPlayerScript.PatientID[0].sicknessID = -1;
                    ToolPlayerScript.PatientID.RemoveAt(0);
                    spareOrgan++;
                    NewSpawnNPCScript.numOfNPC--;
                    UIManagerScript.dead++;
                }
            }
        }
    }
}
