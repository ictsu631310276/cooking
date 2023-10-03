using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class buttom : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    public void PlayGameButtom()
    {
        SceneManager.LoadScene(1);
    }
    public void JoinGameButtom()
    {
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
    public void Exit()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    private void Start()
    {
        pauseUI.SetActive(false);
    }

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
