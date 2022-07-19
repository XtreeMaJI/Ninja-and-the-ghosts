using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void RestartGame()
    {
        gameUI.ShowGameScreen();
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);    
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameUI.ShowGameOverScreen();
    }

}
