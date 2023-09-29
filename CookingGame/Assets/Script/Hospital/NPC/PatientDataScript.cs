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
    public int[] declineH;
    public GameObject[] allModelSickness;
    [SerializeField] private Transform modelSicknessPoint;
    private GameObject modelSickness;
    private int sicknessLevelMo;
    private bool haveModel;

    [SerializeField] private GameObject Obj;
    [SerializeField] private GameObject glowObj;
    public GameObject handPoint;//playerhand1
    public GameObject handPoint2;//playerhand2
    private bool onHand;

    [SerializeField] private TextMeshProUGUI textHP;
    [SerializeField] private float cooldown;
    private float cooldownMax;

    public bool willTreat;
    public bool dead;

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
            Obj.SetActive(true);
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
        if (onHand)
        {
            ToolPlayerScript.havePatient = false;
            onHand = false;
        }
        Destroy(gameObject, 0);
    }
    private void Start()
    {
        heat = 100;
        deHeat = 0;
        haveModel = false;

        onHand = false;
        cooldownMax = cooldown;
        Obj.SetActive(true);
        glowObj.SetActive(false);

        willTreat = false;
        dead = false;
    }
    private void Update()
    {
        textHP.text = heat.ToString();
        if (ToolPlayerScript.PatientID.Count > 0)
        {
            if (id == ToolPlayerScript.PatientID[0].id)
            {
                if (!ToolPlayerScript.havePatient && !onHand /*&& !ToolPlayerScript.haveToola*/)
                {
                    Obj.SetActive(false);
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        onHand = true;
                        ToolPlayerScript.havePatient = true;
                    }
                }//หยิบ
                else if (onHand && ToolPlayerScript.bed.Count == 0 && ToolPlayerScript.havePatient)
                {
                    Obj.SetActive(false);
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        onHand = false;
                        transform.position = handPoint2.transform.position;
                        ToolPlayerScript.havePatient = false;
                    }
                }//วางพื้น
                else if (onHand && ToolPlayerScript.bed.Count > 0 && !ToolPlayerScript.bed[0].haveSit && ToolPlayerScript.havePatient)
                {
                    Obj.SetActive(false);
                    glowObj.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        handPoint = ToolPlayerScript.bed[0].handPoint;
                        ToolPlayerScript.havePatient = false;
                        ToolPlayerScript.bed[0].haveSit = true;
                    }
                }//วางบนเตียง
                else if (onHand && ToolPlayerScript.bed.Count > 0 && ToolPlayerScript.bed[0].haveSit && !ToolPlayerScript.havePatient && !willTreat)
                {
                    Obj.SetActive(false);
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
                Obj.SetActive(true);
                glowObj.SetActive(false);
            }
        }
        if (sicknessID == -1)
        {
            UIManagerScript.score += 10;
            WillDestroy();
        }
        if (sicknessLevel > 0 && !haveModel)
        {
            modelSickness = Instantiate(allModelSickness[sicknessLevel], modelSicknessPoint, false);
            sicknessLevelMo = sicknessLevel;
            haveModel = true;
        }
        if (sicknessLevelMo != sicknessLevel)
        {
            Destroy(modelSickness, 0);
            haveModel = false;
        }

        if (onHand)
        {
            transform.position = handPoint.transform.position;
            Obj.SetActive(true);
            glowObj.SetActive(false);
            cooldown = cooldown - Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                heat -= declineH[sicknessLevel - 1];
            }
        }
        else if (!onHand)
        {
            cooldown = cooldown - Time.deltaTime * 2;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                heat -= declineH[sicknessLevel - 1];
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
        if (dead || heat <= 0)
        {
            UIManagerScript.score -= 10;
            WillDestroy();
            UIManagerScript.dead++;
        }
    }
}
