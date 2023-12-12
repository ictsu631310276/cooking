using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame : MonoBehaviour
{
    [SerializeField] private GameObject[] arrow = new GameObject[0]; //^ v < >
    [SerializeField] private Transform[] spawnPoint = new Transform[0]; //^ v < >
    [SerializeField] private float speedArrow;
    [SerializeField] private GameObject displayPsition;
    [SerializeField] private PotionDataScript dataPotion;
    private float timeDelayInput;
    [HideInInspector] public List<int> intAllArrow;
    [HideInInspector] public List<int> intArrow;
    [HideInInspector] public List<GameObject> arrowShowObj;
    [HideInInspector] public List<DelayScript> listDelay;
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
    }
    private void ShowRhythmArrow()
    {
        GameObject _Arrow;
        for (int i = 0; i < intArrow.Count; i++)
        {
            _Arrow = Instantiate(arrow[intArrow[i]], displayPsition.transform, false);
            arrowShowObj.Add(_Arrow);
            listDelay.Add(_Arrow.GetComponent<DelayScript>());
        }
    }//แบบ1
    private void CheckArrowOld(int i)
    {
        timeDelayInput = 0;
        if (intArrow[0] == i)
        {
            ScoreManeger.score += 5;
            deHeat = healCorrectly;
            buttonPressed++;
        }
        else
        {
            listDelay[buttonPressed].image.sprite = listDelay[buttonPressed].pullFalse;
            ScoreManeger.score -= 5;
            deHeat = healWrong * -1;
            buttonPressed++;
        }
        intArrow.RemoveAt(0);
        if (buttonPressed >= 4)
        {
            for (int j = 0; j < arrowShowObj.Count; j++)
            {
                Destroy(arrowShowObj[j], 0);
                Destroy(listDelay[j], 0);
            }
            arrowShowObj.Clear();
            listDelay.Clear();
            buttonPressed = 0;
            difficulty--;
        }
        arrowAdd = 5;
    }//แบบ1
    private void BuildArrow()//แบบ2
    {
        GameObject _Arrow;
        for (int i = 0; i < intArrow.Count; i++)
        {
            _Arrow = Instantiate(arrow[intArrow[i]], spawnPoint[intArrow[i]], false);
            _Arrow.transform.SetParent(spawnPoint[4]);
            _Arrow.transform.position = spawnPoint[intArrow[i]].position;
            arrowShowObj.Add(_Arrow);
            listDelay.Add(_Arrow.GetComponent<DelayScript>());
        }
        listDelay[0].transform.position = spawnPoint[4].position;
    }
    private void CheckArrowNew(int i)
    {
        timeDelayInput = 0;
        if (intArrow[0] == i)
        {
            ScoreManeger.score += 5;
            deHeat = healCorrectly;
            buttonPressed++;

            Destroy(arrowShowObj[0], 0);
            Destroy(listDelay[0], 0);
            arrowShowObj.RemoveAt(0);
            listDelay.RemoveAt(0);
            intArrow.RemoveAt(0);
            //if (buttonPressed != 4)
            //{
            //    listDelay[0].transform.position = spawnPoint[4].position;
            //}
        }
        //else
        //{
        //    ScoreManeger.score -= 5;
        //    deHeat = healWrong * -1;

        //    listDelay[buttonPressed].image.sprite = listDelay[buttonPressed].pullFalse;
        //}

        if (buttonPressed >= 4)
        {
            buttonPressed = 0;
            difficulty--;
        }
        arrowAdd = 5;
    }//แบบ2
    private void SetNotRhythm()
    {
        timeDelayInput += Time.deltaTime;
        //if (buttonPressed > 0)
        //{
        //    listDelay[0].time = timeDelayInput;
        //}//ใช้ในแบบแรก
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
                    break;
            }
            //ShowRhythmArrow();
            BuildArrow();
            haveRhythm = true;
        }//เริ่มต้น
    }
    private void Start()
    {
        haveRhythm = false;
        deHeat = 0;
        buttonPressed = 0;
        timeDelayInput = 10;
        arrowAdd = 5;
    }
    private void Update()
    {
        SetNotRhythm();
        if (timeDelayInput < dataPotion.timeDelayInput && listDelay.Count > 0)
        {
            listDelay[0].transform.position = Vector3.MoveTowards(listDelay[0].transform.position, spawnPoint[4].position, speedArrow);
        }

        if (intArrow.Count > 0 && timeDelayInput >= dataPotion.timeDelayInput)
        {
            if (arrowAdd != 5)
            {
                //CheckArrowOld(arrowAdd);
                CheckArrowNew(arrowAdd);
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
