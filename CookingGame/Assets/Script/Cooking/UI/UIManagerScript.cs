using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public static int money;
    private TextMeshProUGUI moneyText;
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
        ToolPlayerScript.PatientID.Clear();
        ToolPlayerScript.bed.Clear();
        ToolPlayerScript.itemInHand = 0;
        ToolPlayerScript.havePatient = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        //InteractionPlayerScript.tableInteraction.Clear();
        //SpawnNPCScript.open = false;
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
        //moneyText.text = "money : " + money;
        if (Input.GetKeyDown(KeyCode.Escape) && !BedScript.onMinigame)
        {
            Pause();
        }
    }
}
