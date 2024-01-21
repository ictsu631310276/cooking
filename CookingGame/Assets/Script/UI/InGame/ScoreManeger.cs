using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManeger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public static int score;
    public int scorePass;
    [SerializeField] private int scoreStart;
    [SerializeField] private Slider scoreSlider;

    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private float timeDownScore;
    private float timeDown;
    [SerializeField] private int scoreDown;

    public TimeUI timeS;

    private void Start()
    {
        timeDown = 0;
        score = scoreStart;
        if (scoreSlider != null)
        {
            scoreSlider.maxValue = scorePass * 2;
        }
    }

    private void Update()
    {
        if (scoreSlider != null)
        {
            scoreSlider.value = score;
        }
        timeDown += Time.deltaTime;
        if (timeDown >= timeDownScore)
        {
            timeDown = 0;
            score -= scoreDown;
        }

        scoreText.text = "Score : " + score;

        if (score >= scorePass)
        {
            gameoverText.text = "Succeed";
        }
        else if (score < scorePass)
        {
            gameoverText.text = "Fail";
        }

        if (score >= scorePass * 2)
        {
            timeS.time = timeS.timeMax;
        }
    }
}
