using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLivesDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitPointsRemainingText;
    Player player;

    private void Start()
    {
        hitPointsRemainingText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // checks the player health script to see if player has been found. If it has it attaches the script to the player variable and continues with the code.
        if(FindObjectOfType<PlayerHealthSlider>().PlayerFoundStatus())
        {
            player = FindObjectOfType<Player>();
            {
                //if(player == null || player.playerHealth <= 0)
                //{
                //    hitPointsRemainingText.text = "life: " + 0.ToString();
                //    return;
                //}
                //if (player.playerHealth > 0)
                //{
                //    hitPointsRemainingText.text = "life: " + player.playerHealth.ToString();
                //}
            }
        }
    }
}
