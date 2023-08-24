using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Not2Rhythm : MonoBehaviour
{
    [SerializeField] private GameObject[] arrow = new GameObject[4]; //^ v < >
    [SerializeField] private GameObject displayPsition;
    public List<int> showArrow;
    public List<GameObject> arrowShowObj;
    public int scorePlayerGet;
    public int difficulty = 0;//3-0
    private bool haveRhythm = false;
    public void CreateRandomRhythm(int x)
    {
        for (int i = 0; i < x * 4; i++)//จำนวนตัวที่ต้องกด
        {
            int j = Random.Range(0, arrow.Length);
            showArrow.Add(j);
        }
        ShowRhythmArrow(showArrow);
    }
    public void ShowRhythmArrow(List<int> x)
    {
        GameObject _Arrow;
        for (int i = 0; i < x.Count; i++)
        {
            _Arrow = Instantiate(arrow[showArrow[i]], displayPsition.transform, false);
            arrowShowObj.Add(_Arrow);
        }
    }
    private void Update()
    {
        Debug.Log(difficulty);
        if (difficulty != 0 && !haveRhythm)
        {
            CreateRandomRhythm(difficulty);
            haveRhythm = true;
        }
        if (showArrow.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (showArrow[0] == 0)
                {
                    Debug.Log("Goog");
                }
                else
                {
                    Debug.Log("Bad");
                }
                showArrow.RemoveAt(0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (showArrow[0] == 1)
                {
                    Debug.Log("Goog");
                }
                else
                {
                    Debug.Log("Bad");
                }
                showArrow.RemoveAt(0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (showArrow[0] == 2)
                {
                    Debug.Log("Goog");
                }
                else
                {
                    Debug.Log("Bad");
                }
                showArrow.RemoveAt(0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (showArrow[0] == 3)
                {
                    Debug.Log("Goog");
                }
                else
                {
                    Debug.Log("Bad");
                }
                showArrow.RemoveAt(0);
            }
        }
    }
}
