using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnNPCScript : MonoBehaviour
{
    public int numOfNPCInDay;
    public NewIngredientScript ingredient;//ดูเมนูอาหาร
    public List<int> allIDFoodWant;
    public NewNPCDataScript npcData;
    public Transform spawnPoint;
    public GameObject handPlayer;
    [SerializeField] private float timeInOneRound;
    private float timeCount = 0;
    private int iDNPC = 1;//เพิ่มตลอดทั้งวัน
    private void SpawnNPC()
    {
        NewNPCDataScript npcSpawn = Instantiate(npcData, spawnPoint, false);
        npcSpawn.id = iDNPC;
        npcSpawn.handPoint = handPlayer;
        iDNPC++;
        npcSpawn.itemNPCWant = FindFoodWant();
    }
    private int FindFoodWant()
    {
        int i = Random.Range(0,allIDFoodWant.Count);
        int j = allIDFoodWant[i];
        return j;
    }
    private void Start()
    {
        for (int i = 0; i < ingredient.itemData.Length; i++)
        {
            if (ingredient.itemData[i].canOnPlate)
            {
                allIDFoodWant.Add(ingredient.itemData[i].id);
            }
        }
    }
    private void Update()
    {
        timeCount = timeCount + Time.deltaTime;
        if (timeCount >= timeInOneRound && numOfNPCInDay > 0)
        {
            timeCount = 0;
            SpawnNPC();
            numOfNPCInDay--;
        }
    }
}
