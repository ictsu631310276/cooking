using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMoodScript : MonoBehaviour
{
    public GameObject happyUI;
    public GameObject angryUI;
    public float timeShowMood;
    private void Start()
    {
        angryUI.SetActive(false);
        happyUI.SetActive(false);
    }
    public void ShowMood(bool yes)
    {
        StartCoroutine(showUIMood(yes));
    }
    private IEnumerator showUIMood(bool yes)
    {
        if (yes)
        {
            happyUI.SetActive(true);
        }
        else
        {
            angryUI.SetActive(true);
        }
        yield return new WaitForSeconds(timeShowMood);
        angryUI.SetActive(false);
        happyUI.SetActive(false);
    }
}
