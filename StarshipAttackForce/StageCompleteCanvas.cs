using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageCompleteCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI enemiesDefeated;
    [SerializeField] GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.GameIsNoLongerInSession();
    }
    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = gameSession.GetScore().ToString("0000000");
        }

        if (highScoreText != null)
        {
            highScoreText.text = gameSession.CalculateHighScore().ToString("0000000");
        }

        if(enemiesDefeated != null)
        {
            enemiesDefeated.text = gameSession.GetKills().ToString();
        }
        
    }
}
