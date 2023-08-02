using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public GameObject fridgeUI;
    public GameObject recipeButton;
    public static bool openUI = false;
    private int open;

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
            openUI = false;
            CameraScript.zoomOut = true;
        }
    }
    private void Start()
    {
        recipeButton.transform.position = new Vector3(1820,980,0);
    }
    private void Update()
    {
        if (openUI)
        {
            open = -2;
            if (fridgeUI.GetComponent<Transform>().position.x <= 950)
            {
                open = 0;
            }
        }
        else if (!openUI)
        {
            open = 2;
            if (fridgeUI.GetComponent<Transform>().position.x >= 1800)
            {
                open = 0;
            }
        }
        fridgeUI.transform.Translate(790 * Time.deltaTime * open, 0, 0);
        recipeButton.transform.Translate(0, -1 * 790 * Time.deltaTime * open, 0);
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable &&
                !InteractionPlayerScript.haveItem)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Jump")) && !openUI)
                {
                    openUI = true;
                    CameraScript.zoomOut = false;
                }
                else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Jump")) && openUI)
                {
                    openUI = false;
                    CameraScript.zoomOut = true;
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
    }
}
