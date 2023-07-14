using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPointScript : MonoBehaviour
{
    public int idTable;
    public int iditem;
    public GameObject glowObject;
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
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable &&
                !InteractionPlayerScript.haveItem)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Jump")))
                {
                    InteractionPlayerScript.itemInHand = iditem;
                    InteractionPlayerScript.haveItem = true;
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
