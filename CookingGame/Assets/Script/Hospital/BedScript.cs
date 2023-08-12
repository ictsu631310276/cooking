using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public int id;
    public ShowInjury injury;
    [SerializeField] private PatientDataScript NPCData;
    public GameObject glowObj;
    private bool haveSit = false;
    public GameObject handPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(true);
            ToolPlayerScript.bed.Add(this);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(false);
            ToolPlayerScript.bed.Remove(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patient")
        {
            NPCData = other.gameObject.GetComponent<PatientDataScript>();
            injury.ShowItemWant(NPCData.itemNPCWant);
        }
    }
    private void Update()
    {
        if (ToolPlayerScript.tableInteraction.Count > 0)
        {
            if (ToolPlayerScript.haveItem && id == ToolPlayerScript.tableInteraction[0])
            {
                glowObj.SetActive(true);
                if (Input.GetKeyUp(KeyCode.Q) && !haveSit)
                {
                    haveSit = true;
                    transform.position = handPoint.transform.position;
                }
                if (Input.GetKeyDown(KeyCode.Space) && haveSit
                    && NPCData.itemNPCWant == ToolPlayerScript.itemInHand)
                {

                }
            }
            else
            {
                glowObj.SetActive(false);
            }
        }
    }
}
