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

    public GameObject Obj;
    public GameObject glowObj;
    public Transform handPoint;//playerhand1
    public bool onHand;
    public bool onBed;

    [SerializeField] private TextMeshProUGUI textHP;
    [SerializeField] private float cooldown;
    private float cooldownMax;

    public bool willTreat;
    public bool dead;

    private void Start()
    {
        heat = 100;
        deHeat = 0;
        haveModel = false;

        onHand = false;
        onBed = false;
        cooldownMax = cooldown;
        Obj.SetActive(true);
        glowObj.SetActive(false);

        willTreat = false;
        dead = false;
    }
    private void Update()
    {
        textHP.text = heat.ToString();
        
        if (sicknessID == -1)
        {
            NewSpawnNPCScript.numOfNPC--;
            UIManagerScript.score += 10;
            handPoint = null;
            Destroy(gameObject, 0);
        }//รักษาหาย
        if (sicknessLevel > 0 && !haveModel)
        {
            modelSickness = Instantiate(allModelSickness[sicknessLevel - 1], modelSicknessPoint, false);
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
            Obj.SetActive(true);
            glowObj.SetActive(false);
            cooldown = cooldown - Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                heat -= declineH[sicknessLevel - 1];
            }
            if (handPoint != null)
            {
                transform.position = handPoint.transform.position;
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
        }//เพิ่ม หรือ ตาย
        if (heat <= 0)
        {
            if (!onHand)
            {
                Destroy(gameObject, 0);
            }//ไม่ได้ถือ ไม่ได้อยู่บนเตียง
            else if (onHand && !onBed)
            {
                NewSpawnNPCScript.numOfNPC--;
                UIManagerScript.score -= 10;
                handPoint = null;
                Destroy(gameObject, 0);
                UIManagerScript.dead++;
            }//บนมือ
            else if (onHand && onBed)
            {
                dead = true;
                NewSpawnNPCScript.numOfNPC--;
                UIManagerScript.score -= 10;
                UIManagerScript.dead++;
                handPoint = null;
                Destroy(gameObject, 0);
            }//บนเตียง
        }//ตาย
    }
}
