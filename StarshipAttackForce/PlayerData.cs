using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//required for saving to a file
[System.Serializable]
public class PlayerData
{
    public int timesSaved;

    public int currency;
    public int highScore;
    public int totalKills;
    public int totalBossKills;
    
    public int controlType = 1;

    //new score elements
    public int highScoreFromAllGames;
    public int enemiesKilledFromAllGames;
    public int bossesKilledFromAllGames;
    public int[] topTenScores;
    public float longestTimeSurvived, totalTimePlayed;

    public int dcShipOwnedStatus = 1;
    public int bomberShipOwnedStatus;
    public int saucerShipOwnedStatus;
    public int ultDCShipOwnedStatus;
    public int ultBomberShipOwnedStatus;
    public int ultSaucerShipOwnedStatus;

    public string savedShip;
    public string savedColor;
    public int currentShipIndex;
    public bool defaultArtIsActive;

    public bool isMusicOn;
    public bool isSfxOn;
    public bool isUIBottom, isUIRight, isUILeft;
    //public float[] position; //use this if you want to save the player or some other objects position in the game world
    
    public PlayerData (GameSession gameSession)
    {
        timesSaved = gameSession.GetTotalGameSaves();
        currency = gameSession.GetCredits();
        highScore = gameSession.GetHighScore();
        totalKills = gameSession.GetTotalKills();
        totalBossKills = gameSession.GetTotalBossKills();
        controlType = gameSession.GetControlType();

        topTenScores = new int[gameSession.topTenScores.Length];
        for(int i = 0; i < gameSession.topTenScores.Length; i++)
        {
            topTenScores[i] = gameSession.topTenScores[i];
        }

        dcShipOwnedStatus = gameSession.GetShipOwnedStatus("DC Ship");
        bomberShipOwnedStatus = gameSession.GetShipOwnedStatus("Bomber Ship");
        saucerShipOwnedStatus = gameSession.GetShipOwnedStatus("Saucer Ship");
        ultDCShipOwnedStatus = gameSession.GetShipOwnedStatus("Ult DC Ship");
        ultBomberShipOwnedStatus = gameSession.GetShipOwnedStatus("Ult Bomber Ship");
        ultSaucerShipOwnedStatus = gameSession.GetShipOwnedStatus("Ult Saucer Ship");

        currentShipIndex = gameSession.GetCurrentShipIndex();
        defaultArtIsActive = gameSession.defaultArtIsActive;

        isMusicOn = gameSession.isMusicOn;
        isSfxOn = gameSession.isSfxOn;

        savedShip = gameSession.savedShip;
        savedColor = gameSession.savedColor;

        isUIBottom = gameSession.isUIBottom;
        isUIRight = gameSession.isUIRight;
        isUILeft = gameSession.isUILeft;
        //using this for determining position later on
        //(useless in this case since we dont need GameSession transform but could be used on player in same way)

        //position = new float[3];
        //position[0] = gameSession.transform.position.x;
        //position[1] = gameSession.transform.position.y;
        //position[2] = gameSession.transform.position.z;
    }
}
