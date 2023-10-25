using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour
{
    public TimeUI timeScrpit;
    public GameObject bodyDragon;
    public GameObject fireDragon;
    public GameObject warning;
    private bool changeOpacityWaring = false;
    private float warningOpacityValue = 0;

    private bool startDragon;
    private int right;
    public float speedDragon;

    public float timeWaringPlayer;
    private float timeWaring;
    private void RandomFirePoint()
    {
        int randomNum = Random.Range(0, 2);
        randomNum = 1;
        switch (randomNum)
        {
            case 0:
                transform.rotation = new(0f, 0f, 0f, 0f);
                transform.position = new(0f, 0f, -5f);
                right = 1;
                break;
            case 1:
                transform.rotation = new(0f, 180f, 0f, 0f);
                transform.position = new(0f, 0f, 5f);
                right = -1;
                break;
        }
    }
    private void WaringPlayerPoint()
    {
        if (warningOpacityValue <= -0.5f)
        {
            changeOpacityWaring = true;
        }
        else if (warningOpacityValue >= 2f)
        {
            changeOpacityWaring = false;
        }

        if (changeOpacityWaring)
        {
            warningOpacityValue = warningOpacityValue + 0.05f;
        }
        else
        {
            warningOpacityValue = warningOpacityValue - 0.05f;
        }

        if (warningOpacityValue < 0)
        {
            warning.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0);
        }
        else if (warningOpacityValue > 2.55f)
        {
            warning.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 255);
        }
        else
        {
            warning.GetComponent<Renderer>().material.color = new Color(255, 0, 0, warningOpacityValue);
        }
    }
    private void ResetDragon()
    {
        bodyDragon.transform.position = new Vector3(-30, 35, 0);
        fireDragon.transform.position = new Vector3(50, 13, -5);
        warning.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0);
    } 
    private void Start()
    {
        ResetDragon();
        RandomFirePoint();
    }
    private void Update()
    {
        if (timeScrpit.haveHotTime)
        {
            timeWaring += Time.deltaTime;
            if (timeWaring <= timeWaringPlayer)
            {
                WaringPlayerPoint();
            }
            else
            {
                warning.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0);
                startDragon = true;
            }            
        }

        if (startDragon)
        {
            if (bodyDragon.GetComponent<Transform>().position.x >= -30 &&
                bodyDragon.GetComponent<Transform>().position.x <= 30)
            {
                bodyDragon.transform.Translate(Time.deltaTime * speedDragon * right, 0, 0, Space.World);
            }
            else
            {
                fireDragon.transform.Translate(Time.deltaTime * speedDragon * right * -1, 0, 0, Space.World);
            }
        }
    }
}
