using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncentoryScript : MonoBehaviour
{
    public ingredientScript ingredient;
    public List<GetItemScript> AllItem;
    public GetItemScript showItem;
    public GameObject content;
    public void AllButtom()
    {
        OpenIncentory(0);
    }
    public void MeatButtom()
    {
        OpenIncentory(1);
    }
    public void FishButtom()
    {
        OpenIncentory(2);
    }
    public void ChickenButtom()
    {
        OpenIncentory(3);
    }
    public void VegetableButtom()
    {
        OpenIncentory(4);
    }
    public void SpicesButtom()
    {
        OpenIncentory(5);
    }
    public void LikeButtom()
    {
        DestroyItemInList();
        for (int i = 0; i < ingredient.itemData.Length; i++)
        {
            if (ingredient.itemData[i].like)
            {
                GetItemScript showItem1 = Instantiate(showItem, content.transform, false);
                showItem1.id = ingredient.itemData[i].id;
                showItem1.amount = ingredient.itemData[i].amount;
                showItem1.nameItem = ingredient.itemData[i].name;
                showItem1.imageItem.sprite = ingredient.itemData[i].imageItem;
                showItem1.like = ingredient.itemData[i].like;
                AllItem.Add(showItem1);
            }
        }
    }
    private void DestroyItemInList()
    {
        //GetItemScript.reCreate = true;
        for (int i = 0; i < AllItem.Count; i++)
        {
            AllItem[i].reCreate = true;
        }
    }
    public void OpenIncentory(int type)
    {
        DestroyItemInList();
        int j = type > 0 ? type * 100 : 0;
        for (int i = 0; i < ingredient.itemData.Length; i++)
        {
            if (j == 0)
            {
                if (ingredient.itemData[i].id <= 600)
                {
                    GetItemScript showItem1 = Instantiate(showItem, content.transform, false);
                    showItem1.id = ingredient.itemData[i].id;
                    showItem1.amount = ingredient.itemData[i].amount;
                    showItem1.nameItem = ingredient.itemData[i].name;
                    showItem1.imageItem.sprite = ingredient.itemData[i].imageItem;
                    showItem1.like = ingredient.itemData[i].like;
                    AllItem.Add(showItem1);
                }
            }
            else if (j > 0)
            {
                if (ingredient.itemData[i].id >= j && ingredient.itemData[i].id < j + 100 && ingredient.itemData[i].id <= 600)
                {
                    GetItemScript showItem1 = Instantiate(showItem, content.transform, false);
                    showItem1.id = ingredient.itemData[i].id;
                    showItem1.amount = ingredient.itemData[i].amount;
                    showItem1.nameItem = ingredient.itemData[i].name;
                    showItem1.imageItem.sprite = ingredient.itemData[i].imageItem;
                    showItem1.like = ingredient.itemData[i].like;
                    AllItem.Add(showItem1);
                    //showItem1.transform.parent = content.transform;
                }
            }
        }
    }
    private void Start()
    {
        OpenIncentory(0);//All
    }
    private void Update()
    {
    }
}
