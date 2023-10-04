using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class buttom : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    public void OnePlayer()
    {
        UIManagerScript.numOfPlayer = 1;
        SceneManager.LoadScene(1);
    }
    public void TwoPlayer()
    {
        UIManagerScript.numOfPlayer = 2;
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        if (Time.timeScale != 0 && pauseUI != null)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0  && pauseUI != null)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void BackManu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
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
            Pause();
        }
    }
}
