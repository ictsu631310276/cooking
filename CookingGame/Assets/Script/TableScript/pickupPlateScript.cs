using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPlateScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public int numOfPlate = 1;
    public int maxOfPlate = 4;
    public ShowPlatelScript showModel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && InteractionPlayerScript.itemInHand == 0)
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
        showModel.ShowModel(numOfPlate);
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable &&
                !InteractionPlayerScript.haveItem)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Jump")) && numOfPlate > 0 &&
                    InteractionPlayerScript.havePlate == false)
                {
                    InteractionPlayerScript.havePlate = true;
                    numOfPlate--;
                    glowObject.SetActive(false);
                }
                else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Jump")) && numOfPlate < maxOfPlate &&
                    InteractionPlayerScript.havePlate == true && InteractionPlayerScript.haveItem == false)
                {
                    InteractionPlayerScript.havePlate = false;
                    numOfPlate++;
                    glowObject.SetActive(false);
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }

    }
}
