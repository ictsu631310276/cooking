using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class checkNPC : MonoBehaviour
{
    public bool canSpawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patient")
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Patient")
        {
            canSpawn = true;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
