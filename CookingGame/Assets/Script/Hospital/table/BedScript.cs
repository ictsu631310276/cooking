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
    [SerializeField] private Not2Rhythm minigame;
    private bool onMinigame;

    [SerializeField] private GameObject bedDirtyModel;
    private float timeCheck;
    public bool bedDirty;

    public int arrowAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patient" && !bedDirty && NPCData == null)
        {
            NPCData = other.gameObject.GetComponent<PatientDataScript>();
            haveSit = true;
            minigame.difficulty = NPCData.sicknessLevel;
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
    private void Goodbye()
    {
        NPCData.sicknessID = -1;
        minigame.difficulty = -1;
        haveSit = false;
        NPCData = null;
        CloseMinigame();
    }
    private void RemovePiatent()
    {
        minigame.difficulty = -1;
        NPCData = null;
        haveSit = false;
        haveMinigame = false;
        minigame.ClearRhythm();
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
                minigame.intAllArrow.Add(potionData.sicknessData[potionData.FindNumOfSick(NPCData.sicknessID)].patternPress[i]);
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

        bedDirtyModel.SetActive(false);
        bedDirty = false;
        timeCheck = 0;

        arrowAdd = 5;
    }
    private void Update()
    {
        if (bedDirty)
        {
            timeCheck += Time.deltaTime;
            bedDirtyModel.SetActive(true);
            if (timeCheck >= potionData.timeBedDirty)
            {
                timeCheck = 0;
                bedDirty = false;
                bedDirtyModel.SetActive(false);
            }
        }
        if (NPCData != null)
        {
            NPCData.willTreat = (minigame.buttonPressed != 0) ? true : false;
            if (NPCData.dead)
            {
                bedDirty = true;
                Destroy(NPCData.gameObject, 0);
                RemovePiatent();
            }
        }
        if (NPCData == null)
        {
            CloseMinigame();
            RemovePiatent();
        }

        if (minigame.difficulty == 0 && NPCData != null)
        {
            Goodbye();
            UIManagerScript.treated++;
        }
        if (minigame.deHeat != 0 && NPCData != null)
        {
            if (NPCData.heat <= minigame.deHeat * -1)
            {
                NPCData.deHeat = minigame.deHeat;
                NPCData = null;
                minigame.ClearRhythm();
                haveSit = false;
                bedDirty = true;
            }
            else
            {
                NPCData.deHeat = minigame.deHeat;
            }
            minigame.deHeat = 0;
        }
        if (NPCData != null && minigame.difficulty != NPCData.sicknessLevel)
        {
            NPCData.sicknessLevel = minigame.difficulty;
        }
        
        if (arrowAdd != 5)
        {
            minigame.arrowAdd = arrowAdd;
            arrowAdd = 5;
        }
    }
}
