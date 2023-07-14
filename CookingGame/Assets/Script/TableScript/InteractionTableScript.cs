using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;

    private int itemOnTable;//ของบนโต้ะ   
    public ShowModelScript showModel;

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
        itemOnTable = 0;
        glowObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        showModel.ShowModel(itemOnTable);
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump"))&&
                    InteractionPlayerScript.haveItem)
                {
                    itemOnTable = InteractionPlayerScript.itemInHand;
                    InteractionPlayerScript.itemInHand = 0;
                    InteractionPlayerScript.haveItem = false;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) &&
                    !InteractionPlayerScript.haveItem)
                {
                    Debug.Log("Hi");
                    InteractionPlayerScript.itemInHand = itemOnTable;
                    itemOnTable = 0;
                    InteractionPlayerScript.haveItem = true;
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
    }
}
