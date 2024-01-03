using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatientDataScript : MonoBehaviour
{
    public Animator animatorBunda;
    public Material[] materialBunda;

    public int id;
    public int heat;
    [HideInInspector] public int deHeat;

    public int sicknessID;
    public int sicknessLevel;
    [HideInInspector] public int[] declineH;
    [HideInInspector] public float[] tiemDeclineH;
    [HideInInspector] public GameObject[] allModelSickness;

    [SerializeField] private Transform modelSicknessPoint;
    private GameObject modelSickness;
    private int sicknessLevelMo;
    private bool haveModel;

    public GameObject modelBunda;
    [HideInInspector] public Transform handPoint;//playerhand
    public bool onHand;
    public bool onBed;
    private float cooldown;
    [HideInInspector] public bool willTreat;
    [HideInInspector] public bool dead;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireDragon")
        {
            heat = 0;
        }
    }
    public void ChangeColorModel()
    {
        if (heat < 60 && heat > 31)
        {
            modelBunda.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (heat < 30 && heat > 0)
        {
            modelBunda.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (heat > 51 || heat <= 0)
        {
            modelBunda.GetComponent<Renderer>().material.color = Color.white;
        }
    }//เลือดลด
    private void DieMethod()
    {
        NewSpawnNPCScript.numOfNPC--;
        ScoreManeger.score -= 10;

        animatorBunda.SetBool("die", true);
        animatorBunda.SetBool("good", false);
        sicknessLevel = 0;
        sicknessID = 1;

        UIManagerScript.dead++;
    }//เป็นศพ
    private void Start()
    {
        //heat = 100;
        deHeat = 0;
        haveModel = false;

        onHand = false;
        onBed = false;
        cooldown = tiemDeclineH[sicknessLevel - 1];

        willTreat = false;
        dead = false;
        animatorBunda.SetInteger("treat", 5);
        modelBunda.GetComponent<Renderer>().material = materialBunda[0];
    }
    private void Update()
    {
        ChangeColorModel();
        if (sicknessID == -1)
        {
            NewSpawnNPCScript.numOfNPC--;
            ScoreManeger.score += 10;
            handPoint = null;

            animatorBunda.SetBool("die", true);
            animatorBunda.SetBool("good", true);

            sicknessID = -2;
            Destroy(modelSickness);
            Destroy(gameObject, 1f);
        }//รักษาหาย

        if (sicknessLevel > 0 && !haveModel && !dead)
        {
            modelSickness = Instantiate(allModelSickness[sicknessLevel - 1], modelSicknessPoint, false);
            sicknessLevelMo = sicknessLevel;
            haveModel = true;
        }
        else if (sicknessLevelMo != sicknessLevel)
        {
            Destroy(modelSickness, 0);
            haveModel = false;
        }
        else if (sicknessID == 1)
        {
            Destroy(modelSickness, 0);
            haveModel = false;
        }//โมเดลโรค

        if (onHand)
        {
            if (handPoint != null)
            {
                transform.position = handPoint.transform.position;
                transform.rotation = handPoint.transform.rotation;
            }

            modelBunda.GetComponent<Renderer>().material = materialBunda[0];
            ChangeColorModel();
            cooldown = cooldown - Time.deltaTime;
            if (cooldown <= 0 && sicknessLevel >= 1)
            {
                cooldown = tiemDeclineH[sicknessLevel - 1];
                heat -= declineH[sicknessLevel - 1];
            }
        }//อยู่บนมือ หรือเตียง
        else if (!onHand)
        {
            transform.rotation = new(0, 0, 0, 0);
            cooldown = cooldown - Time.deltaTime * 2;
            if (cooldown <= 0 && sicknessLevel >= 1)
            {
                cooldown = tiemDeclineH[sicknessLevel - 1];
                heat -= declineH[sicknessLevel - 1];
            }
        }//อยู่ที่พื้น

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
            ChangeColorModel();
        }//เพิ่ม หรือ ลด
        if (heat <= 0 && !dead)
        {
            sicknessLevel = 1;
            dead = true;
            if (!onHand)
            {
                DieMethod();
            }//ไม่ได้ถือ ไม่ได้อยู่บนเตียง
            else if (onHand && !onBed)
            {
                //handPoint = null;

                DieMethod();
            }//บนมือ
            else if (onHand && onBed)
            {
                //handPoint = null;

                DieMethod();
            }//บนเตียง
        }//ตาย
    }
}
