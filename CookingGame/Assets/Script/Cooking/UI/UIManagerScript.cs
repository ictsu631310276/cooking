using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public static int money;
    //[SerializeField] private TextMeshProUGUI moneyText;
    public GameObject pauseUI;

    public static int treated = 0;
    public static int dead = 0;
    [SerializeField] private TextMeshProUGUI treatedText;
    [SerializeField] private TextMeshProUGUI deadText;
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
        treatedText.text = "people who have been treated : " + treated + "/40";
        deadText.text = "dead : " + dead + "/40";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
