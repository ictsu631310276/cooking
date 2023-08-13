using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public static int money;
    public TextMeshProUGUI moneyText;
    public GameObject pauseUI;
    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        //InteractionPlayerScript.tableInteraction.Clear();
        ToolPlayerScript.PatientID.Clear();
        ToolPlayerScript.bed.Clear();
        //SpawnNPCScript.open = false;

        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void Start()
    {
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        moneyText.text = "money : " + money;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
