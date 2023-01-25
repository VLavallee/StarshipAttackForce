using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuShipDisplay : MonoBehaviour
{
    [SerializeField] Sprite dcDefault, dcWhite, dcYellow, dcOrange, dcRed, dcPink, dcPurple, dcBlue, dcGreen, dcBrown;
    [SerializeField] Sprite bomberDefault, bomberWhite, bomberYellow, bomberOrange, bomberRed, bomberPink, bomberPurple, bomberBlue, bomberGreen, bomberBrown;
    [SerializeField] Sprite saucerDefault, saucerWhite, saucerYellow, saucerOrange, saucerRed, saucerPink, saucerPurple, saucerBlue, saucerGreen, saucerBrown;
    [SerializeField] Sprite ultDCDefault, ultDCWhite, ultDCYellow, ultDCOrange, ultDCRed, ultDCPink, ultDCPurple, ultDCBlue, ultDCGreen, ultDCBrown;
    [SerializeField] Sprite ultBomberDefault, ultBomberWhite, ultBomberYellow, ultBomberOrange, ultBomberRed, ultBomberPink, ultBomberPurple, ultBomberBlue, ultBomberGreen, ultBomberBrown;
    [SerializeField] Sprite ultSaucerDefault, ultSaucerWhite, ultSaucerYellow, ultSaucerOrange, ultSaucerRed, ultSaucerPink, ultSaucerPurple, ultSaucerBlue, ultSaucerGreen, ultSaucerBrown;
    [SerializeField] Animator exhaustAnimator; // each exhaust animation trigger will be called by the ship name
    [SerializeField] SpriteRenderer shipRenderer;
    [SerializeField] Button leftArrowButton, rightArrowButton;
    [SerializeField] string nowDisplaying, color; // only used for quick reference
    GameSession gameSession;
    private void Start()
    {
        DisableAndHideShipSelectArrowButtons();
        gameSession = FindObjectOfType<GameSession>();
        StartCoroutine(ShowShipNameAndColor());
        StartCoroutine(CheckShipMatrix());
    }
    IEnumerator ShowShipNameAndColor()
    {
        yield return new WaitUntil(() => gameSession.savedGameLoaded);
        SetMainMenuShipDisplay();
        nowDisplaying = gameSession.savedShip;
        color = gameSession.savedColor;
    }
    IEnumerator CheckShipMatrix()
    {
        yield return new WaitUntil(() => gameSession.availableShipsCalculated);
        if(gameSession.GetTotalAvailableShips() > 1)
        {
            EnableAndShowShipSelectArrowButtons();
            Debug.Log("Total available ships = " + gameSession.GetTotalAvailableShips() + ". Ship select arrow buttons enabled!");
        }
        if(gameSession.GetTotalAvailableShips() == 1)
        {
            Debug.Log("Total available ships = " + gameSession.GetTotalAvailableShips() + ". Ship select arrow buttons disabled!");
        }
    }
    private void SetMainMenuShipDisplay()
    {
        if(gameSession.GetTotalGameSaves() == 0)
        {
            // on a fresh game start the total game saves will be zero. when the player enters the main menu this script will run and set the initial ship, its color, total number of available ships
            // and then save the game. Without this, the game runs into an error and will not load the ship, and can create more errors if the player tries to buy a new ship before quitting from their
            // first playthrough.
            gameSession.savedShip = ShipDB.dcShip;
            gameSession.savedColor = ColorDB.defaultColor;
            gameSession.RecalculateTotalAvailableShips();
            gameSession.SaveGame();
        }
        if(gameSession.savedShip == ShipDB.dcShip)
        {
            if(gameSession.savedColor == ColorDB.defaultColor)
            {
                shipRenderer.sprite = dcDefault;
            }
            if (gameSession.savedColor == ColorDB.white)
            {
                shipRenderer.sprite = dcWhite;
            }
            if (gameSession.savedColor == ColorDB.yellow)
            {
                shipRenderer.sprite = dcYellow;
            }
            if (gameSession.savedColor == ColorDB.orange)
            {
                shipRenderer.sprite = dcOrange;
            }
            if (gameSession.savedColor == ColorDB.red)
            {
                shipRenderer.sprite = dcRed;
            }
            if (gameSession.savedColor == ColorDB.pink)
            {
                shipRenderer.sprite = dcPink;
            }
            if (gameSession.savedColor == ColorDB.purple)
            {
                shipRenderer.sprite = dcPurple;
            }
            if (gameSession.savedColor == ColorDB.blue)
            {
                shipRenderer.sprite = dcBlue;
            }
            if (gameSession.savedColor == ColorDB.green)
            {
                shipRenderer.sprite = dcGreen;
            }
            if (gameSession.savedColor == ColorDB.brown)
            {
                shipRenderer.sprite = dcBrown;
            }
            exhaustAnimator.SetTrigger(ShipDB.dcShip);
            return;
        }
        if(gameSession.savedShip == ShipDB.bomberShip)
        {
            if (gameSession.savedColor == ColorDB.defaultColor)
            {
                shipRenderer.sprite = bomberDefault;
            }
            if (gameSession.savedColor == ColorDB.white)
            {
                shipRenderer.sprite = bomberWhite;
            }
            if (gameSession.savedColor == ColorDB.yellow)
            {
                shipRenderer.sprite = bomberYellow;
            }
            if (gameSession.savedColor == ColorDB.orange)
            {
                shipRenderer.sprite = bomberOrange;
            }
            if (gameSession.savedColor == ColorDB.red)
            {
                shipRenderer.sprite = bomberRed;
            }
            if (gameSession.savedColor == ColorDB.pink)
            {
                shipRenderer.sprite = bomberPink;
            }
            if (gameSession.savedColor == ColorDB.purple)
            {
                shipRenderer.sprite = bomberPurple;
            }
            if (gameSession.savedColor == ColorDB.blue)
            {
                shipRenderer.sprite = bomberBlue;
            }
            if (gameSession.savedColor == ColorDB.green)
            {
                shipRenderer.sprite = bomberGreen;
            }
            if (gameSession.savedColor == ColorDB.brown)
            {
                shipRenderer.sprite = bomberBrown;
            }
            exhaustAnimator.SetTrigger(ShipDB.bomberShip);
            return;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            if (gameSession.savedColor == ColorDB.defaultColor)
            {
                shipRenderer.sprite = saucerDefault;
            }
            if (gameSession.savedColor == ColorDB.white)
            {
                shipRenderer.sprite = saucerWhite;
            }
            if (gameSession.savedColor == ColorDB.yellow)
            {
                shipRenderer.sprite = saucerYellow;
            }
            if (gameSession.savedColor == ColorDB.orange)
            {
                shipRenderer.sprite = saucerOrange;
            }
            if (gameSession.savedColor == ColorDB.red)
            {
                shipRenderer.sprite = saucerRed;
            }
            if (gameSession.savedColor == ColorDB.pink)
            {
                shipRenderer.sprite = saucerPink;
            }
            if (gameSession.savedColor == ColorDB.purple)
            {
                shipRenderer.sprite = saucerPurple;
            }
            if (gameSession.savedColor == ColorDB.blue)
            {
                shipRenderer.sprite = saucerBlue;
            }
            if (gameSession.savedColor == ColorDB.green)
            {
                shipRenderer.sprite = saucerGreen;
            }
            if (gameSession.savedColor == ColorDB.brown)
            {
                shipRenderer.sprite = saucerBrown;
            }
            exhaustAnimator.SetTrigger(ShipDB.saucerShip);
            return;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            if (gameSession.savedColor == ColorDB.defaultColor)
            {
                shipRenderer.sprite = ultDCDefault;
            }
            if (gameSession.savedColor == ColorDB.white)
            {
                shipRenderer.sprite = ultDCWhite;
            }
            if (gameSession.savedColor == ColorDB.yellow)
            {
                shipRenderer.sprite = ultDCYellow;
            }
            if (gameSession.savedColor == ColorDB.orange)
            {
                shipRenderer.sprite = ultDCOrange;
            }
            if (gameSession.savedColor == ColorDB.red)
            {
                shipRenderer.sprite = ultDCRed;
            }
            if (gameSession.savedColor == ColorDB.pink)
            {
                shipRenderer.sprite = ultDCPink;
            }
            if (gameSession.savedColor == ColorDB.purple)
            {
                shipRenderer.sprite = ultDCPurple;
            }
            if (gameSession.savedColor == ColorDB.blue)
            {
                shipRenderer.sprite = ultDCBlue;
            }
            if (gameSession.savedColor == ColorDB.green)
            {
                shipRenderer.sprite = ultDCGreen;
            }
            if (gameSession.savedColor == ColorDB.brown)
            {
                shipRenderer.sprite = ultDCBrown;
            }
            exhaustAnimator.SetTrigger(ShipDB.ultDCShip);
            return;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            if (gameSession.savedColor == ColorDB.defaultColor)
            {
                shipRenderer.sprite = ultBomberDefault;
            }
            if (gameSession.savedColor == ColorDB.white)
            {
                shipRenderer.sprite = ultBomberWhite;
            }
            if (gameSession.savedColor == ColorDB.yellow)
            {
                shipRenderer.sprite = ultBomberYellow;
            }
            if (gameSession.savedColor == ColorDB.orange)
            {
                shipRenderer.sprite = ultBomberOrange;
            }
            if (gameSession.savedColor == ColorDB.red)
            {
                shipRenderer.sprite = ultBomberRed;
            }
            if (gameSession.savedColor == ColorDB.pink)
            {
                shipRenderer.sprite = ultBomberPink;
            }
            if (gameSession.savedColor == ColorDB.purple)
            {
                shipRenderer.sprite = ultBomberPurple;
            }
            if (gameSession.savedColor == ColorDB.blue)
            {
                shipRenderer.sprite = ultBomberBlue;
            }
            if (gameSession.savedColor == ColorDB.green)
            {
                shipRenderer.sprite = ultBomberGreen;
            }
            if (gameSession.savedColor == ColorDB.brown)
            {
                shipRenderer.sprite = ultBomberBrown;
            }
            exhaustAnimator.SetTrigger(ShipDB.ultBomberShip);
            return;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            if (gameSession.savedColor == ColorDB.defaultColor)
            {
                shipRenderer.sprite = ultSaucerDefault;
            }
            if (gameSession.savedColor == ColorDB.white)
            {
                shipRenderer.sprite = ultSaucerWhite;
            }
            if (gameSession.savedColor == ColorDB.yellow)
            {
                shipRenderer.sprite = ultSaucerYellow;
            }
            if (gameSession.savedColor == ColorDB.orange)
            {
                shipRenderer.sprite = ultSaucerOrange;
            }
            if (gameSession.savedColor == ColorDB.red)
            {
                shipRenderer.sprite = ultSaucerRed;
            }
            if (gameSession.savedColor == ColorDB.pink)
            {
                shipRenderer.sprite = ultSaucerPink;
            }
            if (gameSession.savedColor == ColorDB.purple)
            {
                shipRenderer.sprite = ultSaucerPurple;
            }
            if (gameSession.savedColor == ColorDB.blue)
            {
                shipRenderer.sprite = ultSaucerBlue;
            }
            if (gameSession.savedColor == ColorDB.green)
            {
                shipRenderer.sprite = ultSaucerGreen;
            }
            if (gameSession.savedColor == ColorDB.brown)
            {
                shipRenderer.sprite = ultSaucerBrown;
            }
            exhaustAnimator.SetTrigger(ShipDB.ultSaucerShip);
            return;
        }
    }
    private void SetNameAndColor(string shipName, string color)
    {
        if(shipName == ShipDB.dcShip || shipName == ShipDB.bomberShip || shipName == ShipDB.saucerShip || shipName == ShipDB.ultDCShip || 
            shipName == ShipDB.ultBomberShip || shipName == ShipDB.ultSaucerShip)
        {
            gameSession.savedShip = shipName;
        }
        if (color == ColorDB.defaultColor || color == ColorDB.white || color == ColorDB.yellow || color == ColorDB.orange || 
            color == ColorDB.red || color == ColorDB.pink || color == ColorDB.purple || color == ColorDB.blue || 
            color == ColorDB.green || color == ColorDB.brown)
        {
            gameSession.savedColor = color;
        }
    }
    private void ChangeShip(string shipName, string color)
    {
        if(shipName == ShipDB.dcShip)
        {
            if(color == ColorDB.defaultColor)
            {
                shipRenderer.sprite = dcDefault;
            }
            if(color == ColorDB.white)
            {
                shipRenderer.sprite = dcWhite;
            }
            if (color == ColorDB.yellow)
            {
                shipRenderer.sprite = dcYellow;
            }
            if (color == ColorDB.orange)
            {
                shipRenderer.sprite = dcOrange;
            }
            if (color == ColorDB.red)
            {
                shipRenderer.sprite = dcRed;
            }
            if (color == ColorDB.pink)
            {
                shipRenderer.sprite = dcPink;
            }
            if (color == ColorDB.purple)
            {
                shipRenderer.sprite = dcPurple;
            }
            if (color == ColorDB.blue)
            {
                shipRenderer.sprite = dcBlue;
            }
            if (color == ColorDB.green)
            {
                shipRenderer.sprite = dcGreen;
            }
            if (color == ColorDB.brown)
            {
                shipRenderer.sprite = dcBrown;
            }
            SetNameAndColor(shipName, color);
            exhaustAnimator.SetTrigger(ShipDB.dcShip);
            return;
        }
        if(shipName == ShipDB.bomberShip)
        {
            if(color == ColorDB.defaultColor)
            {
                shipRenderer.sprite = bomberDefault;
            }
            if (color == ColorDB.white)
            {
                shipRenderer.sprite = bomberWhite;
            }
            if (color == ColorDB.yellow)
            {
                shipRenderer.sprite = bomberYellow;
            }
            if (color == ColorDB.orange)
            {
                shipRenderer.sprite = bomberOrange;
            }
            if (color == ColorDB.red)
            {
                shipRenderer.sprite = bomberRed;
            }
            if (color == ColorDB.pink)
            {
                shipRenderer.sprite = bomberPink;
            }
            if (color == ColorDB.purple)
            {
                shipRenderer.sprite = bomberPurple;
            }
            if (color == ColorDB.blue)
            {
                shipRenderer.sprite = bomberBlue;
            }
            if (color == ColorDB.green)
            {
                shipRenderer.sprite = bomberGreen;
            }
            if (color == ColorDB.brown)
            {
                shipRenderer.sprite = bomberBrown;
            }
            SetNameAndColor(shipName, color);
            exhaustAnimator.SetTrigger(ShipDB.bomberShip);
            return;
        }
        if(shipName == ShipDB.saucerShip)
        {
            if(color == ColorDB.defaultColor)
            {
                shipRenderer.sprite = saucerDefault;
            }
            if(color == ColorDB.white)
            {
                shipRenderer.sprite = saucerWhite;
            }
            if(color == ColorDB.yellow)
            {
                shipRenderer.sprite = saucerYellow;
            }
            if(color == ColorDB.orange)
            {
                shipRenderer.sprite = saucerOrange;
            }
            if(color == ColorDB.red)
            {
                shipRenderer.sprite = saucerRed;
            }
            if(color == ColorDB.pink)
            {
                shipRenderer.sprite = saucerPink;
            }
            if(color == ColorDB.purple)
            {
                shipRenderer.sprite = saucerPurple;
            }
            if(color == ColorDB.blue)
            {
                shipRenderer.sprite = saucerBlue;
            }
            if(color == ColorDB.green)
            {
                shipRenderer.sprite = saucerGreen;
            }
            if(color == ColorDB.brown)
            {
                shipRenderer.sprite = saucerBrown;
            }
            SetNameAndColor(shipName, color);
            exhaustAnimator.SetTrigger(ShipDB.saucerShip);
            return;
        }
        if(shipName == ShipDB.ultDCShip)
        {
            if(color == ColorDB.defaultColor)
            {
                shipRenderer.sprite = ultDCDefault; 
            }
            if(color == ColorDB.white)
            {
                shipRenderer.sprite = ultDCWhite;
            }
            if(color == ColorDB.yellow)
            {
                shipRenderer.sprite = ultDCYellow;
            }
            if(color == ColorDB.orange)
            {
                shipRenderer.sprite = ultDCOrange;
            }
            if(color == ColorDB.red)
            {
                shipRenderer.sprite = ultDCRed;
            }
            if(color == ColorDB.pink)
            {
                shipRenderer.sprite = ultDCPink;
            }
            if(color == ColorDB.purple)
            {
                shipRenderer.sprite = ultDCPurple;
            }
            if(color == ColorDB.blue)
            {
                shipRenderer.sprite = ultDCBlue;
            }
            if(color == ColorDB.green)
            {
                shipRenderer.sprite = ultDCGreen;
            }
            if(color == ColorDB.brown)
            {
                shipRenderer.sprite = ultDCBrown;
            }
            SetNameAndColor(shipName, color);
            exhaustAnimator.SetTrigger(ShipDB.ultDCShip);
            return;
        }
        if(shipName == ShipDB.ultBomberShip)
        {
            if(color == ColorDB.defaultColor)
            {
                shipRenderer.sprite = ultBomberDefault;
            }
            if (color == ColorDB.white)
            {
                shipRenderer.sprite = ultBomberWhite;
            }
            if (color == ColorDB.yellow)
            {
                shipRenderer.sprite = ultBomberYellow;
            }
            if (color == ColorDB.orange)
            {
                shipRenderer.sprite = ultBomberOrange;
            }
            if (color == ColorDB.red)
            {
                shipRenderer.sprite = ultBomberRed;
            }
            if(color == ColorDB.pink)
            {
                shipRenderer.sprite = ultBomberPink;
            }
            if (color == ColorDB.purple)
            {
                shipRenderer.sprite = ultBomberPurple;
            }
            if (color == ColorDB.blue)
            {
                shipRenderer.sprite = ultBomberBlue;
            }
            if (color == ColorDB.green)
            {
                shipRenderer.sprite = ultBomberGreen;
            }
            if (color == ColorDB.brown)
            {
                shipRenderer.sprite = ultBomberBrown;
            }
            SetNameAndColor(shipName, color);
            exhaustAnimator.SetTrigger(ShipDB.ultBomberShip);
            return;
        }
        if(shipName == ShipDB.ultSaucerShip)
        {
            if(color == ColorDB.defaultColor)
            {
                shipRenderer.sprite = ultSaucerDefault;
            }
            if (color == ColorDB.white)
            {
                shipRenderer.sprite = ultSaucerWhite;
            }
            if (color == ColorDB.yellow)
            {
                shipRenderer.sprite = ultSaucerYellow;
            }
            if (color == ColorDB.orange)
            {
                shipRenderer.sprite = ultSaucerOrange;
            }
            if (color == ColorDB.red)
            {
                shipRenderer.sprite = ultSaucerRed;
            }
            if(color == ColorDB.pink)
            {
                shipRenderer.sprite = ultSaucerPink;
            }
            if (color == ColorDB.purple)
            {
                shipRenderer.sprite = ultSaucerPurple;
            }
            if (color == ColorDB.blue)
            {
                shipRenderer.sprite = ultSaucerBlue;
            }
            if (color == ColorDB.green)
            {
                shipRenderer.sprite = ultSaucerGreen;
            }
            if (color == ColorDB.brown)
            {
                shipRenderer.sprite = ultSaucerBrown;
            }
            SetNameAndColor(shipName, color);
            exhaustAnimator.SetTrigger(ShipDB.ultSaucerShip);
            return;
        }
    }
    private void DisableAndHideShipSelectArrowButtons()
    {
        leftArrowButton.interactable = false;
        leftArrowButton.image.enabled = false;
        rightArrowButton.interactable = false;
        rightArrowButton.image.enabled = false;
    }
    private void EnableAndShowShipSelectArrowButtons()
    {
        leftArrowButton.interactable = true;
        leftArrowButton.image.enabled = true;
        rightArrowButton.interactable = true;
        rightArrowButton.image.enabled = true;
    }
    public void RightArrow()
    {
        // if all ships are owned the display order should be dc ship -> bomber ship -> saucer ship -> ult dc ship -> ult bomber ship -> ult saucer ship
        // otherwise the next available ship will be displayed
        if(gameSession.savedShip == ShipDB.dcShip)
        {
            if(gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.bomberShip)
        {
            if(gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.saucerShip)
        {
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.ultDCShip)
        {
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.ultBomberShip)
        {
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            if(gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
        }
    }
    public void LeftArrow()
    {
        // if all ships are owned the display order should be ult saucer ship -> ult bomber ship -> ult dc ship -> saucer ship -> bomber ship -> dc ship
        // otherwise the next available ship will be displayed
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.ultBomberShip)
        {
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.ultDCShip)
        {
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if(gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.saucerShip)
        {
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
        }
        if(gameSession.savedShip == ShipDB.bomberShip)
        {
            if (gameSession.GetShipOwnershipStatus(ShipDB.dcShip) == 1)
            {
                ChangeShip(ShipDB.dcShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
        }
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultSaucerShip) == 1)
            {
                ChangeShip(ShipDB.ultSaucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultBomberShip) == 1)
            {
                ChangeShip(ShipDB.ultBomberShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.ultDCShip) == 1)
            {
                ChangeShip(ShipDB.ultDCShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.saucerShip) == 1)
            {
                ChangeShip(ShipDB.saucerShip, gameSession.savedColor);
                return;
            }
            if (gameSession.GetShipOwnershipStatus(ShipDB.bomberShip) == 1)
            {
                ChangeShip(ShipDB.bomberShip, gameSession.savedColor);
                return;
            }
        }
    }
    public void SelectDefaultColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcDefault;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberDefault;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerDefault;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCDefault;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberDefault;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberDefault;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerDefault;
        }
        gameSession.savedColor = ColorDB.defaultColor;
    }
    public void SelectWhiteColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcWhite;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberWhite;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerWhite;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCWhite;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberWhite;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberWhite;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerWhite;
        }
        gameSession.savedColor = ColorDB.white;
    }
    public void SelectYellowColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcYellow;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberYellow;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerYellow;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCYellow;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberYellow;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberYellow;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerYellow;
        }
        gameSession.savedColor = ColorDB.yellow;
    }
    public void SelectOrangeColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcOrange;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberOrange;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerOrange;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCOrange;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberOrange;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberOrange;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerOrange;
        }
        gameSession.savedColor = ColorDB.orange;
    }
    public void SelectRedColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcRed;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberRed;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerRed;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCRed;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberRed;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberRed;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerRed;
        }
        gameSession.savedColor = ColorDB.red;
    }
    public void SelectPinkColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcPink;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberPink;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerPink;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCPink;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberPink;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberPink;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerPink;
        }
        gameSession.savedColor = ColorDB.pink;
    }
    public void SelectPurpleColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcPurple;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberPurple;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerPurple;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCPurple;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberPurple;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberPurple;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerPurple;
        }
        gameSession.savedColor = ColorDB.purple;
    }
    public void SelectBlueColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcBlue;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberBlue;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerBlue;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCBlue;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberBlue;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberBlue;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerBlue;
        }
        gameSession.savedColor = ColorDB.blue;
    }
    public void SelectGreenColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcGreen;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberGreen;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerGreen;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCGreen;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberGreen;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberGreen;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerGreen;
        }
        gameSession.savedColor = ColorDB.green;
    }
    public void SelectBrownColor()
    {
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            shipRenderer.sprite = dcBrown;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            shipRenderer.sprite = bomberBrown;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            shipRenderer.sprite = saucerBrown;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            shipRenderer.sprite = ultDCBrown;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberBrown;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            shipRenderer.sprite = ultBomberBrown;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            shipRenderer.sprite = ultSaucerBrown;
        }
        gameSession.savedColor = ColorDB.brown;
    }
}
