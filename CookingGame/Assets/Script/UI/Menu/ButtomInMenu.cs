using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomInMenu : MonoBehaviour
{
    [SerializeField] private GameObject playUI;
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

    public void ExitButtom()
    {
        Application.Quit();
    }
    private void Start()
    {
        Time.timeScale = 1;
        if (playUI != null)
        {
            playUI.SetActive(false);
        }
    }
}
