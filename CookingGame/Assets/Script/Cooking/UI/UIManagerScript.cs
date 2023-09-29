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
    [SerializeField] private TextMeshProUGUI scoreText;
    public static int score;
    [SerializeField] private int scoreStart;
    [SerializeField] private Slider scoreSlider;

    public static int treated;
    public static int dead;
    [SerializeField] private TextMeshProUGUI treatedTextInGame;
    [SerializeField] private TextMeshProUGUI deadTextInGame;
    [SerializeField] private TextMeshProUGUI treatedText;
    [SerializeField] private TextMeshProUGUI deadText;

    [SerializeField] private GameObject imageEndgameS;
    [SerializeField] private GameObject imageEndgameF;

    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private float timeDownScore;
    private float timeDown;
    [SerializeField] private int scoreDown;
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
        timeDown = 0;
        treated = 0;
        dead = 0;
        score = scoreStart;
        pauseUI.SetActive(false);
        imageEndgameS.SetActive(false);
        imageEndgameF.SetActive(false);
    }
    private void Update()
    {
        //moneyText.text = "money : " + money;
        scoreSlider.value = score;
        timeDown += Time.deltaTime;
        if (timeDown >= timeDownScore)
        {
            timeDown = 0;
            score -= scoreDown;
        }

        treatedTextInGame.text = treated.ToString();
        deadTextInGame.text = dead.ToString();
        treatedText.text = "treated : " + treated;
        deadText.text = "dead : " + dead;

        scoreText.text = "Score : " + score;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (score >= 400)
        {
            gameoverText.text = "Succeed";
            imageEndgameS.SetActive(true);
            imageEndgameF.SetActive(false);
        }
        else if (score < 400)
        {
            gameoverText.text = "Fail";
            imageEndgameF.SetActive(true);
            imageEndgameS.SetActive(false);
        }
    }
}
