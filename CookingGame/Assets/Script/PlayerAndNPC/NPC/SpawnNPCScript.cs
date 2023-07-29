using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCScript : MonoBehaviour
{
    public static bool open = false;
    public ingredientScript ingredient;//ดูเมนูอาหาร
    public List<int> allIDFoodWant;
    public NPCMoveScript npcMove;
    public GameObject moveChild;
    public GameObject mpcPrefab;
    public Transform outDoor;
    public ChairCustomerScript[] allChair;
    public Transform[] spawnPoint;
    public float timeInOneRound;
    private float timeCount = 0;
    private int iDNPC = 0;//เพิ่มตลอดทั้งวัน
    private void SpawnNPC()
    {
        int i = Random.Range(0, 2);//สุ่มจุดเกิด 2 จุด
        NPCMoveScript npcSpawn = Instantiate(npcMove, spawnPoint[i], false);
        npcSpawn.transform.parent = moveChild.transform;
        npcSpawn.NPC.iDNPC = iDNPC;
        iDNPC++;
        npcSpawn.NPC.itemNPCWant = FindFoodWant();
        npcSpawn.outDoor = outDoor;
        for (int j = 0; j < allChair.Length; j++)
        {
            npcSpawn.chairScript[j] = allChair[j];
        }
        npcSpawn.walk = true;
    }
    private int FindFoodWant()
    {
        int i = Random.Range(0,allIDFoodWant.Count);
        int j = allIDFoodWant[i];
        return j;
    }
    private bool haveChairCanSit()
    {
        bool j = true;
        for (int i = 0; i < allChair.Length; i++)
        {
            if (!allChair[i].haveSit)
            {
                j = true;
                i = allChair.Length + 1;
            }
            else if(allChair[i].haveSit)
            {
                j = false;
            }
        }
        return j;
    }
    private void Start()
    {
        for (int i = 0; i < ingredient.allIngredient.Length; i++)
        {
            if (ingredient.allIngredient[i].canOnPlate)
            {
                allIDFoodWant.Add(ingredient.allIngredient[i].id);
            }
        }
    }
    private void Update()
    {
        if (open)
        {
            timeCount = timeCount + Time.deltaTime;
        }
        if (timeCount >= timeInOneRound && haveChairCanSit())
        {
            timeCount = 0;
            SpawnNPC();
        }
        if (!haveChairCanSit())
        {
            timeCount = 0;
        }
    }
}
