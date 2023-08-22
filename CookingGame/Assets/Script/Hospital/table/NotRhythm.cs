using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotRhythm : MonoBehaviour
{
    [SerializeField] private RawImage[] showPlayerBar;
    [SerializeField] private Scrollbar playerBar;
    private int player = 0;
    private bool increase = true;
    private int[] bar = new int[100];
    public int playerGet = 0;
    public int difficulty = 3;
    private int heart = 0;
    private void RandomStart(int difficulty)
    {
        for (int i = 0; i < bar.Length; i++)
        {
            bar[i] = 0;
            showPlayerBar[i].color = new Color(0, 0, 0, 255);
        }
        int r;
        int s;
        switch (difficulty)
        {
            case 3:
                s = 40;
                break;
            case 2:
                s = 30;
                break;
            case 1:
                s = 15;
                break;
            default :
                s = 10;
                break;
        }
        r = Random.Range(0, 100 - s);
        for (int i = r; i < r + s; i++)
        {
            bar[i] = 15;
            showPlayerBar[i].color = new Color(255, 255, 0, 255);
            if (i > r + 4 && i < r + (s - 5)) 
            {
                bar[i] = 20;
                showPlayerBar[i].color = new Color(0, 255, 0, 255);
            }
        }
    }
    private void Start()
    {
        if (difficulty <= 100 && difficulty >= 60)
        {
            heart = 3;
        }
        else if (difficulty <= 59 && difficulty >= 30)
        {
            heart = 2;
        }
        else if (difficulty <= 29 && difficulty >= 0)
        {
            heart = 1;
        }
        else
        {
            heart = 0;
        }
        RandomStart(heart);
    }

    // Update is called once per frame
    private void Update()
    {
        if (increase)
        {
            player++;
        }
        else if (!increase)
        {
            player--;
        }
        if (player == 198)
        {
            increase = false;
        }
        else if (player == 0)
        {
            increase = true;
        }
        playerBar.value = player/200f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bar[player/2] == 15)
            {
                playerGet = bar[player / 2];
            }
            else if(bar[player / 2] == 20)
            {
                playerGet = bar[player / 2];
            }
            else
            {
                Debug.Log("Bad");
                heart--;
                RandomStart(heart);
            }
            
        }
    }
}
