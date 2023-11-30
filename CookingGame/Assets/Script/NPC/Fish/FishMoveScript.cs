using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoveScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform point_1;
    public Transform point_2;
    private bool startGo;
    private Animator animatorFish;

    private void Start()
    {
        animatorFish = GetComponent<Animator>();
        transform.position = point_1.position;
        startGo = true;
    }
    private void Update()
    {
        if (startGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, point_2.position, moveSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, point_1.position, moveSpeed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
