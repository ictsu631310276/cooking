using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDataScript : MonoBehaviour
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
            ToolPlayerScript.tableInteraction.Add(id);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(false);
            ToolPlayerScript.tableInteraction.Remove(id);
        }
    }
    private void Start()
    {
        glowObj.SetActive(false);
    }
    private void Update()
    {
        if (ToolPlayerScript.tableInteraction.Count > 0)
        {
            if (!ToolPlayerScript.haveItem && id == ToolPlayerScript.tableInteraction[0])
            {
                glowObj.SetActive(true);
                if (Input.GetKeyUp(KeyCode.Q) && !onHand)
                {
                    onHand = true;
                    ToolPlayerScript.haveItem = true;
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
        if (Input.GetKeyDown(KeyCode.Q) && onHand && ToolPlayerScript.bed.Count == 0)
        {
            onHand = false;
            ToolPlayerScript.haveItem = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && onHand && ToolPlayerScript.bed.Count > 0)
        {
            handPoint = ToolPlayerScript.bed[0].handPoint;
            ToolPlayerScript.haveItem = false;
        }
    }
}
