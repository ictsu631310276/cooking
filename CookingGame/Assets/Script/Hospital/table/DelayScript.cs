using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayScript : MonoBehaviour
{
    public Image image;
    [SerializeField] private PotionDataScript dataPotion;
    public Sprite pullFalse;
    public float time;
    private void Start()
    {
    }
    private void Update()
    {
        image.fillAmount = time / dataPotion.timeDelayInput;
    }
}
