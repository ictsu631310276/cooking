using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxScript : MonoBehaviour
{
    public int id;
    public int itemID;
    public int numOfItem;
    public int numOfRequiredMax;
    [SerializeField] private GameObject glowObj;
    [HideInInspector] public int numOfRequired;
    public GameObject modelItem;
    private ToolPlayerScript player;
    public UIItemBoxScript canva;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<ToolPlayerScript>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
    private void Start()
    {
        glowObj.SetActive(false);
        if (canva != null)
        {
            canva.maxValue = numOfRequiredMax;
        }
    }
    private void Update()
    {
        if (player != null)
        {
            if (player.itemBox[0].id == id)
            {
                glowObj.SetActive(true);
            }
            else
            {
                glowObj.SetActive(false);
            }
        }
        else
        {
            glowObj.SetActive(false);
        }//glowObj

        if (numOfRequired >= numOfRequiredMax && numOfItem <= 255)
        {
            numOfRequired = numOfRequired - numOfRequiredMax;
            numOfItem++;
        }

        if (canva != null)
        {
            canva.value = numOfRequired;
            canva.numOfItem = numOfItem;
        }
    }
}
