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

    public static int treated;
    public static int dead;
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
        treated = 0;
        dead = 0;
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        //moneyText.text = "money : " + money;
        treatedText.text = "people who have been treated : " + treated + "/" + (NewSpawnNPCScript.iDNPC);
        deadText.text = "dead : " + dead + "/" + (NewSpawnNPCScript.iDNPC);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
