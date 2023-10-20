using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    private PlayerMoveScript moveScript;
    private ToolPlayerScript toolPlayer;
    private void Start()
    {
        moveScript = GetComponent<PlayerMoveScript>();
        toolPlayer = GetComponent<ToolPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
