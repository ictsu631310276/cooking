using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCTo : MonoBehaviour
{
    public PatientDataScript npcData;
    public GameObject obj;
    private void SpawnNPC()
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 20, 20, 20 };
        float[] _FolatArray = { 20, 20, 20 };
        PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
        npcSpawn.id = 1;

        npcSpawn.sicknessID = 2;
        npcSpawn.allModelSickness = _ObjArray;

        npcSpawn.sicknessLevel = 1;


        npcSpawn.declineH = _IntArray;
        npcSpawn.tiemDeclineH = _FolatArray;
        npcSpawn.transform.parent = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnNPC();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
