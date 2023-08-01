using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoodScript : MonoBehaviour
{
    public ingredientScript ingredient;
    public Sprite[] allMood;
    public GameObject showImageObj;
    public Image showImage;
    public void CloseMood()
    {
        showImageObj.SetActive(false);
    }
    public void ShowMood(int mood)//,0,1,2,3
    {
        showImage.sprite = allMood[mood];
    }
    private void Start()
    {
        CloseMood();
    }
    public void ShowItemWant(int item)
    {
        if (item != 0)
        {
            showImageObj.SetActive(true);
            showImage.sprite = ingredient.itemData[ingredient.FindNumOfArray(item)].imageItem;
        }
        else
        {
            showImageObj.SetActive(false);
        }
    }
}
