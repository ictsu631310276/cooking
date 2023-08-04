using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScript : MonoBehaviour
{
    public ingredientScript ingredient;
    public static int numOfDunWillGo = 0;
    public int[] itemGetD1;
    public int[] itemGetD2;
    public int[] itemGetD3;
    public int[] itemGetD4;
    public int[] itemGetD5;
    public void GoingToDungeon()
    {
        RandRomGetItem();
        ShowInformation();
    }
    private void RandRomGetItem() {
        switch (numOfDunWillGo)
        {
            case 0:
                Debug.Log("choose dungeon");
                break;
            case 1:
                for (int i = 0; i < itemGetD1.Length; i++)
                {
                    int j = Random.Range(1, 3);
                    ingredient.itemData[ingredient.FindNumOfArray(itemGetD1[i])].amount += j;
                }
                break;
            case 2:
                for (int i = 0; i < itemGetD1.Length; i++)
                {
                    int j = Random.Range(1, 3);
                    ingredient.itemData[ingredient.FindNumOfArray(itemGetD1[i])].amount += j;
                }
                break;
            case 3:
                for (int i = 0; i < itemGetD1.Length; i++)
                {
                    int j = Random.Range(1, 3);
                    ingredient.itemData[ingredient.FindNumOfArray(itemGetD1[i])].amount += j;
                }
                break;
            case 4:
                for (int i = 0; i < itemGetD1.Length; i++)
                {
                    int j = Random.Range(1, 3);
                    ingredient.itemData[ingredient.FindNumOfArray(itemGetD1[i])].amount += j;
                }
                break;
            case 5:
                for (int i = 0; i < itemGetD1.Length; i++)
                {
                    int j = Random.Range(1, 3);
                    ingredient.itemData[ingredient.FindNumOfArray(itemGetD1[i])].amount += j;
                }
                break;
            default:
                Debug.LogError("numOfDunWillGo != 0-6");
                break;
        }
    }
    private void ShowInformation()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        Debug.Log(numOfDunWillGo);
    }
}
