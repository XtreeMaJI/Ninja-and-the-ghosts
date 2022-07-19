using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject playerObj;
    private int score = 0;

    public GameUI gameUI;

    void Update()
    {
        if (score < playerObj.transform.position.x / 10)
        {
            score = (int)(playerObj.transform.position.x / 10);
            gameUI.UpdateScore(score);
        }
         
    }

}
