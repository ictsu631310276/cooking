using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowOrderScript : MonoBehaviour
{
    public ingredientScript ingredient;
    public int idItem;
    public Image image;
    public TextMeshProUGUI text;
    public bool reCreate = false;
    private int FineNumOfArray(int id)
    {
        int j = 0;
        for (j = 0; j < ingredient.allIngredient.Length; j++)
        {
            if (ingredient.allIngredient[j].id == id)
            {
                break;
            }
        }
        return j;
    }
    private void Start()
    {
        image.sprite = ingredient.itemSprite[FineNumOfArray(idItem)];
    }

    
    private void Update()
    {
        if (reCreate)
        {
            Destroy(gameObject);
        }
    }
}
