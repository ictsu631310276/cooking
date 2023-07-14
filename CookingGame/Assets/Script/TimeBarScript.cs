using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarScript : MonoBehaviour
{
    public Slider timeBar;
    public GameObject timeBarUI;
    public void walkingTime(float i,float timeUse)
    {
        timeBar.maxValue = timeUse;
        timeBarUI.SetActive(true);
        timeBar.value = i;
    }
    public void Showtime(bool i)
    {
        timeBarUI.SetActive(i);
    }
    private void Start()
    {
        timeBarUI.SetActive(false);
    }
}
