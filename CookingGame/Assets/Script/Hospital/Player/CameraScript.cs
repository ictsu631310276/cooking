using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private bool followPlayer = true;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;

    public static bool zoomOut = true;

    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    private void Update()
    {
        Vector3 camara;
        if (followPlayer)
        {
            camara = player.position + offSet;
        }
        else
        {
            camara = offSet;
        }
        transform.position = camara;
        if (zoomOut)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
        else
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }
}
