using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenRestaurant : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public GameObject openUI;
    public GameObject closeUI;
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
    private void Update()
    {
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)
            {
                if (!TimeUI.closeDay)
                {
                    glowObject.SetActive(true);
                    if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))
                    {
                        if (!SpawnNPCScript.open)
                        {
                            SpawnNPCScript.open = true;

                        }
                        else if (SpawnNPCScript.open)
                        {
                            SpawnNPCScript.open = false;
                        }
                    }
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
        if (SpawnNPCScript.open)
        {
            openUI.SetActive(true);
            closeUI.SetActive(false);
        }
        else if (!SpawnNPCScript.open)
        {
            openUI.SetActive(false);
            closeUI.SetActive(true);
        }
    }
}
