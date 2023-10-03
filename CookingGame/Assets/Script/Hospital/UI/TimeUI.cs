using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public float timeMax;
    [SerializeField] private float startHotTime;
    [SerializeField] private float hotTimeStay;
    public bool haveHotTime = false;
    public bool endDay = false;
    public Image Fill;
    public float time;
    private float hotTime;
    //public static bool closeDay = false;//use in restaurant

    [SerializeField] private GameObject endgameGoj;
    private void Start()
    {
        endDay = false;
        time = 0;
        hotTime = 0;

        endgameGoj.SetActive(false);
        Time.timeScale = 1;
    }
    private void Update()
    {
        time += Time.deltaTime;
        Fill.fillAmount = time / timeMax;
        if (time >= timeMax)
        {
            Debug.Log("End Day");
            endDay = true;
            //closeDay = true;
            //SpawnNPCScript.open = false;

            endgameGoj.SetActive(true);
            Time.timeScale = 0;
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
