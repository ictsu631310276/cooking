using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemBoxScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider slider;
    [HideInInspector] public int numOfItem;
    [HideInInspector] public int value;
    [HideInInspector] public int maxValue;
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
