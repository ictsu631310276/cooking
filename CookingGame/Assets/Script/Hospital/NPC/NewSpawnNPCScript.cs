using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnNPCScript : MonoBehaviour
{
    [SerializeField] private TimeUI timeUIScript;
    public PotionDataScript potionData;
    public PatientDataScript[] npcData;
    public Transform[] spawnPoint;
    private int numSpawn;
    public GameObject handPlayer;
    public GameObject handPlayer2;
    public static GameObject handPlayerShare;
    [SerializeField] private float timeInOneRound;
    private float timeCount;
    public static int iDNPC;//เพิ่มตลอดทั้งวัน
    public static int numOfNPC;
    [SerializeField] private int maxNumOfNPCInS;
    private void SpawnNPC()
    {
        int a = numSpawn;
        do
        {
            numSpawn = Random.Range(0, spawnPoint.Length);
        } while (numSpawn == a);
        int typeBun = Random.Range(0, npcData.Length);
        int typeSickness = Random.Range(0, potionData.sicknessData.Length);

        PatientDataScript npcSpawn = Instantiate(npcData[typeBun], spawnPoint[numSpawn], false);
        npcSpawn.id = iDNPC;
        npcSpawn.handPoint = handPlayer;
        npcSpawn.handPoint2 = handPlayer2;

        npcSpawn.sicknessID = potionData.sicknessData[typeSickness].id;

        int levelSickness = Random.Range(potionData.sicknessData[typeSickness].startSicknessLevel, 3);
        npcSpawn.sicknessLevel = levelSickness;

        npcSpawn.declineH = potionData.sicknessData[typeSickness].declineLife;
        iDNPC++;
        npcSpawn.transform.parent = null;
        numOfNPC++;
    }
    private void Start()
    {
        numSpawn = 0;
        timeCount = 0;
        iDNPC = 0;
        numOfNPC = 0;
        handPlayerShare = handPlayer;
    }
    private void Update()
    {
        //Debug.Log("numOfNPC : " + numOfNPC);
        timeCount = timeCount + Time.deltaTime;
        if (timeUIScript.haveHotTime && !timeUIScript.endDay && numOfNPC <= maxNumOfNPCInS)
        {
            if (timeCount >= 1)
            {
                timeCount = 0;
                SpawnNPC();
            }
        }
        else if (timeCount >= timeInOneRound && !timeUIScript.haveHotTime && !timeUIScript.endDay && numOfNPC <= maxNumOfNPCInS)
        {
            timeCount = 0;
            SpawnNPC();
        }

    }
}
