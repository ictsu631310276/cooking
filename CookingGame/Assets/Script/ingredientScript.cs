using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingredientScript : MonoBehaviour
{
    public GameObject plate;
    public GameObject dirtyPlate;
    public IDAndIngredient[] allIngredient;
    public MixFood[] food;
    public float timeUseAuto;
    public float timeUseManuel;
    [System.Serializable]
    public class IDAndIngredient {
        public GameObject ingredient;
        public int id;
        public bool canOnPlate;
    }//ข้อมูลทั้งหมด
    [System.Serializable]
    public class MixFood
    {
        public int[] mixfood;
        public int food;
    }
}
