using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ingredientScript : MonoBehaviour
{
    public GameObject plate;
    public GameObject dirtyPlate;
    public Sprite[] itemSprite;
    public GameObject[] modelItem;
    public IDAndIngredient[] allIngredient;
    public MixFood[] mixFood;
    public float timeUseAuto;
    public float timeUseManuel;
    [System.Serializable]
    public class IDAndIngredient {
        public string name;
        public int id;
        public int amount;
        public bool canOnPlate;
    }//ข้อมูลทั้งหมด
    [System.Serializable]
    public class MixFood
    {
        public int[] mixfood;
        public int food;
    }
}
