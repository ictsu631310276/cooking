using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnNPCScript : MonoBehaviour
{
    public int numOfNPCInDay;
    [SerializeField] private TimeUI timeUIScript;
    public PotionDataScript potionData;
    public PatientDataScript npcData;
    public Transform[] spawnPoint;
    public GameObject handPlayer;
    public GameObject handPlayer2;
    public static GameObject handPlayerShare;
    [SerializeField] private float timeInOneRound;
    private float timeCount = 0;
    public static int iDNPC;//เพิ่มตลอดทั้งวัน
    public static int numOfNPC = 0;
    private void SpawnNPC()
    {
        int a = Random.Range(0, spawnPoint.Length);
        PatientDataScript npcSpawn = Instantiate(npcData, spawnPoint[a], false);
        npcSpawn.id = iDNPC;
        npcSpawn.handPoint = handPlayer;
        npcSpawn.handPoint2 = handPlayer2;
        int i = Random.Range(0, potionData.sicknessData.Length);
        npcSpawn.sicknessID = potionData.sicknessData[i].id;
        int j = Random.Range(potionData.sicknessData[i].startSicknessLevel ,3);
        npcSpawn.sicknessLevel = j;
        npcSpawn.declineH = potionData.sicknessData[i].declineLife;
        iDNPC++;
        npcSpawn.transform.parent = null;
        numOfNPC++;
    }
    private void Start()
    {
        iDNPC = 0;
        numOfNPC = 0;
        handPlayerShare = handPlayer;
    }
    private void Update()
    {
        //Debug.Log("numOfNPC : " + numOfNPC);
        timeCount = timeCount + Time.deltaTime;
        if (timeUIScript.haveHotTime && !timeUIScript.endDay && numOfNPC <= 7)
        {
            if (timeCount >= 1)
            {
                timeCount = 0;
                SpawnNPC();
                numOfNPCInDay--;
            }
        }
        else if (timeCount >= timeInOneRound && numOfNPCInDay > 0 && !timeUIScript.haveHotTime && !timeUIScript.endDay && numOfNPC <= 7)
        {
            timeCount = 0;
            SpawnNPC();
            numOfNPCInDay--;
        }

    }
}
