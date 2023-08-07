using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetItemScript : MonoBehaviour
{
    public ingredientScript ingredient;
    public int id;
    public int amount;
    public bool like;
    public string nameItem;
    public TextMeshProUGUI nameItemText;
    public GameObject likeImageItem;
    public Image imageItem;
    public bool reCreate = false;
    public void Like()
    {
        if (like)
        {
            like = false;
        }
        else if (!like)
        {
            like = true;
        }
        ingredient.itemData[ingredient.FindNumOfArray(id)].like = like;
    }
    public void GetItem()
    {
        if ((!InteractionPlayerScript.havePlate[0]) ||
            (InteractionPlayerScript.havePlate[0] && ingredient.itemData[ingredient.FindNumOfArray(id)].canOnPlate))
        {
            if (amount > 0)
            {
                if (amount != 999)
                {
                    amount--;
                }
                InteractionPlayerScript.itemInHand = id;
                FridgeScript.openUI = false;
                CameraScript.zoomOut = true;
            }
            else if (amount <= 0)
            {
                Debug.Log("out of item");
            }
        }
    }
    private void Update()
    {
        nameItemText.text = nameItem + " : " + amount;
        if (like)
        {
            likeImageItem.SetActive(true);
        }
        else if (!like)
        {
            likeImageItem.SetActive(false);
        }
        if (reCreate)
        {
            Destroy(gameObject);
        }
    }
}
