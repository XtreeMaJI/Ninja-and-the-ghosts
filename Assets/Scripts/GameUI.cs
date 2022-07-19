using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    public GameObject gamePanelObj;
    public GameObject gameOverPanelObj;

    private LevelManager lm;

    private void Start()
    {
        lm = Object.FindObjectOfType<LevelManager>();
        ShowGameScreen();
    }

    public void UpdateScore(int newVal)
    {
        scoreText.SetText("Score: " + newVal.ToString());
        finalScoreText.SetText("Your score: " + newVal.ToString());
    }

    private void HidePanels()
    {
        gamePanelObj.SetActive(false);
        gameOverPanelObj.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        HidePanels();
        gameOverPanelObj.SetActive(true);
    }

    public void ShowGameScreen()
    {
        HidePanels();
        gamePanelObj.SetActive(true);
    }

    public void OnRestartButton()
    {
        lm.RestartGame();
    }

}
