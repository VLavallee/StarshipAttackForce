using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    
    GameSession gameSession;
    [SerializeField] bool isGameSessionFound;
    private void Start()
    {
        isGameSessionFound = false;
    }

    private void Update()
    {
        if (isGameSessionFound == false || gameSession == null)
        {
            FindObjectOfType<GameSession>();
            if (FindObjectOfType<GameSession>() != null)
            {
                gameSession = FindObjectOfType<GameSession>();
                isGameSessionFound = true;
            }
        }

        if (scoreText != null && isGameSessionFound)
        {
            scoreText.text = gameSession.GetScore().ToString("0000000");
        }
    }
}
