 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public int id;
    public ShowInjury injury;
    [SerializeField] private PatientDataScript NPCData;
    [SerializeField] private PotionDataScript potionData;
    public GameObject glowObj;
    public bool haveSit;
    private bool haveMinigame;
    public GameObject handPoint;
    [SerializeField] private GameObject minigameObj;
    [SerializeField] private Not2Rhythm minigame;
    private bool onMinigame;

    [SerializeField] private float timeBedDirty;
    [SerializeField] private GameObject bedDirtyModel;
    private float timeCheck;
    private bool bedDirty;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !bedDirty)
        {
            glowObj.SetActive(true);
            ToolPlayerScript.bed.Add(this);
        }
        if (other.tag == "Patient" && !bedDirty)
        {
            NPCData = other.gameObject.GetComponent<PatientDataScript>();
            //injury.ShowItemWant(NPCData.sicknessID);
            haveSit = true;
            minigame.difficulty = NPCData.sicknessLevel;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !onMinigame)
        {
            glowObj.SetActive(false);
            ToolPlayerScript.bed.Remove(this);
        }
        else if (other.gameObject.tag == "Player" && onMinigame)
        {
            if (NPCData != null)
            {
                injury.ShowItemWant(NPCData.sicknessID);
            }
            CloseMinigame();
            glowObj.SetActive(false);
            ToolPlayerScript.bed.Remove(this);
        }
        if (other.tag == "Patient")
        {
            RemovePiatent();
        }
    }
    private void DebutAllEnum()
    {
        Debug.Log("id Bed : " + id);
        Debug.Log("NPCData : " + NPCData);
        Debug.Log("haveSit : " + haveSit);
        Debug.Log("onMinigame : " + onMinigame);
        Debug.Log("~~~~~~~~~~~~~~~~~~~~");
    }
    private void Goodbye()
    {
        NPCData.sicknessID = -1;
        minigame.difficulty = -1;
        haveSit = false;
        injury.CloseImage();
        NPCData = null;
        CloseMinigame();
    }
    private void RemovePiatent()
    {
        minigame.difficulty = -1;
        NPCData = null;
        injury.CloseImage();
        haveSit = false;
        haveMinigame = false;
        minigame.ClearRhythm();
    }
    private void PlayMinigame()
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
    private void CloseMinigame()
    {
        minigameObj.SetActive(false);
        ////CameraScript.zoomOut = true;
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
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    DebutAllEnum();
        //}
        if (bedDirty)
        {
            timeCheck += Time.deltaTime;
            bedDirtyModel.SetActive(true);
            if (timeCheck >= timeBedDirty)
            {
                timeCheck = 0;
                bedDirty = false;
                bedDirtyModel.SetActive(false);
            }
        }
        //Debug.Log("bedDirty : " + bedDirty);
        if (ToolPlayerScript.bed.Count > 0)
        {
            if (id == ToolPlayerScript.bed[0].id && NPCData != null)
            {
                glowObj.SetActive(true);
                if (haveSit)
                {
                    PlayMinigame();
                    injury.CloseImage();
                }
            }
        }
        else if (ToolPlayerScript.bed.Count == 0)
        {
            if (NPCData != null)
            {
                injury.ShowItemWant(NPCData.sicknessID);
            }
            CloseMinigame();
            glowObj.SetActive(false);
        }
        {
            //if (minigame.playerGet != 0)
            //{
            //    switch (minigame.playerGet)
            //    {
            //        case 20:
            //            Debug.Log("Nice");
            //            Goodbye();
            //            break;
            //        default:
            //            Debug.Log("Ok");
            //            Goodbye();
            //            break;
            //    }
            //    minigame.playerGet = 0;
            //}
        }//for Not1Rhythm
        if (minigame.difficulty == 0 && NPCData != null)
        {
            Debug.Log("HI");
            Goodbye();
            Debug.Log("bedDirty : " + bedDirty);
            UIManagerScript.treated++;
            NewSpawnNPCScript.numOfNPC--;
        }
        if (minigame.deHeat != 0 && NPCData != null)
        {
            if (NPCData.heat <= minigame.deHeat * -1)
            {
                NPCData.deHeat = minigame.deHeat;
                NPCData = null;
                minigame.ClearRhythm();
                CloseMinigame();
                haveSit = false;
                NewSpawnNPCScript.numOfNPC--;
            }
            else
            {
                NPCData.deHeat = minigame.deHeat;
            }
            minigame.deHeat = 0;
            bedDirty = true;
        }
        if (NPCData != null && minigame.difficulty != NPCData.sicknessLevel)
        {
            NPCData.sicknessLevel = minigame.difficulty;
        }
    }
}
