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
    public bool haveSit = false;
    public GameObject handPoint;
    [SerializeField] private GameObject minigameObj;
    [SerializeField] private Not2Rhythm minigame;
    public static bool onMinigame = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            glowObj.SetActive(true);
            ToolPlayerScript.bed.Add(this);
        }
        else if (other.tag == "Patient")
        {
            NPCData = other.gameObject.GetComponent<PatientDataScript>();
            injury.ShowItemWant(NPCData.sicknessID);
            haveSit = true;
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
            injury.ShowItemWant(NPCData.sicknessID);
            CloseMinigame();
            glowObj.SetActive(false);
            ToolPlayerScript.bed.Remove(this);
        }
        else if (other.tag == "Patient")
        {
            NPCData = null;
            injury.CloseImage();
            haveSit = false;
        }
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
    private void PlayMinigame()
    {
        minigameObj.SetActive(true);
        minigame.difficulty = NPCData.sicknessLevel;
        for (int i = 0; i < potionData.sicknessData[potionData.FindNumOfSick(NPCData.sicknessID)].patternPress.Length; i++)
        {
            minigame.intAllArrow.Add(potionData.sicknessData[potionData.FindNumOfSick(NPCData.sicknessID)].patternPress[i]);
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
        minigameObj.SetActive(false);
    }
    private void Update()
    {
        if (ToolPlayerScript.bed.Count > 0)
        {
            if (ToolPlayerScript.haveItem && id == ToolPlayerScript.bed[0].id
                && ToolPlayerScript.itemInHand > 0)
            {
                glowObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) && haveSit)
                {
                    PlayMinigame();
                    onMinigame = true;
                    injury.CloseImage();
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    injury.ShowItemWant(NPCData.sicknessID);
                    CloseMinigame();
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
        }
    }
}
