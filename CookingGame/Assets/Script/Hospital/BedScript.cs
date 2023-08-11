using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public int id;
    public GameObject glowObj;
    private bool haveSit = false;
    public GameObject handPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(true);
            NewInteractionPlayerScript.bed.Add(this);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObj.SetActive(false);
            NewInteractionPlayerScript.bed.Remove(this);
        }
    }
    private void Update()
    {
        if (NewInteractionPlayerScript.tableInteraction.Count > 0)
        {
            if (NewInteractionPlayerScript.haveItem && id == NewInteractionPlayerScript.tableInteraction[0])
            {
                glowObj.SetActive(true);
                if (Input.GetKeyUp(KeyCode.Q) && !haveSit)
                {
                    haveSit = true;
                    transform.position = handPoint.transform.position;
                }
            }
            else
            {
                glowObj.SetActive(false);
            }
        }
    }
}
