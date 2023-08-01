using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class procesIngredientScript : MonoBehaviour
{
    public Processing[] proces;
}
[System.Serializable] 
public class Processing
{
    public int substrate;
    public int result;
}