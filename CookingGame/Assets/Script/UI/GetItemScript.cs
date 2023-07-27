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
    private int FindNumOdArray(int id)
    {
        int i;
        for (i = 0; i < ingredient.allIngredient.Length; i++)
        {
            if (ingredient.allIngredient[i].id == id)
            {
                break;
            }
            else if (ingredient.allIngredient.Length == i)
            {
                Debug.LogError("Can't find Array of ID");
            }
        }
        return i;
    }
    public void Like()
    {
        if (like)
        {
            like = false;
            ingredient.allIngredient[FindNumOdArray(id)].like = like;
        }
        else if (!like)
        {
            like = true;
            ingredient.allIngredient[FindNumOdArray(id)].like = like;
        }
    }
    public void GetItem()
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
