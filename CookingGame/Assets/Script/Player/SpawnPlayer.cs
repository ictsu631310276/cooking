using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public bool twoPlayer;

    public PotionDataScript potionData;
    public GameObject[] playerP;
    public Transform[] spawnPoint;
    private GameObject[] player = new GameObject[2];
    private void Spawn(int x)
    {
        switch (x)
        {
            case 1:
                player[0] = Instantiate(playerP[0], this.gameObject.transform, false);
                player[0].GetComponent<PlayerMoveScript>().dataPotion = potionData;
                player[0].GetComponent<ToolPlayerScript>().potionData = potionData;
                player[0].GetComponent<ThrowScript>().potionData = potionData;
                player[0].transform.parent = null;
                break;
            case 2:
                player[0] = Instantiate(playerP[0], spawnPoint[0], false);
                player[1] = Instantiate(playerP[1], spawnPoint[1], false);
                player[0].GetComponent<PlayerMoveScript>().dataPotion = potionData;
                player[0].GetComponent<ToolPlayerScript>().potionData = potionData;
                player[0].GetComponent<ThrowScript>().potionData = potionData;
                player[1].GetComponent<PlayerMoveScript>().dataPotion = potionData;
                player[1].GetComponent<ToolPlayerScript>().potionData = potionData;
                player[1].GetComponent<ThrowScript>().potionData = potionData;
                player[0].transform.parent = null;
                player[1].transform.parent = null;
                break;
        }
    }
    private void Start()
    {
#if UNITY_EDITOR
        if (twoPlayer)
        {
            UIManagerScript.numOfPlayer = 2;
        }
        else
        {
            UIManagerScript.numOfPlayer = 1;
        }
#endif
        Spawn(UIManagerScript.numOfPlayer);
    }
    private void Update()
    {
        
    }
}
