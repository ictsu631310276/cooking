using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class ItemData : ScriptableObject
{
    public string nameItem;
    public int id;
    public int amount;
    public bool canOnPlate;
    public Sprite imageItem;
    public GameObject model;
    public bool like;
}
