using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesDefeatedDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesDefeatedText;
    GameSession gameSession;

    private void Start()
    {
        enemiesDefeatedText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        enemiesDefeatedText.text = gameSession.GetKills().ToString("0000");
    }
}
