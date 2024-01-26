using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame : MonoBehaviour
{
    [SerializeField] private GameObject[] arrow = new GameObject[0]; //^ v < >
    [SerializeField] private Transform[] spawnPoint = new Transform[0]; //^ v < >
    [SerializeField] private SpriteRenderer potion;
    [SerializeField] private ParticleSystem[] particle;
    [SerializeField] private Sprite[] allPotionSprite = new Sprite[0];
    private float speedArrow;
    private float errorProtectionDistance;
    [HideInInspector] public PotionDataScript dataPotion;
    private float timeDelayInput;
    [HideInInspector] public List<int> intAllArrow;
    [HideInInspector] public List<int> intArrow;
    [HideInInspector] public List<GameObject> arrowShowObj;
    [HideInInspector] public List<GameObject> listDelay;
    [HideInInspector] public int difficulty;//3-0
    [HideInInspector] public bool haveRhythm;
    [HideInInspector] public int deHeat;
    [SerializeField] private int healCorrectly;
    [SerializeField] private int healWrong;
    [HideInInspector] public int buttonPressed;
    [HideInInspector] public int arrowAdd;
    public void ClearRhythm()
    {
        intAllArrow.Clear();
        intArrow.Clear();
        listDelay.Clear();
        for (int i = 0; i < arrowShowObj.Count; i++)
        {
            Destroy(arrowShowObj[i], 0);
        }
        arrowShowObj.Clear();
        difficulty = -1;
        buttonPressed = 0;
        haveRhythm = false;
        potion.sprite = allPotionSprite[0];
    }
    private void BuildArrow()//แบบ2
    {
        GameObject _Arrow;
        for (int i = 0; i < intArrow.Count; i++)
        {
            _Arrow = Instantiate(arrow[intArrow[i]], spawnPoint[intArrow[i]], false);
            _Arrow.transform.SetParent(spawnPoint[4]);
            _Arrow.transform.position = spawnPoint[intArrow[i]].position;
            arrowShowObj.Add(_Arrow);
            listDelay.Add(_Arrow);
        }
    }
    private void CheckArrowNew(int i)
    {
        timeDelayInput = 0;
        if (intArrow[0] == i)
        {
            particle[0].Play();
            dataPotion.sound.PlaySoundArrow(true);
            ScoreManeger.score += 5;
            deHeat = healCorrectly;
            buttonPressed++;

            Destroy(arrowShowObj[0], 0);
            Destroy(listDelay[0], 0);
            arrowShowObj.RemoveAt(0);
            listDelay.RemoveAt(0);
            intArrow.RemoveAt(0);
        }
        else
        {
            particle[1].Play();
            dataPotion.sound.PlaySoundArrow(false);
            ScoreManeger.score -= 5;
            deHeat = healWrong * -1;
            listDelay[0].transform.position = spawnPoint[intArrow[0]].position;
        }

        if (buttonPressed >= 4)
        {
            buttonPressed = 0;
            difficulty--;
        }
        arrowAdd = 5;
    }//แบบ2
    private void SetRhythm()
    {
        timeDelayInput += Time.deltaTime;
        if (!haveRhythm)
        {
            switch (difficulty)
            {
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        intArrow.Add(intAllArrow[i]);
                    }
                    break;
                case 2:
                    for (int i = 4; i < 8; i++)
                    {
                        intArrow.Add(intAllArrow[i]);
                    }
                    break;
                case 3:
                    for (int i = 8; i < 12; i++)
                    {
                        intArrow.Add(intAllArrow[i]);
                    }
                    break;
                default:
                    Debug.LogError("Don't have data!!!");
                    break;
            }
            BuildArrow();
            timeDelayInput = 0;
            haveRhythm = true;
        }//เริ่มต้น
    }
    private void MoveRhythm()
    {
        if (intArrow.Count > 0)
        {
            potion.sprite = allPotionSprite[intArrow[0]];
            switch (intArrow[0])
            {
                case 0:
                    listDelay[0].transform.Translate(0, 1f * speedArrow * Time.deltaTime, 0);
                    break;
                case 1:
                    listDelay[0].transform.Translate(0, -1f * speedArrow * Time.deltaTime, 0);
                    break;
                case 2:
                    listDelay[0].transform.Translate(1f * speedArrow * Time.deltaTime, 0, 0);
                    break;
                case 3:
                    listDelay[0].transform.Translate(-1f * speedArrow * Time.deltaTime, 0, 0);
                    break;
                default:
                    break;
            }
        }
    }
    private void Start()
    {
        haveRhythm = false;
        deHeat = 0;
        buttonPressed = 0;
        timeDelayInput = 10;
        arrowAdd = 5;
        potion.sprite = allPotionSprite[0];
        speedArrow = 1.5f * 3 / dataPotion.timeDelayInput;//ใช้ได้เฉพาะ ระยะ 1.5 หน่วย
        errorProtectionDistance = (1.75f / dataPotion.timeDelayInput) - (1.25f / dataPotion.timeDelayInput);
    }
    private void Update()
    {
        SetRhythm();
        MoveRhythm();
        if (intArrow.Count > 0)
        {
            if (timeDelayInput < dataPotion.timeDelayInput - errorProtectionDistance && arrowAdd != 5)
            {
                particle[1].Play();
                CheckArrowNew(4);
            }
            else if (timeDelayInput >= dataPotion.timeDelayInput - errorProtectionDistance && 
                timeDelayInput <= dataPotion.timeDelayInput + errorProtectionDistance && arrowAdd != 5)
            {
                CheckArrowNew(arrowAdd);
            }
            else if (timeDelayInput > dataPotion.timeDelayInput + errorProtectionDistance)
            {
                particle[1].Play();
                CheckArrowNew(4);
            }
        }
        else if (intArrow.Count == 0 && haveRhythm && difficulty >= 0)
        {
            haveRhythm = false;
        }
        if (arrowAdd != 5)
        {
            arrowAdd = 5;
        }
    }
}
