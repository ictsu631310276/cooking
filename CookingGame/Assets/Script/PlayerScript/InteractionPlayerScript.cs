using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlayerScript : MonoBehaviour
{
    public static List<int> tableInteraction = new List<int>();//สำหรับโต้ะ
    public static int itemInHand = 0;//ของในมือ
    public static bool haveItem = false;
    private GameObject i;
    public GameObject handPoint;
    public GameObject[] allItemModel;
    
    private void OnCollisionEnter(Collision collision)
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("handItem : " + haveItem);
            Debug.Log("itemInHand : " + itemInHand);
        }
        //if (Input.GetKeyDown(KeyCode.Q))
        //{

        //}
        if (!haveItem && itemInHand != 0)
        {
            haveItem = true;
            switch (itemInHand)
            {
                case 21:
                    i = Instantiate(allItemModel[0], handPoint.transform.position, Quaternion.identity);
                    i.transform.parent = handPoint.transform;
                    break;
                default:
                    Debug.LogError("No have this ID.");
                    break;
            }
        }
        else if (itemInHand == 0 && haveItem)
        {
            haveItem = false;
            Destroy(i, 0);
        }
        if (Input.GetKeyDown(KeyCode.E) && tableInteraction.Count >= 2)
        {
            tableInteraction.Add(tableInteraction[0]);
            tableInteraction.RemoveAt(0); 
        }
    }
}
