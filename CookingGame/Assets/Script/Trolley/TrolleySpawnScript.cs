using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleySpawnScript : MonoBehaviour
{
    public PotionDataScript potionData;
    public PatientDataScript[] npcData;

    public Transform spawnPoint;
    public static int iDNPC;//เพิ่มตลอดทั้งวัน
    public static int numOfNPC;
    public int maxNumOfNPC;
    public int minNumOfNPC;
    public void SpawnPatient()
    {
        int typeBun = Random.Range(0, npcData.Length);
        int typeSickness = Random.Range(1, potionData.sicknessData.Length);

        float random1 = Random.Range(-2f, 2f);
        float random2 = Random.Range(-2f, 2f);
        float random3 = Random.Range(-2f, 2f);
        Vector3 ranTransform = new Vector3(spawnPoint.transform.position.x + random1, spawnPoint.transform.position.y + random2, spawnPoint.transform.position.z + random3);

        PatientDataScript npcSpawn = Instantiate(npcData[typeBun], ranTransform, Quaternion.identity);
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
    private void Start()
    {
        iDNPC = 0;
        numOfNPC = 0;
    }
}
