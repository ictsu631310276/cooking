 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public int id;
    public PatientDataScript NPCData;
    [SerializeField] private PotionDataScript potionData;
    public GameObject glowObj;
    public bool haveSit;

    private bool haveMinigame;
    public Transform handPoint;
    [SerializeField] private GameObject minigameObj;
    [SerializeField] private miniGame minigame;
    private bool onMinigame;

    public int itemId;
    public int arrowAdd;
    public int treatTheSick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patient" && NPCData == null)
        {
            if (treatTheSick < 0)
            {
                AddPatient(other.gameObject);
            }//ถ้าต่ำกว่า 0 จะรักษาแบบไม่สนโรค
            else if (treatTheSick > 0)
            {
                if (treatTheSick == other.gameObject.GetComponent<PatientDataScript>().sicknessID)
                {
                    AddPatient(other.gameObject);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && onMinigame)
        {
            CloseMinigame();
        }
        if (other.tag == "Patient")
        {
            RemovePiatent();
        }
    }
    private void AddPatient(GameObject other)
    {
        NPCData = other.GetComponent<PatientDataScript>();

        NPCData.handPoint = handPoint;
        NPCData.onHand = true;
        NPCData.onBed = true;
        haveSit = true;

        minigame.notRhythm.difficulty = NPCData.sicknessLevel;
        PlayMinigame();
    }
    private void Goodbye()
    {
        NPCData.sicknessID = -1;
        haveMinigame = false;
        minigame.notRhythm.difficulty = -1;
        haveSit = false;
        NPCData = null;
        minigame.notRhythm.ClearRhythm();
        CloseMinigame();
    }
    private void RemovePiatent()
    {
        haveMinigame = false;
        minigame.notRhythm.difficulty = -1;
        haveSit = false;
        NPCData = null;
        minigame.notRhythm.ClearRhythm();
    }
    public void PlayMinigame()
    {
        onMinigame = true;
        minigameObj.SetActive(true);
        if (NPCData != null && !haveMinigame)
        {
            haveMinigame = true;
            for (int i = 0; i < potionData.sicknessData[potionData.FindNumOfSick(NPCData.sicknessID)].patternPress.Length; i++)
            {
                minigame.notRhythm.intAllArrow.Add(potionData.sicknessData[potionData.FindNumOfSick(NPCData.sicknessID)].patternPress[i]);
            }
        }
    }
    public void CloseMinigame()
    {
        minigameObj.SetActive(false);
        onMinigame = false;
    }
    private void Start()
    {
        haveMinigame = false;
        haveSit = false;
        onMinigame = false;
        minigameObj.SetActive(false);

        itemId = 0;
        arrowAdd = 5;
        if (treatTheSick == 0)
        {
            Debug.Log("Add treatTheSick in bed " + id);
        }
    }
    private void Update()
    {
        if (NPCData != null)
        {
            NPCData.willTreat = (minigame.notRhythm.buttonPressed != 0) ? true : false;
            if (treatTheSick > 0 && treatTheSick != NPCData.sicknessID)
            {
                RemovePiatent();
            }//เตียงไม่ตรงโรค
            else if (itemId == 99)
            {
                Goodbye();

                itemId = 0;
                Destroy(handPoint.GetChild(0).gameObject, 0);
                UIManagerScript.treated++;
            }//ยาวิเศษ
        }
        if (NPCData == null)
        {
            CloseMinigame();
            RemovePiatent();
        }
        if (minigame.notRhythm.difficulty == 0 && NPCData != null)
        {
            //if (NPCData.sicknessID != 1 && NPCData.sicknessLevel != -1)
            //{
            //    itemId = 0;
            //    Destroy(handPoint.GetChild(0).gameObject, 0);
            //    minigame.difficulty = 1;
            //    NPCData.declineH = potionData.sicknessData[0].declineLife;
            //    NPCData.tiemDeclineH = potionData.sicknessData[0].timeToDeclineLife;
            //    NPCData.allModelSickness = potionData.sicknessData[0].modleSickness;
            //    if (NPCData.sicknessID == 2)
            //    {
            //        NPCData.sicknessID = 1;
            //        minigame.ClearRhythm();
            //        minigame.difficulty = 1;
            //        haveMinigame = false;
            //        PlayMinigame();
            //        CloseMinigame();
            //    }
            //    else
            //    {
            //        NPCData.sicknessID = 1;
            //        NPCData.handPoint = null;
            //        NPCData.onBed = false;
            //        NPCData.onHand = false;
            //        RemovePiatent();
            //    }
            //}//รักษาโรคปกติ
            //else
            //{
                Goodbye();   
            if (itemId != 0)
            {
                itemId = 0;
                Destroy(handPoint.GetChild(0).gameObject, 0);
            }
                UIManagerScript.treated++;
            //}//รักษาหาย
        }//เรื่องรักษา
        if (minigame.notRhythm.deHeat != 0 && NPCData != null)
        {
            if (NPCData.heat <= minigame.notRhythm.deHeat * -1)
            {
                NPCData.sicknessLevel = 1;
                NPCData.deHeat = minigame.notRhythm.deHeat;
                CloseMinigame();
                minigame.notRhythm.ClearRhythm();
                //NPCData = null;
                //haveSit = false;

                itemId = 0;
                Destroy(handPoint.GetChild(0).gameObject, 0);
                //bedDirty = true;
            }
            else
            {
                NPCData.deHeat = minigame.notRhythm.deHeat;
            }
            minigame.notRhythm.deHeat = 0;
        }//ได้รับความเสียหาย
        if (NPCData != null && minigame.notRhythm.difficulty != NPCData.sicknessLevel)
        {
            NPCData.sicknessLevel = minigame.notRhythm.difficulty;
        }
        
        if (arrowAdd != 5)
        {
            minigame.notRhythm.arrowAdd = arrowAdd;
            if (NPCData != null)
            {
                NPCData.animatorBunda.SetInteger("treat", arrowAdd);
            }
            arrowAdd = 5;
        }
    }
}
