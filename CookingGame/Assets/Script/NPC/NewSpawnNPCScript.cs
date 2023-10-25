using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnNPCScript : MonoBehaviour
{
    [SerializeField] private TimeUI timeUIScript;
    public PotionDataScript potionData;
    public PatientDataScript[] npcData;

    public checkNPC[] spawnPoint;
    [HideInInspector] public List<int> canSp;

    [SerializeField] private float timeInOneRound;
    private float timeCount;
    public static int iDNPC;//เพิ่มตลอดทั้งวัน
    public static int numOfNPC;
    [SerializeField] private int maxNumOfNPCInS;
    private void SpawnNPC()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            if (spawnPoint[i].canSpawn == true)
            {
                canSp.Add(i);
            }
        }
        int typeBun = Random.Range(0, npcData.Length);
        int typeSickness = Random.Range(1, potionData.sicknessData.Length);

        if (canSp.Count != 0)
        {
            int numSpawn = canSp[Random.Range(0, canSp.Count)];

            PatientDataScript npcSpawn = Instantiate(npcData[typeBun], spawnPoint[numSpawn].transform, false);
            npcSpawn.id = iDNPC;

            npcSpawn.sicknessID = potionData.sicknessData[typeSickness].id;
            npcSpawn.allModelSickness = potionData.sicknessData[typeSickness].modleSickness;

            int levelSickness = Random.Range(potionData.sicknessData[typeSickness].startSicknessLevel, 4);
            npcSpawn.sicknessLevel = levelSickness;

            npcSpawn.declineH = potionData.sicknessData[typeSickness].declineLife;
            npcSpawn.tiemDeclineH = potionData.sicknessData[typeSickness].timeToDeclineLife;
            iDNPC++;
            npcSpawn.transform.parent = null;
            numOfNPC++;
        }
        canSp.Clear();
    }
    private void Start()
    {
        iDNPC = 0;
        numOfNPC = 0;
        if (UIManagerScript.numOfPlayer == 2)
        {
            timeInOneRound = timeInOneRound / 2;
        }
        timeCount = timeInOneRound;
    }
    private void Update()
    {
        timeCount = timeCount + Time.deltaTime;
        if (timeCount >= timeInOneRound && !timeUIScript.endDay && numOfNPC <= maxNumOfNPCInS)
        {
            timeCount = 0;
            SpawnNPC();
        }
    }
}
