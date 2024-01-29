using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private ScoreManeger score;
    [SerializeField] private TimeUI time;
    public static int numOfPlayer;
    public static int dayInGame = 1;
    public static int treated;
    public static int dead;

    [SerializeField] private TextMeshProUGUI treatedTextInGame;
    [SerializeField] private TextMeshProUGUI deadTextInGame;
    [SerializeField] private TextMeshProUGUI treatedText;
    [SerializeField] private TextMeshProUGUI deadText;

    [SerializeField] private GameObject imageEndgameS;
    [SerializeField] private GameObject imageEndgameF;
    [SerializeField] private GameObject nextDatButtom;

    private SoundScript sound;
    private void Start()
    {
        dayInGame = 1;
        treated = 0;
        dead = 0;

        imageEndgameS.SetActive(false);
        imageEndgameF.SetActive(false);
        nextDatButtom.SetActive(false);

        sound = this.GetComponent<SoundScript>();
    }
    private void Update()
    {
        treatedTextInGame.text = treated.ToString();
        deadTextInGame.text = dead.ToString();
        treatedText.text = "treated : " + treated;
        deadText.text = "dead : " + dead;

        if (time != null)
        {
            if (ScoreManeger.score >= score.scorePass && time.endDay)
            {
                sound.PlaySoundWin();
                imageEndgameS.SetActive(true);
                imageEndgameF.SetActive(false);
                nextDatButtom.SetActive(true);
            }
            else if (ScoreManeger.score < score.scorePass && time.endDay)
            {
                sound.PlaySoundLose();
                imageEndgameF.SetActive(true);
                imageEndgameS.SetActive(false);
                nextDatButtom.SetActive(false);
            }
        }
    }
}
