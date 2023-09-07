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
    public GameObject handPoint;
    [SerializeField] private GameObject minigameObj;
    [SerializeField] private Not2Rhythm minigame;
    private bool onMinigame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            glowObj.SetActive(true);
            ToolPlayerScript.bed.Add(this);
        }
        if (other.tag == "Patient")
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
    }
    private void PlayMinigame()
    {
        onMinigame = true;
        minigameObj.SetActive(true);
        if (NPCData != null)
        {
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
        haveSit = false;
        onMinigame = false;
        minigameObj.SetActive(false);
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    DebutAllEnum();
        //}
        if (ToolPlayerScript.bed.Count > 0)
        {
            if (id == ToolPlayerScript.bed[0].id)
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
            Goodbye();
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
                UIManagerScript.dead++;
                NewSpawnNPCScript.numOfNPC--;
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
    }
}
