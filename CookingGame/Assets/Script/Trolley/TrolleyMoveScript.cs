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
    private Animator animatorTrolley;
    private void MoveTrolley()
    {
        transform.position = Vector3.MoveTowards(transform.position, position2.position, moveSpeed);
    }
    private void ResetTrolley()
    {
        timeCount = timeInOneRound;
        animatorTrolley.SetBool("run", true);
        animatorTrolley.SetBool("reset", false);
        po1 = true;
        transform.position = position1.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpawnPoint")
        {
            animatorTrolley.SetBool("run", false);
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
        animatorTrolley = GetComponent<Animator>();
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
                animatorTrolley.SetBool("run", true);
            }
        }
        else if (!po1)
        {
            animatorTrolley.SetBool("reset", true);
            timeCount = timeCount - Time.deltaTime;
            if (timeCount <= 0)
            {
                ResetTrolley();
            }
        }
    }
}
