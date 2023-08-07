using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDungeon : MonoBehaviour
{
    public ingredientScript ingredient;
    public int amount;
    public TextMeshProUGUI text;
    public Sprite imageItem;

    private void Start()
    {
        GetComponent<Image>().sprite = imageItem;
        text.text = " + " + amount;
    }
}
