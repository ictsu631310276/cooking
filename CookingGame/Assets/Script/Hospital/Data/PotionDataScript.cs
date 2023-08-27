using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionDataScript : MonoBehaviour
{
    public ItemData[] itemData;
    public CreateSicknessScript[] sicknessData;
    //public MixFood[] mixFood;
    //public float timeUseAuto;
    //public float timeUseManuel;

    public int FindNumOfItem(int id)
    {
        int j = 0;
        for (j = 0; j < itemData.Length; j++)
        {
            if (itemData[j].id == id)
            {
                break;
            }
        }
        return j;
    }
    public int FindNumOfSick(int id)
    {
        int j = 0;
        for (j = 0; j < sicknessData.Length; j++)
        {
            if (sicknessData[j].id == id)
            {
                break;
            }
        }
        return j;
    }

    //[System.Serializable]
    //public class MixFood
    //{
    //    public int[] mixfood;
    //    public int food;
    //}
}
