using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxScript : MonoBehaviour
{
    public int id;
    public int itemID;
    [SerializeField] private GameObject glowObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            glowObj.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            glowObj.SetActive(false);
        }
    }
    private void Start()
    {
        glowObj.SetActive(false);
    }
    private void Update()
    {
        
    }
}
