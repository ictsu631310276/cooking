using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowModelScript : MonoBehaviour
{
    public Transform handPoint;
    private GameObject plateModel;
    private GameObject itemModel;
    public ingredientScript ingredient;
    private bool haveItem = false;
    private bool createPlate = false;
    private bool dirtyPlate = false;
    private int num;

    public void ShowModel(int idItemInHand , bool havePlate ,bool dirtyDishes)
    {
        if (!createPlate && havePlate)
        {
            if (!dirtyDishes)
            {
                plateModel = Instantiate(ingredient.plate, handPoint.transform, false);
                plateModel.transform.parent = handPoint.transform;
                createPlate = true;
            }
            else if (dirtyDishes)
            {
                plateModel = Instantiate(ingredient.dirtyPlate, handPoint.transform, false);
                plateModel.transform.parent = handPoint.transform;
                createPlate = true;
            }
        }
        else if(createPlate && ((!havePlate || dirtyDishes) || (!havePlate && dirtyDishes)))
        {
            if (!dirtyPlate)
            {
                Destroy(plateModel, 0);
                createPlate = false;
                dirtyPlate = true;
            }
            if (!havePlate && !dirtyDishes)
            {
                Destroy(plateModel, 0);
                createPlate = false;
                dirtyPlate = false;
            }
        }//จาน
        if (idItemInHand != 0 && !haveItem)
        {
            itemModel = Instantiate(ingredient.allIngredient[findNumArray(idItemInHand)].ingredient, handPoint.transform, false);
            itemModel.transform.parent = handPoint.transform;
            haveItem = true;
            num = idItemInHand;
        }
        else if (idItemInHand == 0 || num != idItemInHand)
        {
            Destroy(itemModel, 0);
            haveItem = false;
            num = idItemInHand;
        }//มือว่าง
    }
    private int findNumArray(int idItem)
    {
        int j = 0;
        bool x = true;
        if (idItem != 0)
        {
            do
            {
                if (j >= ingredient.allIngredient.Length)
                {
                    x = false;
                    Debug.LogError("Have Problem in findNumArray");
                }
                else if (ingredient.allIngredient[j].id == idItem)
                {
                    x = false;
                }
                else
                {
                    j++;
                }
            } while (x);
        }
        return j;
    }
}
