using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNPCDataScript : MonoBehaviour
{
    public int id;
    public int itemNPCWant;
    [SerializeField] private GameObject glowObj;
    public GameObject handPoint;
    public bool finishedEating = false;
    private bool onHand = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NewInteractionPlayerScript.tableInteraction.Add(id);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(false);
            NewInteractionPlayerScript.tableInteraction.Remove(id);
        }
    }
    private void Start()
    {
        glowObj.SetActive(false);
    }
    private void Update()
    {
        if (NewInteractionPlayerScript.tableInteraction.Count > 0)
        {
            if (!NewInteractionPlayerScript.haveItem && id == NewInteractionPlayerScript.tableInteraction[0])
            {
                glowObj.SetActive(true);
                if (Input.GetKeyUp(KeyCode.Q) && !onHand)
                {
                    onHand = true;
                    NewInteractionPlayerScript.haveItem = true;
                }
            }
            else
            {
                glowObj.SetActive(false);
            }
        }
        if (finishedEating)
        {
            itemNPCWant = 0;
            Destroy(this, 0);
        }
        if (onHand)
        {
            transform.position = handPoint.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Q) && onHand && NewInteractionPlayerScript.bed.Count == 0)
        {
            onHand = false;
            NewInteractionPlayerScript.haveItem = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && onHand && NewInteractionPlayerScript.bed.Count > 0)
        {
            handPoint = NewInteractionPlayerScript.bed[0].handPoint;
            NewInteractionPlayerScript.haveItem = false;
        }
    }
}
