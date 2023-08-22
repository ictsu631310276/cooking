using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public int id;
    public ShowInjury injury;
    [SerializeField] private PatientDataScript NPCData;
    public GameObject glowObj;
    public bool haveSit = false;
    public GameObject handPoint;
    [SerializeField] private GameObject minigameObj;
    [SerializeField] private NotRhythm minigame;
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
            injury.ShowItemWant(NPCData.itemNPCWant);
            haveSit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
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
        NPCData.itemNPCWant = 0;
        NPCData = null;
        haveSit = false;
        injury.CloseImage();

        minigameObj.SetActive(false);
        CameraScript.zoomOut = true;
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
            if (ToolPlayerScript.haveItem && id == ToolPlayerScript.bed[0].id)
            {
                glowObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) && haveSit
                    && NPCData.itemNPCWant == ToolPlayerScript.itemInHand)
                {
                    minigameObj.SetActive(true);
                    minigame.difficulty = NPCData.heat;
                    //CameraScript.zoomOut = false;
                    onMinigame = true;
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    minigameObj.SetActive(false);
                    //CameraScript.zoomOut = true;
                    onMinigame = false;
                }
            }
        }
        if (minigame.playerGet != 0)
        {
            switch (minigame.playerGet)
            {
                case 20:
                    Debug.Log("Nice");
                    Goodbye();
                    break;
                default:
                    Debug.Log("Ok");
                    Goodbye();
                    break;
            }
            minigame.playerGet = 0;
        }
    }
}
