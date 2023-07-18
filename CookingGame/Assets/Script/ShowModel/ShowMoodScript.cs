using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMoodScript : MonoBehaviour
{
    public GameObject veryHappyUI;
    public GameObject happyUI;
    public GameObject angryUI;
    public GameObject veryAngryUI;
    public float timeEat;
    public float timeShowMood;
    private float timeMood;
    private void Start()
    {
        veryHappyUI.SetActive(false);
        happyUI.SetActive(false);
        angryUI.SetActive(false);
        veryAngryUI.SetActive(false);
    }
    public void ShowMood(int mood)//-2,-1,0,1,2
    {
        if (mood != 0)
        {
            switch (mood)
            {
                case 2:
                    veryHappyUI.SetActive(true);
                    break;
                case 1:
                    happyUI.SetActive(true);
                    break;
                case -1:
                    angryUI.SetActive(true);
                    break;
                case -2:
                    veryAngryUI.SetActive(true);
                    break;
                default:
                    break;
            }
            timeMood = timeMood + Time.deltaTime;
            if (timeMood >= timeShowMood)
            {
                timeMood = 0;
                veryHappyUI.SetActive(false);
                happyUI.SetActive(false);
                angryUI.SetActive(false);
                veryAngryUI.SetActive(false);
            }
        }
    }
}
