using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStopTime : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            if(Time.timeScale == 0f)
                Time.timeScale = 1f;
            else
                Time.timeScale = 0f;
        }
            
    }
}
