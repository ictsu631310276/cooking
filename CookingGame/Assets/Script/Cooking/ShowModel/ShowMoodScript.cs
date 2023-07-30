using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMoodScript : MonoBehaviour
{
    public ShowModelScript showModel;
    public GameObject showItemWantPoint;
    public GameObject veryHappyUI;
    public GameObject happyUI;
    public GameObject angryUI;
    public GameObject veryAngryUI;
    private void Start()
    {
        showItemWantPoint.SetActive(false);
        CloseMood();
    }
    public void CloseMood()
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
        }
    }
    public void ShowItemWant(int item)
    {
        if (item != 0)
        {
            showItemWantPoint.SetActive(true);
            showModel.ShowItemWant(item);
        }
        else
        {
            showItemWantPoint.SetActive(false);
        }
    }
}
