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
    
    private void RandomStart()
    {
        for (int i = 0; i < bar.Length; i++)
        {
            bar[i] = 0;
            showPlayerBar[i].color = new Color(130, 130, 130, 255);
        }
        int r = Random.Range(0, 70);
        for (int i = r; i < r + 30; i++)
        {
            bar[i] = 15;
            showPlayerBar[i].color = new Color(255, 255, 0, 255);
            if (i > r + 4 && i < r + 25) 
            {
                bar[i] = 20;
                showPlayerBar[i].color = new Color(0, 255, 0, 255);
            }
        }
    }
    private void Start()
    {
        RandomStart();
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
                RandomStart();
            }
            
        }
    }
}
