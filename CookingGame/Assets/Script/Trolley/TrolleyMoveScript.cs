using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyMoveScript : MonoBehaviour
{
    private TrolleySpawnScript spawnScript;
    public float moveSpeed;

    [SerializeField] private float timeInOneRound;
    private float timeCount;
    public Transform position1;
    public Transform position2;
    private bool po1;
    private bool willSpawn;

    public float timeSpawnSet;
    private float timeSpawn;
    private void SetTrolley()
    {
        willSpawn = true;
        timeCount = timeInOneRound;
        po1 = true;
        transform.position = position1.position;
    }
    private void Start()
    {
        spawnScript = GetComponent<TrolleySpawnScript>();
        SetTrolley();
        timeCount = timeInOneRound;
        timeSpawn = timeSpawnSet;
    }
    // Update is called once per frame
    private void Update()
    {
        if (po1)
        {
            if (this.gameObject.transform.position.x >= -2 && this.gameObject.transform.position.x <= 2)
            {
                //transform.position = Vector3.MoveTowards(transform.position, position2.position, moveSpeed);
                //spawnScript.SpawnPatient();
                timeSpawn = timeSpawn - Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, position2.position, moveSpeed);
                timeSpawn = timeSpawnSet;
            }
        }
        else if (!po1)
        {
            timeCount = timeCount - Time.deltaTime;
            if (timeCount <= 0)
            {
                SetTrolley();
            }
        }

        if (timeSpawn < timeSpawnSet)
        {
            if (willSpawn)
            {
                spawnScript.SpawnPatient();
                willSpawn = false;
            }
            if (timeSpawn <= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, position2.position, moveSpeed);
            }
        }
    }
}
