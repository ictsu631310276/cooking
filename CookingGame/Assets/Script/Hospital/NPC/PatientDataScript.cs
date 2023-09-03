using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatientDataScript : MonoBehaviour
{
    public int id;
    public int heat;
    public int deHeat;
    public int sicknessID;
    public int sicknessLevel;

    [SerializeField] private GameObject glowObj;
    public GameObject handPoint;
    private bool onHand;

    [SerializeField] private TextMeshProUGUI textHP;
    [SerializeField] private float cooldown;
    private float cooldownMax;

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
    private void WillDestroy()
    {
        for (int i = 0; i < ToolPlayerScript.PatientID.Count; i++)
        {
            if (ToolPlayerScript.PatientID[i].id == id)
            {
                ToolPlayerScript.PatientID.RemoveAt(i);
                break;
            }
        }
        Destroy(gameObject, 0);
    }
    private void Start()
    {
        heat = 100;
        deHeat = 0;
        sicknessID = 0;
        sicknessLevel = 1;

        onHand = false;
        cooldownMax = cooldown;
        glowObj.SetActive(false);
    }
    private void Update()
    {
        textHP.text = "HP : " + heat;
        if (ToolPlayerScript.PatientID.Count > 0)
        {
            if (id == ToolPlayerScript.PatientID[0].id)
            {
                if (!ToolPlayerScript.havePatient && !onHand /*&& !ToolPlayerScript.haveToola*/)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        onHand = true;
                        ToolPlayerScript.havePatient = true;
                    }
                }//หยิบ
                else if (onHand && ToolPlayerScript.bed.Count == 0 && ToolPlayerScript.havePatient)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        onHand = false;
                        ToolPlayerScript.havePatient = false;
                    }
                }//วางพื้น
                else if (onHand && ToolPlayerScript.bed.Count > 0 && !ToolPlayerScript.bed[0].haveSit && ToolPlayerScript.havePatient)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        handPoint = ToolPlayerScript.bed[0].handPoint;
                        ToolPlayerScript.havePatient = false;
                        ToolPlayerScript.bed[0].haveSit = true;
                    }
                }//วางบนเตียง
                else if (onHand && ToolPlayerScript.bed.Count > 0 && ToolPlayerScript.bed[0].haveSit && !ToolPlayerScript.havePatient)
                {
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        handPoint = NewSpawnNPCScript.handPlayerShare;
                        ToolPlayerScript.havePatient = true;
                        ToolPlayerScript.bed[0].haveSit = false;
                    }
                }//ยกออกจากเตียง
            }
            else
            {
                glowObj.SetActive(false);
            }
        }
        if (sicknessID == -1)
        {
            WillDestroy();
        }
        if (onHand)
        {
            transform.position = handPoint.transform.position;
            glowObj.SetActive(false);
            cooldown = cooldown - Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                heat -= 5;
            }
        }
        else if (!onHand)
        {
            cooldown = cooldown - Time.deltaTime * 2;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                heat -= 5;
            }
        }
        if (deHeat != 0)
        {
            heat = heat + deHeat;
            deHeat = 0;
            if (heat > 100 || heat < 0)
            {
                if (heat > 100)
                {
                    heat = 100;
                }
                else
                {
                    heat = 0;
                }
            }
            //Debug.Log("Heat : " + heat);
        }
        if (heat <= 0)
        {
            WillDestroy();
        }
    }
}
