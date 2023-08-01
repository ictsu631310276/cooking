using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoveScript : MonoBehaviour
{
    public NPCDataScript NPC;
    private Transform target;
    public Transform outDoor;
    private NavMeshAgent agent;
    public List<ChairCustomerScript> chairScript;
    public List<ChairCustomerScript> canSet;
    public bool walk = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OutDoor")
        {
            Destroy(gameObject,0);
        }
    }
    private void FindChair()
    {
        for (int i = 0; i < chairScript.Count; i++)
        {
            if (!chairScript[i].haveSit)
            {
                canSet.Add(chairScript[i]);
            }
        }
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (walk)
        {
            FindChair();
            target = canSet[0].positionChair;
            agent.destination = target.position;
            walk = false;
        }
        if (NPC.finishedEating)
        {
            target = outDoor;
            agent.destination = target.position;
        }
    }
}
