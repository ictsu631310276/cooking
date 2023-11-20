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
            transform.position = Vector3.MoveTowards(transform.position, point_2.position, moveSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, point_1.position, moveSpeed);
        }

        if (transform.position == point_2.position)
        {
            startGo = false;
        }
        else if (transform.position == point_1.position)
        {
            startGo = true;
        }
        
    }
}
