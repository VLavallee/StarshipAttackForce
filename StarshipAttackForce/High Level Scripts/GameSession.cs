using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int timesSaved;
    [SerializeField] int highScore;
    [SerializeField] int elapsedTimeScoreValue;
    [SerializeField] float elapsedScoreTime, totalElapsedTime, elapsedTimeScoreThreshold;

    public int score;
    public int[] topTenScores;
    public bool isTopTenScoreUpdated;
    public bool newTopScore;
    public bool isMusicOn, isSfxOn;
    int numberOfScores = 10;
    int scoreIncreasePerQuarterSec = 5;
    float quarterSecondTimeLimit = 0.25f;

    [SerializeField] bool isLive;
    [SerializeField] float timeAlive;

    public int kills;
    public int totalKills;
    public int bossKills;
    public int totalBossKills;
    public int credits;
    public int creditsThisGame;
    public int creditsCalculatedForThisGame;
    public int creditsPerKill = 100;
    public int creditsPerBossKill = 1000;

    [SerializeField] bool gameIsInSession = true;
    [SerializeField] bool modeSelected = false;
    public bool savedGameLoaded = false;

    public string savedShip;
    public string savedColor;

    public bool defaultArtIsActive = true;
    [SerializeField] private string selectedColor;
    [SerializeField] private string selectedShip;
    [SerializeField] public bool shipColorHasBeenReApplied = true;

    [SerializeField] int currentShipIndexGameSession;
    [SerializeField] int controlType = 1;
    [SerializeField] bool isShipSelectFound = false;

    public bool isUIBottom, isUIRight, isUILeft;
    // owned ships
    // 0 for not owned, 1 for owned. Link this up with game session and ship database
    [SerializeField] int totalAvailableShips;
    public bool availableShipsCalculated;
    [SerializeField] int dcShipOwnedStatus, bomberShipOwnedStatus, saucerShipOwnedStatus, ultDCShipOwnedStatus, ultBomberShipOwnedStatus, ultSaucerShipOwnedStatus;
    [SerializeField] List<GameObject> UIShips;
    

    // refs
    Player player;
    ShipSelect shipSelect;



    private void Awake()
    {
        SetUpSingleton();
        //StartCoroutine(SetVolumeAtGameStart());
        elapsedScoreTime = 0;
    }
    private void Start()
    {
        //ResetShipOwnedStatusAndReplaceCurrency();
        //ClearAllScores();
        Time.timeScale = 1;
        LoadSavedGame();
        StartCoroutine(CheckForDefaultShip());
        StartCoroutine(CheckForDefaultColor());
        StartCoroutine(CalculateTotalAvailableShips());
        Debug.Log("Test.. test..");
    }

    private void Update()
    {
        if (isLive)
        {
            timeAlive += Time.deltaTime;
        }
        if (!shipColorHasBeenReApplied)
        {
            ReApplyPaint();
        }
        if(selectedColor == "")
        {
            //selectedColor = "Default";
        }
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if(player != null)
        {
            if (player.isAlive)
            {
                elapsedScoreTime += Time.deltaTime;
                if(elapsedScoreTime > elapsedTimeScoreThreshold)
                {
                    score += scoreIncreasePerQuarterSec;
                    elapsedScoreTime = 0;
                }
            }
        }
        if(FindObjectOfType<ShipSelect>() == null && !isShipSelectFound)
        {
            isShipSelectFound = false;
        }
        if(!isShipSelectFound)
        {
            FindObjectOfType<ShipSelect>();
            if(FindObjectOfType<ShipSelect>())
            {
                ResetShipSelectIndexAndBool();
                isShipSelectFound = true;
            }
        }
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetTotalGameSaves()
    {
        return timesSaved;
    }
    public void AddToTotalGameSaves()
    {
        timesSaved += 1;
    }
    public void StartFreshTimer()
    {
        timeAlive = 0;
        isLive = true;
    }
    public float GetTimeAlive()
    {
        return timeAlive;
    }
    public void StopLiveTimer()
    {
        isLive = false;
    }
    public void ResetShipSelectIndexAndBool()
    {
        // this function resets the values in ShipSelect so when a new ship is purchased the script will be able to reset the index, enabling the addition of new ships. 
        FindObjectOfType<ShipSelect>().ResetIndex();
        FindObjectOfType<ShipSelect>().ResetBool();
    }
    
    public int GetCurrentShipIndex()
    {
        return currentShipIndexGameSession;
    }
    public void SetCurrentShipIndex(int shipIndex)
    {
        currentShipIndexGameSession = shipIndex;
    }
    public void SetShipName(string shipName)
    {
        selectedShip = shipName;
    }
    
    
    public string GetShipName()
    {
        return selectedShip;
    }
    

    // will either set saved ship to cdShip by default, or pull the name of the last ship that the player used
    IEnumerator CheckForDefaultShip()
    {
        yield return new WaitUntil(() => savedGameLoaded = true);
        if(savedShip == null)
        {
            savedShip = ShipDB.dcShip;
        }
    }

    // will either set saved color to default color or pull the name of the last color used
    IEnumerator CheckForDefaultColor()
    {
        yield return new WaitUntil(() => savedGameLoaded = true);
        if(savedColor == null)
        {
            savedColor = ColorDB.defaultColor;
        }
    }
    
    public void ReApplyPaint()
    {
        if (FindObjectOfType<Player>() == null)
        {
            string color = FindObjectOfType<GameSession>().GetSelectedColor();
            if(FindObjectOfType<PaintSelectButton>() == null)
            {
                // when changing scenes where the paint select button is not active, return without looking to apply colors, since it is useless
                return;
            }
            if (color == "Default")
            {
                if (FindObjectOfType<PaintSelectButton>() != null)
                {
                    FindObjectOfType<PaintSelectButton>().NoPaint();
                }
                shipColorHasBeenReApplied = true;
            }
            if (color == "White")
            {
                FindObjectOfType<PaintSelectButton>().WhitePaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Yellow")
            {
                FindObjectOfType<PaintSelectButton>().YellowPaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Orange")
            {
                FindObjectOfType<PaintSelectButton>().OrangePaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Red")
            {
                FindObjectOfType<PaintSelectButton>().RedPaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Pink")
            {
                FindObjectOfType<PaintSelectButton>().PinkPaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Purple")
            {
                FindObjectOfType<PaintSelectButton>().PurplePaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Blue")
            {
                FindObjectOfType<PaintSelectButton>().BluePaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Green")
            {
                FindObjectOfType<PaintSelectButton>().GreenPaint();
                shipColorHasBeenReApplied = true;
            }
            if (color == "Brown")
            {
                FindObjectOfType<PaintSelectButton>().BrownPaint();
                shipColorHasBeenReApplied = true;
            }
        }
    }
    public string GetSelectedShip()
    {
        return selectedShip;
    }
    public bool GetDefaultArtStatus()
    {
        return defaultArtIsActive;
    }
    public string GetSelectedColor()
    {
        return selectedColor;
    }
    public void SetSelectedColor(string colorName)
    {
        selectedColor = colorName;
    }
    public void ResetCurrency()
    {
        credits = 0;
        SaveGame();
    }
    public void ResetShipOwnedStatusAndReplaceCurrency()
    {
        // for testing purposes. This will delete the ships from the ship database, restore currency to 1,000,000 and save the game
        dcShipOwnedStatus = 1;
        bomberShipOwnedStatus = 0;
        saucerShipOwnedStatus = 0;
        ultDCShipOwnedStatus = 0;
        ultBomberShipOwnedStatus = 0;
        ultSaucerShipOwnedStatus = 0;
        FindObjectOfType<ShipDatabase>().RemoveNonDefaultShipsFromDatabase();
        credits = 0;
        SaveGame();
    }
    public int GetShipOwnershipStatus(string shipName)
    {
        if(shipName == ShipDB.dcShip)
        {
            return dcShipOwnedStatus;
        }
        if (shipName == ShipDB.bomberShip)
        {
            return bomberShipOwnedStatus;
        }
        if (shipName == ShipDB.saucerShip)
        {
            return saucerShipOwnedStatus;
        }
        if (shipName == ShipDB.ultDCShip)
        {
            return ultDCShipOwnedStatus;
        }
        if (shipName == ShipDB.ultBomberShip)
        {
            return ultBomberShipOwnedStatus;
        }
        if (shipName == ShipDB.ultSaucerShip)
        {
            return ultSaucerShipOwnedStatus;
        }
        else
        {
            Debug.Log("String name does not match any known ship. Check Spelling and try again.");
            return 0;
        }
    }
    public int GetShipOwnedStatus(string shipName)
    {
        if(shipName == "DC Ship")
        {
            return dcShipOwnedStatus;
        }
        if(shipName == "Bomber Ship")
        {
            return bomberShipOwnedStatus;
        }
        if(shipName == "Saucer Ship")
        {
            return saucerShipOwnedStatus;
        }
        if(shipName == "Ult DC Ship")
        {
            return ultDCShipOwnedStatus;
        }
        if(shipName == "Ult Bomber Ship")
        {
            return ultBomberShipOwnedStatus;
        }
        if(shipName == "Ult Saucer Ship")
        {
            return ultSaucerShipOwnedStatus;
        }
        else
        {
            Debug.Log("String name does not match any known ship. Check Spelling and try again.");
            return 0;
        }
    }

    public void SetShipOwnedStatusToTrue(string shipName)
    {
        if (shipName == "DC Ship")
        {
            dcShipOwnedStatus = 1;
            return;
        }
        if (shipName == "Bomber Ship")
        {
            bomberShipOwnedStatus = 1;
            return;
        }
        if (shipName == "Saucer Ship")
        {
            saucerShipOwnedStatus = 1;
            return;
        }
        if (shipName == "Ult DC Ship")
        {
            ultDCShipOwnedStatus = 1;
            return;
        }
        if (shipName == "Ult Bomber Ship")
        {
            ultBomberShipOwnedStatus = 1;
            return;
        }
        if (shipName == "Ult Saucer Ship")
        {
            ultSaucerShipOwnedStatus = 1;
            return;
        }
        else
        {
            Debug.Log("String name does not match any known ship. Check Spelling and try again.");
        }
    }

    public void NewLevel()
    {
        //this script will make sure the score is starting at 0 each time the player starts the game again. It should not affect high score values
        // it will be called from another script that doesn't persist through scenes so that it can call through its start method each time. 
        score = 0;
        kills = 0;
        bossKills = 0;
        creditsThisGame = 0;
        creditsCalculatedForThisGame = 0;
        newTopScore = false;
        isTopTenScoreUpdated = false;
    }
    public int GetCredits()
    {
        return credits;
    }
    public int GetCreditsThisGame()
    {
        return creditsThisGame;
    }
    public int GetCreditsPerKill()
    {
        return creditsPerKill;
    }
    public int GetCreditsPerBossKill()
    {
        return creditsPerBossKill;
    }
    public void IncreaseCredits(int creditsToAdd)
    {
        credits += creditsToAdd;
        creditsThisGame += creditsToAdd;
    }
    public void DecreaseCredits(int creditsToSubtract)
    {
        credits -= creditsToSubtract;
        SaveGame();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void AddToScore(int pointsPerKill)
    {
        score += pointsPerKill;
    }

    public int GetKills()
    {
        return kills;
    }
    public int GetTotalKills()
    {
        return totalKills;
    }

    public void KillsPlusOne()
    {
        kills += 1;
        totalKills += 1;
    }
    public int GetBossKills()
    {
        return bossKills;
    }
    public int GetTotalBossKills()
    {
        return totalBossKills;
    }
    public void BossKillsPlusOne()
    {
        bossKills += 1;
        totalBossKills += 1;
    }
    public void ClearAllScores()
    {
        kills = 0;
        totalKills = 0;
        bossKills = 0;
        totalBossKills = 0;
        score = 0;
        highScore = 0;
        topTenScores = new int[topTenScores.Length];
        for(int i = 0; i < topTenScores.Length; i++)
        {
            topTenScores[i] = 0;
        }
        SaveGame();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int CalculateHighScore()
    {
        if(score > highScore)
        {
            highScore = score;
        }
        return highScore;
    }
    public void GameIsInSession()
    {
        gameIsInSession = true;
    }

    public void GameIsNoLongerInSession()
    {
        gameIsInSession = false;
    }

    public void SaveGame()
    {
        AddToTotalGameSaves();
        SaveSystem.SavePlayer(this);
    }

    public void LoadSavedGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        timesSaved = data.timesSaved;
        for(int i = 0; i < topTenScores.Length; i++)
        {
            topTenScores[i] = data.topTenScores[i];
        }
        highScore = data.highScore;
        kills = data.totalKills;
        credits = data.currency;
        controlType = data.controlType;
        if(controlType == 0)
        {
            controlType = 1;
        }
        dcShipOwnedStatus = 1;
        bomberShipOwnedStatus = data.bomberShipOwnedStatus;
        saucerShipOwnedStatus = data.saucerShipOwnedStatus;
        ultDCShipOwnedStatus = data.ultDCShipOwnedStatus;
        ultBomberShipOwnedStatus = data.ultBomberShipOwnedStatus;
        ultSaucerShipOwnedStatus = data.ultSaucerShipOwnedStatus;
        isMusicOn = data.isMusicOn;
        isSfxOn = data.isSfxOn;

        savedShip = data.savedShip;
        savedColor = data.savedColor;
        if(savedShip == null)
        {
            savedShip = ShipDB.dcShip;
        }
        if(savedColor == null)
        {
            savedColor = ColorDB.defaultColor;
        }

        isUIBottom = data.isUIBottom;
        isUIRight = data.isUIRight;
        isUILeft = data.isUILeft;

        savedGameLoaded = true;
    }
    public void SetOffsetHandling()
    {
        controlType = 1;
    }
    public void SetPointerHanding()
    {
        controlType = 2;
    }
    public int GetControlType()
    {
        return controlType;
    }
    public int FindScorePosition(int inputScoreValue)
    {
        var numOfRanks = topTenScores.Length;
        for (int i = 0; i < numOfRanks; i++)
        {
            if (inputScoreValue > topTenScores[i])
            {
                return i;
            }
        }
        return 1;
    }
    public void SetNewTopTenScore(int newScorePosition, int inputScoreValue)
    {
        var numOfRanks = topTenScores.Length;

        for (int i = numOfRanks - 1; i > newScorePosition; i--)
        {
            topTenScores[i] = topTenScores[i - 1];
        }
        topTenScores[newScorePosition] = inputScoreValue;
        if(topTenScores[newScorePosition] == topTenScores[0])
        {
            newTopScore = true;
        }
        isTopTenScoreUpdated = true;
    }
    public void RecalculateTotalAvailableShips()
    {
        StartCoroutine(CalculateTotalAvailableShips());
    }
    IEnumerator CalculateTotalAvailableShips()
    {
        yield return new WaitUntil(() => savedGameLoaded);

        // scorch starship is the default free ship so the totalAvailableShips should always be 1 or higher
        if(dcShipOwnedStatus == 1)
        {
            totalAvailableShips += 1;
        }
        if(bomberShipOwnedStatus == 1)
        {
            totalAvailableShips += 1;
        }
        if (saucerShipOwnedStatus == 1)
        {
            totalAvailableShips += 1;
        }
        if (ultDCShipOwnedStatus == 1)
        {
            totalAvailableShips += 1;
        }
        if (ultBomberShipOwnedStatus == 1)
        {
            totalAvailableShips += 1;
        }
        if (ultSaucerShipOwnedStatus == 1)
        {
            totalAvailableShips += 1;
        }
        availableShipsCalculated = true;
    }
    public int GetTotalAvailableShips()
    {
        if (availableShipsCalculated)
        {
            return totalAvailableShips;
        }
        else return 1;
    }

}
