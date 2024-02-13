using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    private ToolPlayerScript playerData;
    [SerializeField] private Transform whatGo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerData = other.gameObject.GetComponent<ToolPlayerScript>();
        }
    }
    private void MovePlayer()
    {
        playerData.gameObject.transform.position = whatGo.position;
        if (playerData != null)
        {
            if (playerData.havePatient)
            {
                playerData.patientID[0].heat = 0;
            }
        }
        playerData = null;
    }
    private void Start()
    {
        if (whatGo == null)
        {
            Debug.LogError("put data whatGo");
        }
    }
    private void Update()
    {
        if (playerData != null)
        {
            MovePlayer();
        }
    }
}
