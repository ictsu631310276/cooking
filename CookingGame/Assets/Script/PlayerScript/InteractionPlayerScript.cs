using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlayerScript : MonoBehaviour
{
    public static List<int> tableInteraction = new List<int>();//สำหรับโต้ะ
    public static int itemInHand = 0;//ของในมือ
    public static bool haveItem = false;
    private GameObject itemModel;
    public GameObject handPoint;
    private GameObject[] allItemModel;

    public ingredientScript ingredientScript;
    private void Start()
    {
        allItemModel = new GameObject[ingredientScript.allIngredient.Length];
        for (int i = 0; i < ingredientScript.allIngredient.Length; i++)
        {
            allItemModel[i] = ingredientScript.allIngredient[i];
        }
    }
    private void ShowModel()
    {
        if (!haveItem && itemInHand != 0)
        {
            haveItem = true;
            switch (itemInHand)
            {
                case 1:
                    itemModel = Instantiate(allItemModel[itemInHand - 1], handPoint.transform, false);
                    break;
                case 2:
                    itemModel = Instantiate(allItemModel[itemInHand - 1], handPoint.transform, false);
                    break;
                default:
                    Debug.LogError("No have this ID.");
                    break;
            }
            itemModel.transform.parent = handPoint.transform;
        }
        else if (itemInHand == 0 && haveItem)
        {
            haveItem = false;
            Destroy(itemModel, 0);
        }
    }
    private void Update()
    {
        ShowModel();
        if (Input.GetKeyDown(KeyCode.E) && tableInteraction.Count >= 2)
        {
            tableInteraction.Add(tableInteraction[0]);
            tableInteraction.RemoveAt(0); 
        }
    }
}
