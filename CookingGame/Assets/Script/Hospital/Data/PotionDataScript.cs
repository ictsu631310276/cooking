using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionDataScript : MonoBehaviour
{
    public CreateSicknessScript[] sicknessData;
    public float timeDelayInput;
    //public MixFood[] mixFood;
    //public float timeUseAuto;
    //public float timeUseManuel;

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
