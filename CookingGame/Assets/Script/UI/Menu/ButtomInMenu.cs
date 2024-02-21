using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtomInMenu : MonoBehaviour
{
    [SerializeField] private GameObject playUI;
    [SerializeField] private GameObject buttomContinue;
    [SerializeField] private GameObject continueUI;
    [SerializeField] private GameObject[] dayButtom;
    public void PlayButtom()
    {
        playUI.SetActive(true);
    }
    public void PlayBackButtom()
    {
        playUI.SetActive(false);
    }
    public void OnePlayerButtom()
    {
        UIManagerScript.numOfPlayer = 1;
        CutScene();
    }
    public void TwoPlayerButtom()
    {
        UIManagerScript.numOfPlayer = 2;
        CutScene();
    }
    private void CutScene()
    {
        SceneManager.LoadScene("CutScene1");
    }
    public void GoToTuTorialDayButtom()
    {
        SceneManager.LoadScene("TuDay");
    }
    public void GoToDayOneButtom()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenContinueUI()
    {
        continueUI.SetActive(true);
    }
    public void CloseContinueUI()
    {
        continueUI.SetActive(false);
    }
    public void ExitButtom()
    {
        Application.Quit();
    }
    private void Start()
    {
        Display.displays[0].Activate(0, 0, 0);
        Time.timeScale = 1;
        Debug.Log(PlayerPrefs.GetInt("Day"));
        if (playUI != null)
        {
            playUI.SetActive(false);
        }
        foreach (var item in dayButtom)
        {
            item.SetActive(false);
        }
        if (buttomContinue != null)
        {
            buttomContinue.SetActive(false);

            if (PlayerPrefs.GetInt("Day") > 0)
            {
                buttomContinue.SetActive(true);

                for (int i = 0; i < PlayerPrefs.GetInt("Day") ; i++)
                {
                    dayButtom[i].SetActive(true);
                }
            }
        }
        if (continueUI != null)
        {
            continueUI.SetActive(false);
        }
    }
}
