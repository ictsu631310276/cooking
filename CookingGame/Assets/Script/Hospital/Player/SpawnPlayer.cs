using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public bool twoPlayer;

    public GameObject[] playerP;
    public Transform[] spawnPoint;
    private void Spawn(int x)
    {
        switch (x)
        {
            case 1:
                GameObject player = Instantiate(playerP[0], spawnPoint[0], false);
                break;
            case 2:
                GameObject player1 = Instantiate(playerP[0], spawnPoint[1], false);
                GameObject player2 = Instantiate(playerP[1], spawnPoint[2], false);
                break;
            default:
                if (!twoPlayer)
                {
                    GameObject playerEx = Instantiate(playerP[0], spawnPoint[0], false);
                }
                else
                {
                    GameObject playerEx1 = Instantiate(playerP[0], spawnPoint[1], false);
                    GameObject playerEx2 = Instantiate(playerP[1], spawnPoint[2], false);
                }
                break;
        }
    }
    private void Start()
    {
        Spawn(UIManagerScript.numOfPlayer);
    }
    private void Update()
    {
        
    }
}
