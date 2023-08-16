using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScript : MonoBehaviour
{
    public ingredientScript ingredient;
    public static int numOfDunWillGo = 0;
    public DataDungeon dataDungeon;
    public GameObject informationUI;
    public ItemDungeon itemShow;
    public GameObject allItem;
    public GameObject loading;

    private float i = 2f;
    public List<int> amountGet;
    public void GoingToDungeon()
    {
        loading.SetActive(true);
        if (numOfDunWillGo != 0)
        {
            RandRomGetItem();
            ShowInformation();
        }
    }
    private void RandRomGetItem() {
        switch (numOfDunWillGo)
        {
            case 1:
                foreach (int i in dataDungeon.itemGetD1)
                {
                    int j = Random.Range(1, 3);
                    amountGet.Add(j);
                    ingredient.itemData[ingredient.FindNumOfArray(i)].amount += j;
                }
                break;
            case 2:
                foreach (int i in dataDungeon.itemGetD2)
                {
                    int j = Random.Range(1, 3);
                    amountGet.Add(j);
                    ingredient.itemData[ingredient.FindNumOfArray(i)].amount += j;
                }
                break;
            case 3:
                foreach (int i in dataDungeon.itemGetD3)
                {
                    int j = Random.Range(1, 3);
                    amountGet.Add(j);
                    ingredient.itemData[ingredient.FindNumOfArray(i)].amount += j;
                }
                break;
            case 4:
                foreach (int i in dataDungeon.itemGetD4)
                {
                    int j = Random.Range(1, 3);
                    amountGet.Add(j);
                    ingredient.itemData[ingredient.FindNumOfArray(i)].amount += j;
                }
                break;
            case 5:
                foreach (int i in dataDungeon.itemGetD5)
                {
                    int j = Random.Range(1, 3);
                    amountGet.Add(j);
                    ingredient.itemData[ingredient.FindNumOfArray(i)].amount += j;
                }
                break;
        }
    }
    private void ShowInformation()
    {
        informationUI.SetActive(true);
        int j = 0;
        switch (numOfDunWillGo)
        {
            case 1:
                foreach (int i in dataDungeon.itemGetD1)
                {
                    ItemDungeon item = Instantiate(itemShow, allItem.transform, false);
                    item.imageItem = ingredient.itemData[ingredient.FindNumOfArray(i)].imageItem;
                    item.amount = amountGet[j];
                    j++;
                }
                break;
            case 2:
                foreach (int i in dataDungeon.itemGetD2)
                {
                    ItemDungeon item = Instantiate(itemShow, allItem.transform, false);
                    item.imageItem = ingredient.itemData[ingredient.FindNumOfArray(i)].imageItem;
                    item.amount = amountGet[j];
                    j++;
                }
                break;
            case 3:
                foreach (int i in dataDungeon.itemGetD3)
                {
                    ItemDungeon item = Instantiate(itemShow, allItem.transform, false);
                    item.imageItem = ingredient.itemData[ingredient.FindNumOfArray(i)].imageItem;
                    item.amount = amountGet[j];
                    j++;
                }
                break;
            case 4:
                foreach (int i in dataDungeon.itemGetD4)
                {
                    ItemDungeon item = Instantiate(itemShow, allItem.transform, false);
                    item.imageItem = ingredient.itemData[ingredient.FindNumOfArray(i)].imageItem;
                    item.amount = amountGet[j];
                    j++;
                }
                break;
            case 5:
                foreach (int i in dataDungeon.itemGetD5)
                {
                    ItemDungeon item = Instantiate(itemShow, allItem.transform, false);
                    item.imageItem = ingredient.itemData[ingredient.FindNumOfArray(i)].imageItem;
                    item.amount = amountGet[j];
                    j++;
                }
                break;
        }
        amountGet.Clear();
    }
    public void GoHome()
    {

    }
    private void Start()
    {
        informationUI.SetActive(false);
    }
    private void Update()
    {
        i = i - Time.deltaTime;
        if (i < 0)
        {
            loading.SetActive(false);
            i = 2f;
        }
    }
}
[System.Serializable]
public class DataDungeon
{
    public int[] itemGetD1;
    public int[] itemGetD2;
    public int[] itemGetD3;
    public int[] itemGetD4;
    public int[] itemGetD5;
}
