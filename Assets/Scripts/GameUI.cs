using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int newVal)
    {
        scoreText.SetText("Score: " + newVal.ToString());
    }
}
