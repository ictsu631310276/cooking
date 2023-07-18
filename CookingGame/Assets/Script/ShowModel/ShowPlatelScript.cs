using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlatelScript : MonoBehaviour
{
    public GameObject platePrefab;//Prefab
    public GameObject[] handPoint;//ตำแหน่ง
    public GameObject[] plateModel;//Model
    private bool haveItem = false;
    private int num;

    public void ShowModel(int numOfPlate)
    {
        if (numOfPlate < 5)
        {
            if (!haveItem)
            {
                for (int i = 0; i < numOfPlate; i++)
                {
                    plateModel[i] = Instantiate(platePrefab, handPoint[i].transform, false);
                    plateModel[i].transform.parent = handPoint[i].transform;
                }
                haveItem = true;
                num = numOfPlate;
            }
            else if (haveItem && num != numOfPlate)
            {
                for (int i = 0; i < 4; i++)
                {
                    Destroy(plateModel[i], 0);
                    plateModel[i] = null;
                }
                haveItem = false;
                num = numOfPlate;
            }
        }
    }
}
