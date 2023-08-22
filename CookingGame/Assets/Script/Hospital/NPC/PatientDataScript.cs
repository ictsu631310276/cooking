using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDataScript : MonoBehaviour
{
    public int id;
    public int itemNPCWant;
    public int heat = 100;
    [SerializeField] private GameObject glowObj;
    public GameObject handPoint;
    private bool onHand = false;
    [SerializeField] private float cooldownMax = 5;
    [SerializeField] private float cooldown = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ToolPlayerScript.PatientID.Add(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            glowObj.SetActive(false);
            ToolPlayerScript.PatientID.Remove(this);
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
            if (id == ToolPlayerScript.PatientID[0].id)
            {
                if (!ToolPlayerScript.haveItem && !onHand)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        onHand = true;
                        ToolPlayerScript.haveItem = true;
                    }
                }//หยิบ
                else if (onHand && ToolPlayerScript.bed.Count == 0 && ToolPlayerScript.haveItem)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        onHand = false;
                        ToolPlayerScript.haveItem = false;
                    }
                }//วางพื้น
                else if (onHand && ToolPlayerScript.bed.Count > 0 && !ToolPlayerScript.bed[0].haveSit && ToolPlayerScript.haveItem)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        handPoint = ToolPlayerScript.bed[0].handPoint;
                        ToolPlayerScript.haveItem = false;
                    }
                }//วางบนเตียง
                else if (onHand && !ToolPlayerScript.haveItem && ToolPlayerScript.bed.Count > 0 && ToolPlayerScript.bed[0].haveSit)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        handPoint = NewSpawnNPCScript.handPlayerShare;
                        ToolPlayerScript.haveItem = true;
                    }
                }//ยกออกจากเตียง
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
            glowObj.SetActive(false);
        }
        else if (!onHand)
        {
            cooldown = cooldown - Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                heat -= 5;
            }
        }
    }
}
