using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private float timeMax;
    [SerializeField] private float startHotTime;
    [SerializeField] private float hotTimeStay;
    public bool haveHotTime = false;
    public Image Fill;
    private float time;
    private float hotTime;
    //public static bool closeDay = false;//use in restaurant
    private void Start()
    {
        time = 0;
        hotTime = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        Fill.fillAmount = time / timeMax;
        if (time >= timeMax)
        {
            Debug.Log("End Day");
            //closeDay = true;
            //SpawnNPCScript.open = false;
        }
        if (hotTime >= hotTimeStay)
        {   
            haveHotTime = false;
        }
        else if (time >= startHotTime)
        {
            hotTime += Time.deltaTime;
            haveHotTime = true;
        }
    }
}
