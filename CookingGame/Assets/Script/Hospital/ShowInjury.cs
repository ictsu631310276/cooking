using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInjury : MonoBehaviour
{
    public PotionDataScript potionData;
    public GameObject showImageObj;
    public Image showImage;
    public void CloseImage()
    {
        showImageObj.SetActive(false);
    }
    private void Start()
    {
        CloseImage();
    }
    public void ShowItemWant(int item)
    {
        if (item != 0)
        {
            showImageObj.SetActive(true);
            showImage.sprite = potionData.itemData[potionData.FindNumOfArray(item)].imageItem;
        }
    }
}
