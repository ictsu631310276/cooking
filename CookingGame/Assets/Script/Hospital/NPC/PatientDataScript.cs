using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDataScript : MonoBehaviour
{
    public int id;
    public int itemNPCWant;
    [SerializeField] private GameObject glowObj;
    public GameObject handPoint;
    private bool onHand = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToolPlayerScript.PatientID.Add(id);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(false);
            ToolPlayerScript.PatientID.Remove(id);
        }
    }
    private void Start()
    {
        glowObj.SetActive(false);
    }
    private void Update()
    {
        if (ToolPlayerScript.PatientID.Count > 0)
        {
            if (!ToolPlayerScript.haveItem && id == ToolPlayerScript.PatientID[0])
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
        if (itemNPCWant == 0)
        {
            Destroy(this.gameObject, 0);
        }
        if (onHand)
        {
            transform.position = handPoint.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (onHand && ToolPlayerScript.bed.Count == 0)
            {
                onHand = false;
                ToolPlayerScript.haveItem = false;
            }//วางพื้น
            else if (onHand && ToolPlayerScript.bed.Count > 0 && !ToolPlayerScript.bed[0].haveSit)
            {
                handPoint = ToolPlayerScript.bed[0].handPoint;
                ToolPlayerScript.haveItem = false;
            }//วางบนเตียง
        }
    }
}
