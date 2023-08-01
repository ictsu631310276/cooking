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

    private void Start()
    {
        image.sprite = ingredient.itemData[ingredient.FindNumOfArray(idItem)].imageItem;
    }

    
    private void Update()
    {
        if (reCreate)
        {
            Destroy(gameObject);
        }
    }
}
