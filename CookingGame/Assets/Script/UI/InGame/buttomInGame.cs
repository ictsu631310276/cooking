using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class buttomInGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackManuButtom()
    {
        SceneManager.LoadScene(0);
    }
    public void NextDayButtom()
    {
        UIManagerScript.dayInGame++;
        SceneManager.LoadScene(UIManagerScript.dayInGame);
        if (PlayerPrefs.GetInt("Day") <= UIManagerScript.dayInGame)
        {
            PlayerPrefs.SetInt("Day", UIManagerScript.dayInGame);
        }
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
