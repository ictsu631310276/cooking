using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class buttom : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private int day = 1;

    public void OnePlayerButtom()
    {
        UIManagerScript.numOfPlayer = 1;
        SceneManager.LoadScene(day);
    }
    public void TwoPlayerButtom()
    {
        UIManagerScript.numOfPlayer = 2;
        SceneManager.LoadScene(day);
    }
    public void PauseButtom()
    {
        if (Time.timeScale != 0 && pauseUI != null)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0 && pauseUI != null)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void RestartButtom()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void BackManuButtom()
    {
        SceneManager.LoadScene(0);
    }
    public void NextDayButtom()
    {
        day++;
        SceneManager.LoadScene(day);
    }
    public void ExitButtom()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    private void Start()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtom();
        }
    }
}
