using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public ScoreManeger score;

    public static int numOfPlayer;

    public static int treated;
    public static int dead;

    [SerializeField] private TextMeshProUGUI treatedTextInGame;
    [SerializeField] private TextMeshProUGUI deadTextInGame;
    [SerializeField] private TextMeshProUGUI treatedText;
    [SerializeField] private TextMeshProUGUI deadText;

    [SerializeField] private GameObject imageEndgameS;
    [SerializeField] private GameObject imageEndgameF;

    private void Start()
    {
        treated = 0;
        dead = 0;

        imageEndgameS.SetActive(false);
        imageEndgameF.SetActive(false);
    }
    private void Update()
    {
        treatedTextInGame.text = treated.ToString();
        deadTextInGame.text = dead.ToString();
        treatedText.text = "treated : " + treated;
        deadText.text = "dead : " + dead;

        if (ScoreManeger.score >= score.scorePass)
        {
            imageEndgameS.SetActive(true);
            imageEndgameF.SetActive(false);
        }
        else if (ScoreManeger.score < score.scorePass)
        {
            imageEndgameF.SetActive(true);
            imageEndgameS.SetActive(false);
        }
    }
}
