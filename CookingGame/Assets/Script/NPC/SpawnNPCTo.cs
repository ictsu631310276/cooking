using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCTo : MonoBehaviour
{
    [SerializeField] private PatientDataScript npcData;
    private int idNPC;
    [SerializeField] private GameObject obj;//โรค
    [SerializeField] private GameObject bedObj;
    [SerializeField] private GameObject binObj;
    private void SpawnNPC(int numNPC)
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 1, 1, 1 };//ระดับความรุนแรงของโรค
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด
        for (int i = 0; i < numNPC; i++)
        {
            PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
            npcSpawn.id = idNPC;
            idNPC++;
            npcSpawn.sicknessID = 2;
            npcSpawn.allModelSickness = _ObjArray;

            npcSpawn.sicknessLevel = 1;

            npcSpawn.declineH = _IntArray;
            npcSpawn.tiemDeclineH = _FolatArray;
            npcSpawn.transform.parent = null;
        }
    }
    private void SpawnDeadNPC()
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 0, 0, 0 };//ระดับความรุนแรงของโรค
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด

        PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
        npcSpawn.id = idNPC;
        idNPC++;
        npcSpawn.sicknessID = 2;
        npcSpawn.allModelSickness = _ObjArray;

        npcSpawn.sicknessLevel = 1;

        npcSpawn.declineH = _IntArray;
        npcSpawn.tiemDeclineH = _FolatArray;
        npcSpawn.heat = 0;
        npcSpawn.transform.parent = null;
    }
    private void SpawnNPCWillDead()
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 0, 0, 0 };//ระดับความรุนแรงของโรค
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด

        PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
        npcSpawn.id = idNPC;
        idNPC++;
        npcSpawn.sicknessID = 2;
        npcSpawn.allModelSickness = _ObjArray;

        npcSpawn.sicknessLevel = 1;

        npcSpawn.declineH = _IntArray;
        npcSpawn.tiemDeclineH = _FolatArray;
        npcSpawn.heat = 50;
        npcSpawn.transform.parent = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        idNPC = 0;
        bedObj.SetActive(false);
        binObj.SetActive(false);
        SpawnNPC(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (TextScript.textStart == 2)
        {
            bedObj.SetActive(true);
        }
        else if (TextScript.textStart == 4)
        {
            SpawnNPC(2);
            TextScript.textStart++;
        }
        else if (TextScript.textStart == 8)
        {
            binObj.SetActive(true);
            SpawnDeadNPC();
            TextScript.textStart++;
        }
        else if (TextScript.textStart == 11)
        {
            SpawnNPCWillDead();
            TextScript.textStart++;
        }
    }
}
