using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncentoryScript : MonoBehaviour
{
    public static IncentoryScript incentory;
    public ingredientScript ingredient;
    public GetItemScript showItem;
    public GameObject content;
    private void Start()
    {
        incentory = this;
        OpenIncentory();
    }
    public void OpenIncentory()
    {
        for (int i = 0; i < ingredient.allIngredient.Length; i++)
        {
            GetItemScript showItem1 = Instantiate(showItem, content.transform, false);
            showItem1.id = ingredient.allIngredient[i].id;
            showItem1.amount = ingredient.allIngredient[i].amount;
            showItem1.nameItem = ingredient.allIngredient[i].name;
            showItem1.imageItem.sprite = ingredient.itemSprite[i];
            showItem1.transform.parent = content.transform;
        }
    }
    // Update is called once per frame
    private void Update()
    {
    }
}
