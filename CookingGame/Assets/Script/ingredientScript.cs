using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingredientScript : MonoBehaviour
{
    public GameObject plate;
    public IDAndIngredient[] allIngredient;
    public float timeUseAuto;
    public float timeUseManuel;
    [System.Serializable]
    public class IDAndIngredient {
        public GameObject ingredient;
        public int id;
        public bool canOnPlate;
    }//ข้อมูลทั้งหมด
}
