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
    [SerializeField] private float timeToSpare;
    private bool po1;
    private bool spawning;
    private int randomNumToSpawn;
    private void MoveTrolley()
    {
        transform.position = Vector3.MoveTowards(transform.position, position2.position, moveSpeed);
    }
    private void ResetTrolley()
    {
        timeCount = timeInOneRound;
        po1 = true;
        transform.position = position1.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpawnPoint")
        {
            spawning = true;
            StartCoroutine(SpawnDelay(timeToSpare));
            randomNumToSpawn = Random.Range(spawnScript.minNumOfNPC, spawnScript.maxNumOfNPC);
        }
    }
    IEnumerator SpawnDelay(float T)
    {
        randomNumToSpawn = Random.Range(spawnScript.minNumOfNPC, spawnScript.maxNumOfNPC);
        for (int i = 0; i < randomNumToSpawn; i++)
        {
            yield return new WaitForSeconds(T);
            spawnScript.SpawnPatient();
        }
        spawning = false;
    }
    private void Start()
    {
        spawnScript = GetComponent<TrolleySpawnScript>();
        randomNumToSpawn = Random.Range(spawnScript.minNumOfNPC, spawnScript.maxNumOfNPC);
        ResetTrolley();
        timeCount = timeInOneRound;
        spawning = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if (po1)
        {
            if (!spawning)
            {
                MoveTrolley();
            }
        }
        else if (!po1)
        {
            timeCount = timeCount - Time.deltaTime;
            if (timeCount <= 0)
            {
                ResetTrolley();
            }
        }
    }
}
