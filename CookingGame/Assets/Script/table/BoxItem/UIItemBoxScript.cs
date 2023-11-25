using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemBoxScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
     public int numOfItem;
     public int value;
     public int maxValue;
    private void Start()
    {
        
    }
    private void Update()
    {
        slider.maxValue = maxValue;
        text.text = numOfItem.ToString();
        slider.value = value;
    }
}
