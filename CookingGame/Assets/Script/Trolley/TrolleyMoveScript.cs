using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyMoveScript : MonoBehaviour
{
    private TrolleySpawnScript spawnScript;
    public float moveSpeed;

    [SerializeField] private float timeInOneRound;
    [SerializeField] private float timeCount;
    public Transform position1;
    public Transform position2;
    [SerializeField] private float timeToSpare;
    [SerializeField] private bool po1;
    private bool spawning;
    private int randomNumToSpawn;
    private Animator animatorTrolley;

    public Animator bantaAnimation;
    private bool yeet;

    private void MoveTrolley()
    {
        transform.position = Vector3.MoveTowards(transform.position, position2.position, moveSpeed);
    }
    private void ResetTrolley()
    {
        timeCount = timeInOneRound + 7f;
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
            if (!yeet)
            {
                yeet = true;
            }
            else
            {
                yeet = false;
            }
            bantaAnimation.SetBool("Yeet", yeet);
            yield return new WaitForSeconds(T + 0.05f);
            spawnScript.SpawnPatient();
        }
        spawning = false;
    }
    private void Start()
    {
        yeet = false;
        spawnScript = GetComponent<TrolleySpawnScript>();
        animatorTrolley = GetComponent<Animator>();
        randomNumToSpawn = Random.Range(spawnScript.minNumOfNPC, spawnScript.maxNumOfNPC);
        ResetTrolley();
        timeCount = timeInOneRound + 7f;
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
            timeCount = timeCount - Time.deltaTime;
            if (timeCount <= 0)
            {
                po1 = false;
            }
        }
        else if (!po1)
        {
            animatorTrolley.SetBool("reset", true);
            ResetTrolley();
        }
    }
}
