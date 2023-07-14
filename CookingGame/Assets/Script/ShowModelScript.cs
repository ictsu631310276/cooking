using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowModelScript : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject itemModel;
    public ingredientScript ingredient;
    private bool haveItem = false;
    public void ShowModel(int idItemInHand)
    {
        if (idItemInHand != 0 && !haveItem)
        {
            int j = 0;
            bool x = true;
            do
            {
                if (ingredient.idItem[j] == idItemInHand)
                {
                    x = false;
                }
                else
                {
                    j++;
                }
            } while (x);
            itemModel = Instantiate(ingredient.allIngredient[j], handPoint.transform, false);
            itemModel.transform.parent = handPoint.transform;
            haveItem = true;
        }
        else if (idItemInHand == 0)
        {
            WillDestroy();
        }//มือว่าง
    }
    public void WillDestroy()
    {
        Destroy(itemModel, 0);
        haveItem = false;
    }
    private void Update()
    {
    }
}
