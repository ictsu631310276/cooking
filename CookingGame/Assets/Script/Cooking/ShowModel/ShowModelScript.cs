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
    private bool[] havePlate = new bool[2] { false, false };
    private int num;

    public void ShowModel(int idItemInHand , bool _havePlate ,bool dirtyDishes)
    {
        if (!createPlate && _havePlate)
        {
            if (!dirtyDishes)
            {
                plateModel = Instantiate(ingredient.plate, handPoint.transform, false);
            }
            else if (dirtyDishes)
            {
                plateModel = Instantiate(ingredient.dirtyPlate, handPoint.transform, false);
            }
            plateModel.transform.parent = handPoint.transform;
            createPlate = true;
            havePlate[0] = _havePlate;
            havePlate[1] = dirtyDishes;
        }
        else if(createPlate && (havePlate[0] != _havePlate || havePlate[1] != dirtyDishes))
        {
            Destroy(plateModel, 0);
            createPlate = false;
        }//จาน
        if (idItemInHand != 0 && !haveItem)
        {
            itemModel = Instantiate(ingredient.itemData[ingredient.FindNumOfArray(idItemInHand)].model, handPoint.transform, false);
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
    public void ShowItemWant (int item)
    {
        if (item != 0 && !haveItem)
        {
            itemModel = Instantiate(ingredient.itemData[ingredient.FindNumOfArray(item)].model, handPoint.transform, false);
            itemModel.transform.position = itemModel.transform.position + new Vector3(-0.25f, 0f, -0.3f) ;
            itemModel.transform.parent = handPoint.transform;
            haveItem = true;
            num = item;
        }
        else if (item == 0 || num != item)
        {
            Destroy(itemModel, 0);
            haveItem = false;
            num = item;
        }//มือว่าง
    }
}
