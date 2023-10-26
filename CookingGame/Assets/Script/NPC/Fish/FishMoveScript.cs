using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoveScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform point_1;
    public Transform point_2;
    private bool startGo;

    private void Start()
    {
        transform.position = point_1.position;
        startGo = true;
    }
    private void Update()
    {
        if (startGo)
        {
            transform.Translate(point_2.position * Time.deltaTime * moveSpeed , Space.World);
        }
        else
        {
            transform.Translate(point_1.position * Time.deltaTime * moveSpeed, Space.World);
        }

        if (transform.position.z <= point_2.position.z)
        {
            startGo = false;
        }
        else if (transform.position.z >= point_1.position.z)
        {
            startGo = true;
        }
        
    }
}
