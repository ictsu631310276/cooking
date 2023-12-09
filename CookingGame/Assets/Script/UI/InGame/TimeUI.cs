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
    public bool haveHotTime;
    public bool endDay;
    public Image Fill;
    public float time;

    [SerializeField] private GameObject endgameGoj;
    private void Start()
    {
        haveHotTime = false;
        endDay = false;
        time = 0;

        endgameGoj.SetActive(false);
        Time.timeScale = 1;
    }
    private void Update()
    {
        time += Time.deltaTime;
        Fill.fillAmount = time / timeMax;
        if (time >= timeMax)
        {
            endDay = true;

            endgameGoj.SetActive(true);
            Time.timeScale = 0;
        }
        if (time >= startHotTime)
        {
            haveHotTime = true;
        }
    }
}
