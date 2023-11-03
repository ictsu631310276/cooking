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
    public int deHeat;

    public int sicknessID;
    public int sicknessLevel;

    public int[] declineH;
    public float[] tiemDeclineH;
    public GameObject[] allModelSickness;
    [SerializeField] private Transform modelSicknessPoint;
    private GameObject modelSickness;
    private int sicknessLevelMo;
    private bool haveModel;

    public GameObject modelBunda;
    public Transform handPoint;//playerhand
    public bool onHand;
    public bool onBed;

    [SerializeField] private TextMeshProUGUI textHP;
    private float cooldown;
    public GameObject canva;

    public bool willTreat;
    private float opacityValue;
    private float timeFaded;
    public bool dead;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireDragon")
        {
            heat = 0;
        }
    }
    private void ChangeOpacityModel()
    {
        opacityValue = 2.55f;
        timeFaded = timeFaded + Time.deltaTime;
        if (timeFaded >= 1)
        {
            opacityValue = opacityValue - 0.25f;
            modelBunda.GetComponent<Renderer>().material.color = new Color(0, 0, 0, opacityValue);

        }
    }
    private void DieMethod()
    {
        NewSpawnNPCScript.numOfNPC--;
        ScoreManeger.score -= 10;

        Destroy(modelSickness, 0);
        animatorBunda.SetBool("die", true);
        animatorBunda.SetBool("good", false);
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());

        Destroy(gameObject, 2);
        UIManagerScript.dead++;
    }
    private void Start()
    {
        heat = 100;
        deHeat = 0;
        haveModel = false;

        onHand = false;
        onBed = false;
        cooldown = tiemDeclineH[sicknessLevel - 1];

        willTreat = false;
        dead = false;
    }
    private void Update()
    {
        textHP.text = heat.ToString();
        if (sicknessID == -1)
        {
            NewSpawnNPCScript.numOfNPC--;
            ScoreManeger.score += 10;
            handPoint = null;

            animatorBunda.SetBool("die", true);
            animatorBunda.SetBool("good", true);

            sicknessID = -2;
            Destroy(modelSickness);
            Destroy(canva);

            ChangeOpacityModel();
            Destroy(gameObject, 1f);
        }//รักษาหาย

        if (sicknessLevel > 0 && !haveModel)
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
            modelBunda.GetComponent<Renderer>().material = materialBunda[0];
            cooldown = cooldown - Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = tiemDeclineH[sicknessLevel - 1];
                heat -= declineH[sicknessLevel - 1];
            }

            if (handPoint != null)
            {
                transform.position = handPoint.transform.position;
                transform.rotation = handPoint.transform.rotation;
                canva.transform.rotation = new(handPoint.transform.rotation.x, 0f
                    , handPoint.transform.rotation.z, handPoint.transform.rotation.w);
            }
        }//อยู่บนมือ หรือเตียง
        else if (!onHand)
        {
            transform.rotation = new(0, 0, 0, 0);
            cooldown = cooldown - Time.deltaTime * 2;
            if (cooldown <= 0)
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
        }//เพิ่ม หรือ ลด
        if (heat <= 0 && !dead)
        {
            dead = true;
            if (!onHand)
            {
                DieMethod();
            }//ไม่ได้ถือ ไม่ได้อยู่บนเตียง
            else if (onHand && !onBed)
            {
                handPoint = null;

                DieMethod();
            }//บนมือ
            else if (onHand && onBed)
            {
                handPoint = null;

                DieMethod();
            }//บนเตียง
        }//ตาย
    }
}
