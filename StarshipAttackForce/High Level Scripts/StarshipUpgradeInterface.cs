using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StarshipUpgradeInterface : MonoBehaviour
{
    // General Parameters

    [SerializeField] TextMeshProUGUI highlightedUpgradeTitleText, highlightedUpgradeDescriptionText, upgradeTextPromptText, upgradeUnlockedText, availablePointsText,
        upgradeYourStarshipText, resetsOnDeathText, touchToLearnMoreText;

    [SerializeField] int availablePoints;
    [SerializeField] string costString, pointString, pointsString, availablePointsString, pointsQuestionString, orString, willReplaceString;
    [Multiline(2)]
    [SerializeField]
    string unlockForString, morePointsRequiredString, skillRequiredString, periodString, touchToLearnOrSkipString,
        touchToLearnOrCloseString, removeString, questionString;
    //unlockForPricePrompt is "unlock for " morePointsNeededPrompt is "more point(s) required for unlock"
    [SerializeField] List<GameObject> allLines, allReqText;
    [SerializeField] List<TextMeshProUGUI> textObjects, costTextObjects;
    [SerializeField] Sprite selectedSprite, deselectedSprite;
    [SerializeField] GameObject confirmButton, cancelButton, skipButton, okButton, closeButton, background, upgradeInterfaceObject;
    public bool isScorch, isBroadshot, isTheVisitor, isQuadranis, isDecimator, isPrototypeX, isOpenedFromPauseMenu, isOpenedFromWaveCompletion;
    [SerializeField] float closeButtonDelayTime;
    [SerializeField] bool isGameSessionFound, currentShipSelected;
    [SerializeField] Color invisibleColor, whiteColor;
    [SerializeField] string whiteColorString;
    // Script References
    GameSession gameSession;
    Player player;
    StageManager stageManager;
    // SCORCH Parameters
    public bool isHeatAmplifierIHighlighted, isHeatAmplifierIIHighlighted, isArmorPlatingHighlighted, isFlamethrowerHighlighted, isShieldGeneratorHighlighted, isExtraArmamentsHighlighted;
    [SerializeField] int heatAmplifierICost, heatAmplifierIICost, armorPlatingCost, flamethrowerCost, shieldGeneratorCost, extraArmamentsCost;
    public bool isHeatAmplifierIActive, isHeatAmplifierIIActive, isArmorPlatingActive, isFlamethrowerActive, isShieldGeneratorActive, isExtraArmamentsActive;
    [SerializeField] List<GameObject> scorchLines, scorchReqText;
    [SerializeField] string heatAmplifierIColorString, heatAmplifierIIColorString, armorPlatingColorString, flamethrowerColorString, shieldGeneratorColorString, extraArmamentsColorString;
    [SerializeField] Image heatAmplifierIButtonImage, heatAmplifierIIButtonImage, flamethrowerButtonImage,
        armorPlatingButtonImage, shieldGeneratorButtonImage, extraArmamentsButtonImage;
    [SerializeField]
    TextMeshProUGUI heatAmplifierINameText, heatAmplifierICostText, heatAmplifierIINameText, heatAmplifierIICostText, heatAmplifierIIReqText, armorPlatingNameText, armorPlatingCostText,
        flamethrowerNameText, flamethrowerCostText, flamethrowerReqText, shieldGeneratorNameText, shieldGeneratorCostText, shieldGeneratorReqText, extraArmamentsNameText, extraArmamentsCostText;
    [Multiline(2)]
    [SerializeField]
    string heatAmplifierIName, heatAmplifierIDesc, heatAmplifierIIName, heatAmplifierIIDesc, armorPlatingName, armorPlatingDesc, flamethrowerName, flamethrowerDesc,
        shieldGeneratorName, shieldGeneratorDesc, extraArmamentsName, extraArmamentsDesc;

    [SerializeField] Color heatAmplifierITextColor, heatAmplifierIITextColor, armorPlatingTextColor, flamethrowerTextColor, shieldGeneratorTextColor, extraArmamentsColor;

    // BROADSHOT PARAMETERS

    public bool isFocusFireHighlighted, isMineEnhancerHighlighted, isWideFireHighlighted, isMinefieldHighlighted, isMultiShotHighlighted, isHeavyDiscountHighlighted;
    [SerializeField] int focusFireCost, mineEnhancerCost, wideFireCost, minefieldCost, multiShotCost, heavyDiscountCost;
    public bool isFocusFireActive, isMineEnhancerActive, isWideFireActive, isMinefieldActive, isMultiShotActive, isHeavyDiscountActive;
    [SerializeField] List<GameObject> broadshotLines, broadshotReqText;
    [SerializeField] string focusFireColorString, mineEnhancerColorString, wideFireColorString, minefieldColorString, multiShotColorString, heavyDiscountColorString;
    [SerializeField] Image focusFireButtonImage, mineEnhancerButtonImage, wideFireButtonImage, minefieldButtonImage, multiShotButtonImage, heavyDiscountButtonImage;
    [SerializeField]
    TextMeshProUGUI focusFireNameText, focusFireCostText, mineEnhancerNameText, mineEnhancerCostText, wideFireNameText, wideFireCostText, minefieldNameText, 
        minefieldCostText, multiShotNameText, multiShotCostText, heavyDiscountNameText, heavyDiscountCostText;
    [Multiline(2)]
    [SerializeField]
    string focusFireName, focusFireDesc, mineEnhancerName, mineEnhancerDesc, wideFireName, wideFireDesc, minefieldName, minefieldDesc, multiShotName, multiShotDesc, heavyDiscountName, heavyDiscountDesc;
    [SerializeField] Color focusFireTextColor, mineEnhancerTextColor, wideFireTextColor, minefieldTextColor, multiShotTextColor, heavyDiscountTextColor;
    // THE VISITOR Parameters

    public bool isBlastReturnHighlighted, isMagneticResonatorHighlighted, isHealBeamHighlighted, isUltraBeamHighlighted, isOrbpocolypseHighlighted, isDeathDanceHighlighted;
    [SerializeField] int blastReturnCost, magneticResonatorCost, healBeamCost, ultraBeamCost, orbpocolypseCost, deathDanceCost;
    public bool isBlastReturnActive, isMagneticResonatorActive, isHealBeamActive, isUltraBeamActive, isOrbpocolypseActive, isDeathDanceActive;
    [SerializeField] List<GameObject> theVisitorLines, theVisitorReqText;
    [SerializeField] string blastReturnColorString, magneticResonatorColorString, healBeamColorString, ultraBeamColorString, orbpocolypsColorString, deathDanceColorString;
    [SerializeField] Image blastReturnButtonImage, magneticResonatorButtonImage, healBeamButtonImage, ultraBeamButtonImage, orbpocolypseButtonImage, deathDanceButtonImage;
    [SerializeField] TextMeshProUGUI blastReturnNameText, blastReturnCostText, magneticResonatorNameText, magneticResonatorCostText, healBeamNameText, healBeamCostText, ultraBeamNameText,
        ultraBeamCostText, ultraBeamReqText, orbpocolypsNameText, orbpocolypseCostText, orbpocolypseReqText, deathDanceNameText, deathDanceCostText;
    [Multiline(2)]
    [SerializeField]
    string blastReturnName, blastReturnDesc, magneticResonatorName, magneticResonatorDesc, healBeamName, healBeamDesc, ultraBeamName, ultraBeamDesc, orbpocolypseName, orbpocolypseDesc,
        deathDanceName, deathDanceDesc;
    [SerializeField] Color blastReturnTextColor, magneticResonatorTextColor, healBeamTextColor, ultraBeamTextColor, orbpocolypseTextColor, deathDanceTextColor;

    // QUADRANIS Parameters

    public bool isResupplyHighlighted, isMissileVolleyHighlighted, isBladeMatrixHighlighted, isShieldRageHighlighted, isTwinBladesHighlighted, isEnergyCoilHighlighted;
    [SerializeField] int resupplyCost, missileVolleyCost, bladeMatrixCost, shieldRageCost, twinBladesCost, energyCoilCost;
    public bool isResupplyActive, isMissileVolleyActive, isBladeMatrixActive, isShieldRageActive, isTwinBladesActive, isEnergyCoilActive;
    [SerializeField] List<GameObject> quadranisLines, quadranisReqText;
    [SerializeField] string resupplyColorString, missileVolleyColorString, bladeMatrixColorString, shieldRageColorString, twinBladesColorString, energyCoilColorString;
    [SerializeField] Image resupplyButtonImage, missileVolleyButtonImage, bladeMatrixButtonImage, shieldRageButtonImage, twinBladesButtonImage, energyCoilButtonImage;
    [SerializeField]
    TextMeshProUGUI resupplyNameText, resupplyCostText, missileVolleyNameText, missileVolleyCostText, bladeMatrixNameText, bladeMatrixCostText, shieldRageNameText,
        shieldRageCostText, twinBladesNameText, twinBladesCostText, energyCoilNameText, energyCoilCostText;
    [Multiline(2)]
    [SerializeField]
    string resupplyName, resupplyDesc, missileVolleyName, missileVolleyDesc, bladeMatrixName, bladeMatrixDesc, shieldRageName, shieldRageDesc, twinBladeName, twinBladeDesc,
        energyCoilName, energyCoilDesc;
    [SerializeField] Color resupplyTextColor, missileVolleyTextColor, bladeMatrixTextColor, shieldRageTextColor, twinBladesTextColor, energyCoilTextColor;

    // DECIMATOR Parameters

    public bool isTurboShotHighlighted, isPowerShotHighlighted, isAttackShieldHighlighted, isDisruptorCannonHighlighted, isMachMissileHighlighted, isTorpedoHighlighted;
    [SerializeField] int turboShotCost, powerShotCost, attackShieldCost, disruptorCannonCost, machMissileCost, torpedoCost;
    public bool isTurboShotActive, isPowerShotActive, isAttackShieldActive, isDisruptorCannonActive, isMachMissileActive, isTorpedoActive;
    [SerializeField] List<GameObject> decimatorLines, decimatorReqText;
    [SerializeField]
    string turboShotColorString, powerShotColorString, attackShieldColorString, disruptorCannonColorString,
        machMissileColorString, torpedoColorString;
    [SerializeField] Image turboShotButtonImage, powerShotButtonImage, attackShieldButtonImage, disruptorCannonButtonImage,
        machMissileButtonImage, torpedoButtonImage;
    [SerializeField]
    TextMeshProUGUI turboShotNameText, turboShotCostText, powerShotNameText, powerShotCostText, attackShieldNameText, attackShieldCostText,
        disruptorCannonNameText, disruptorCannonCostText, machMissileNameText, machMissileCostText, torpedoNameText, torpedoCostText;
    [Multiline(2)]
    [SerializeField]
    string turboShotName, turboShotDesc, powerShotName, powerShotDesc, attackShieldName, attackShieldDesc, disruptorCannonName, disruptorCannonDesc,
        machMissileName, machMissileDesc, torpedoName, torpedoDesc;
    [SerializeField] Color turboShotTextColor, powerShotTextColor, attackShieldTextColor, disruptorCannonTextColor, machMissileTextColor, torpedoTextColor;

    // PROTOTYPE X Parameters

    public bool isAssaultDroneIHighlighted, isAssaultDroneIIHighlighted, isMatterMatrixHighlighted,
        isMatterShatterHighlighted, isAftershockHighlighted, isParticleAcceleratorHighlighted;
    [SerializeField] int assaultDroneICost, assaultDroneIICost, matterMatrixCost, matterShatterCost, aftershockCost, particleAcceleratorCost;
    public bool isAssaultDroneIActive, isAssaultDroneIIActive, isMatterMatrixActive, isMatterShatterActive, isAftershockActive, isParticleAcceleratorActive;
    [SerializeField] List<GameObject> prototypeXLines, prototypeXReqText;
    [SerializeField] string assaultDroneIColorString, assaultDroneIIColorString, matterMatrixColorString,
        matterShatterColorString, aftershockColorString, particleAcceleratorColorString;
    [SerializeField] Image assaultDroneIButtonImage, assaultDroneIIButtonImage, matterMatrixButtonImage,
        matterShatterButtonImage, aftershockButtonImage, particleAcceleratorButtonImage;
    [SerializeField]
    TextMeshProUGUI assaultDroneINameText, assaultDroneICostText, assaultDroneIINameText, assaultDroneIICostText,
        matterMatrixNameText, matterMatrixCostText, matterShatterNameText, matterShatterCostText, aftershockNameText, aftershockCostText,
        particleAcceleratorNameText, particleAcceleratorCostText;
    [Multiline(2)]
    [SerializeField]
    string assaultDroneIName, assaultDroneIDesc, assaultDroneIIName, assaultDroneIIDesc, matterMatrixName, matterMatrixDesc, matterShatterName, matterShatterDesc,
        aftershockName, aftershockDesc, particleAcceleratorName, particleAcceleratorDesc;
    [SerializeField] Color assaultDroneITextColor, assaultDroneIITextColor, matterMatrixTextColor, matterShatterTextColor, aftershockTextColor, particleAcceleratorTextColor;


    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        player = FindObjectOfType<Player>();

        HideAllLines();
        HideAllText();
        ResetAllHighlightedBools();
        ShowCostTexts();
        ShowGeneralTexts();
        StartCoroutine(ActivateCurrentShipUpgrades());
        ShowCloseButton();
        FindGameSessionAndUpdateSkillPointAmount();
    }
    IEnumerator FindActiveGameSession()
    {
        yield return new WaitUntil(() => gameSession != null);
        isGameSessionFound = true;
        if(gameSession.savedShip == ShipDB.dcShip)
        {
            isScorch = true;
            currentShipSelected = true;
        }
        if (gameSession.savedShip == ShipDB.bomberShip)
        {
            isBroadshot = true;
            currentShipSelected = true;
        }
        if (gameSession.savedShip == ShipDB.saucerShip)
        {
            isTheVisitor = true;
            currentShipSelected = true;
        }
        if (gameSession.savedShip == ShipDB.ultBomberShip)
        {
            isQuadranis = true;
            currentShipSelected = true;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            isDecimator = true;
            currentShipSelected = true;
        }
        if (gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            isPrototypeX = true;
            currentShipSelected = true;
        }
        
        FindGameSessionAndUpdateSkillPointAmount();
    }
    IEnumerator ActivateCurrentShipUpgrades()
    {
        yield return new WaitUntil (() => currentShipSelected);
        if(isScorch)
        {
            ShowScorchUpgrades();
        }
        if(isBroadshot)
        {
            ShowBroadshotUpgrades();
        }
        if(isTheVisitor)
        {
            ShowTheVisitorUpgrades();
        }
        if(isQuadranis)
        {
            ShowQuadranisUpgrades();
        }
        if(isDecimator)
        {
            ShowDecimatorUpgrades();
        }
        if(isPrototypeX)
        {
            ShowPrototypeXUpgrades();
        }
    }
    
    public void FindGameSessionAndUpdateSkillPointAmount()
    {
        if(!isGameSessionFound)
        {
            StartCoroutine(FindActiveGameSession());
            return;
        }
        availablePoints = gameSession.currentUpgradePoints;
        availablePointsText.text = availablePointsString + availablePoints.ToString();
    }
    public void UseUpgradePoints(int amountToUse)
    {
        gameSession.currentUpgradePoints -= amountToUse;
        FindGameSessionAndUpdateSkillPointAmount();
    }
    
    public void OpenUpgradeInterface()
    {
        ShowActiveStarshipUpgrades();
    }
    // Resetting all highlighted bools
    private void ResetAllHighlightedBools()
    {
        ResetAllScorchHighlightedBools();
        ResetAllBroadshotHighlightedBools();
        ResetAllTheVisitorHighlightedBools();
        ResetAllQuadranisHighlightedBools();
        ResetAllDecimatorHighlightedBools();
        ResetAllPrototypeXHighlightedBools();
    }
    
    // HIDING & SHOWING General UI OBJECTS
    private void HideAllLines()
    {
        foreach (GameObject line in allLines)
        {
            line.SetActive(false);
        }
    }
    public void ShowCloseButtonAndTouchToLearnOnDelayCoroutine()
    {
        StartCoroutine(ShowCloseButtonAndTouchToLearnOnDelay());
    }
    IEnumerator ShowCloseButtonAndTouchToLearnOnDelay()
    {
        okButton.SetActive(false);
        upgradeTextPromptText.color = invisibleColor;
        upgradeUnlockedText.color = invisibleColor;
        CloseHighlightedInformation();
        yield return new WaitForSecondsRealtime(closeButtonDelayTime);
        ShowTouchToLearnOrCloseText();
        ShowCloseButton();
    }
    public void CloseButtonPress()
    {
        CloseUpgradeInterface();
        if(isOpenedFromWaveCompletion)
        {
            gameSession.ResumeTime();
            isOpenedFromWaveCompletion = false;
        }
    }
    private void ShowSkipButton()
    {
        skipButton.SetActive(true);
    }
    private void ShowOkButton()
    {
        okButton.SetActive(true);
    }
    public void CloseOkButton()
    {
        okButton.SetActive(false);
        upgradeTextPromptText.color = invisibleColor;
        HideUpgradeUnlockedText();
    }
    
    public void CloseSkipButton()
    {
        skipButton.SetActive(false);
    }
    public void CloseHighlightedInformation()
    {
        highlightedUpgradeTitleText.color = invisibleColor;
        highlightedUpgradeDescriptionText.color = invisibleColor;
        HideUpgradePrompt();
        CloseConfirmAndCancelButtons();
        ResetAllHighlightedBools();
    }
    private void HideUpgradeUnlockedText()
    {
        upgradeUnlockedText.color = invisibleColor;
    }
    public void HideTouchToLearnText()
    {
        touchToLearnMoreText.color = invisibleColor;
    }
    public void ShowTouchToLearnOrSkipText()
    {
        touchToLearnMoreText.text = touchToLearnOrSkipString;
        touchToLearnMoreText.color = whiteColor;
    }
    public void ShowTouchToLearnOrCloseText()
    {
        touchToLearnMoreText.text = touchToLearnOrCloseString;
        touchToLearnMoreText.color = whiteColor;
    }
    public void HideUpgradePrompt()
    {
        upgradeTextPromptText.color = invisibleColor;
    }
    private void CloseCloseButton()
    {
        closeButton.SetActive(false);
    }
    private void ShowCloseButton()
    {
        closeButton.SetActive(true);
    }
    public void CloseAndResumeButtonPress()
    {
        closeButton.SetActive(false);
    }
    private void HideAllText()
    {
        foreach (TextMeshProUGUI textObj in textObjects)
        {
            textObj.color = invisibleColor;
        }
    }
    private void CloseConfirmAndCancelButtons()
    {
        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
    }
    private void ShowGeneralTexts()
    {
        upgradeYourStarshipText.color = whiteColor;
        resetsOnDeathText.color = whiteColor;
        availablePointsText.text = availablePointsString + availablePoints.ToString();
        availablePointsText.color = whiteColor;
        touchToLearnMoreText.text = touchToLearnOrCloseString;
        touchToLearnMoreText.color = whiteColor;
    }
    public void CloseUpgradeInterface()
    {
        upgradeInterfaceObject.SetActive(false);
    }
    private void ShowCostTexts()
    {
        foreach (TextMeshProUGUI costObj in costTextObjects)
        {
            costObj.color = whiteColor;
        }
    }
    public void ShowTouchToLearnMoreTextAndCloseButton()
    {
        touchToLearnMoreText.color = whiteColor;
    }
    private void ShowActiveStarshipUpgrades()
    {
        background.SetActive(true);
        ShowCostTexts();
        if (gameSession.savedShip == ShipDB.dcShip)
        {
            ShowScorchUpgrades();
            return;
        }
        if(gameSession.savedShip == ShipDB.bomberShip)
        {
            ShowBroadshotUpgrades();
            return;
        }
        if(gameSession.savedShip == ShipDB.saucerShip)
        {
            ShowTheVisitorUpgrades();
            return;
        }
        if(gameSession.savedShip == ShipDB.ultBomberShip)
        {
            ShowQuadranisUpgrades();
            return;
        }
        if (gameSession.savedShip == ShipDB.ultDCShip)
        {
            ShowDecimatorUpgrades();
            return;
        }
        if(gameSession.savedShip == ShipDB.ultSaucerShip)
        {
            ShowPrototypeXUpgrades();
            return;
        }
    }
    // Button Call Scripts
    public void ConfirmButtonPress()
    {
        if(isScorch)
        {
            if(isHeatAmplifierIHighlighted && !isHeatAmplifierIActive)
            {
                UseUpgradePoints(heatAmplifierICost);
                isHeatAmplifierIActive = true;
                heatAmplifierIButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateHeatAmplifierI();
            }
            if(isHeatAmplifierIIHighlighted && isHeatAmplifierIActive && !isHeatAmplifierIIActive)
            {
                UseUpgradePoints(heatAmplifierIICost);
                isHeatAmplifierIIActive = true;
                heatAmplifierIIButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateHeatAmplifierII();
            }
            if(isArmorPlatingHighlighted && !isArmorPlatingActive)
            {
                UseUpgradePoints(armorPlatingCost);
                isArmorPlatingActive = true;
                armorPlatingButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateArmorPlating();
            }
            if(isFlamethrowerHighlighted && isHeatAmplifierIIActive && !isFlamethrowerActive)
            {
                UseUpgradePoints(flamethrowerCost);
                isFlamethrowerActive = true;
                flamethrowerButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateScorchFlamethrower();
            }
            if(isShieldGeneratorHighlighted && isArmorPlatingActive && !isShieldGeneratorActive)
            {
                UseUpgradePoints(shieldGeneratorCost);
                isShieldGeneratorActive = true;
                shieldGeneratorButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateShieldGenerator();
            }
            if(isExtraArmamentsHighlighted && !isExtraArmamentsActive)
            {
                UseUpgradePoints(extraArmamentsCost);
                isExtraArmamentsActive = true;
                extraArmamentsButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateScorchExtraArmaments();
            }
        }
        if(isBroadshot)
        {
            if(isFocusFireHighlighted && !isFocusFireActive)
            {
                UseUpgradePoints(focusFireCost);
                isFocusFireActive = true;
                isWideFireActive = false;
                focusFireButtonImage.sprite = selectedSprite;
                wideFireButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateBroadshotFocusFire();
            }
            if(isFocusFireHighlighted && isFocusFireActive)
            {
                isFocusFireActive = false;
                focusFireButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                ShowOkButton();

                player.DeactivateBroadshotFocusFire();
            }
            if (isMineEnhancerHighlighted && !isMineEnhancerActive)
            {
                UseUpgradePoints(mineEnhancerCost);
                isMineEnhancerActive = true;
                mineEnhancerButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateBroadshotMineEnhancer();
            }
            if (isWideFireHighlighted && !isWideFireActive)
            {
                UseUpgradePoints(wideFireCost);
                isWideFireActive = true;
                isFocusFireActive = false;
                wideFireButtonImage.sprite = selectedSprite;
                focusFireButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateBroadshotWideFire();
            }
            if(isWideFireHighlighted && isWideFireActive)
            {
                isWideFireActive = false;
                wideFireButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                ShowOkButton();

                player.DeactivateBroadshotWideFire();
            }
            if (isMinefieldHighlighted && !isMinefieldActive)
            {
                UseUpgradePoints(minefieldCost);
                isMinefieldActive = true;
                minefieldButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateBroadshotMinefield();
            }
            if (isMultiShotHighlighted && !isMultiShotActive)
            {
                UseUpgradePoints(multiShotCost);
                isMultiShotActive = true;
                multiShotButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateBroadshotMultiShot();
            }
            if(isHeavyDiscountHighlighted && !isHeavyDiscountActive)
            {
                UseUpgradePoints(heavyDiscountCost);
                isHeavyDiscountActive = true;
                heavyDiscountButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateBroadshotHeavyDiscount();
            }
        }
        if (isTheVisitor)
        {
            if (isBlastReturnHighlighted && !isBlastReturnActive)
            {
                UseUpgradePoints(blastReturnCost);
                isBlastReturnActive = true;
                blastReturnButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateTheVisitorBlastReturn();
            }
            if(isMagneticResonatorHighlighted && !isMagneticResonatorActive)
            {
                UseUpgradePoints(magneticResonatorCost);
                isMagneticResonatorActive = true;
                magneticResonatorButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateTheVisitorMagneticResonator();
            }
            if(isHealBeamHighlighted && !isHealBeamActive)
            {
                UseUpgradePoints(healBeamCost);
                isHealBeamActive = true;
                healBeamButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateTheVisitorHealBeam();
            }
            if(isUltraBeamHighlighted && !isUltraBeamActive)
            {
                UseUpgradePoints(ultraBeamCost);
                isUltraBeamActive = true;
                ultraBeamButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateTheVisitorUltraBeam();
            }
            if (isOrbpocolypseHighlighted && !isOrbpocolypseActive)
            {

                UseUpgradePoints(orbpocolypseCost);
                isOrbpocolypseActive = true;
                orbpocolypseButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateTheVisitorOrbpocolypse();
            }
            if(isDeathDanceHighlighted && !isDeathDanceActive)
            {
                UseUpgradePoints(deathDanceCost);
                isDeathDanceActive = true;
                deathDanceButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateTheVisitorDeathDance();
            }

        }
        if(isQuadranis)
        {
            if (isResupplyHighlighted && !isResupplyActive)
            {
                UseUpgradePoints(resupplyCost);
                isResupplyActive = true;
                resupplyButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateQuadranisResupply();
            }
            if(isMissileVolleyHighlighted && !isMissileVolleyActive)
            {
                UseUpgradePoints(missileVolleyCost);
                isMissileVolleyActive = true;
                missileVolleyButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateQuadranisMissileVolley();
            }
            if (isBladeMatrixHighlighted && !isBladeMatrixActive)
            {
                UseUpgradePoints(bladeMatrixCost);
                isBladeMatrixActive = true;
                bladeMatrixButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateQuadranisBladeMatrix();
            }
            if (isShieldRageHighlighted && !isShieldRageActive)
            {
                UseUpgradePoints(shieldRageCost);
                isShieldRageActive = true;
                shieldRageButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                //player.ActivateQuadranisShieldRage();
            }
            if (isTwinBladesHighlighted && !isTwinBladesActive)
            {
                UseUpgradePoints(twinBladesCost);
                isTwinBladesActive = true;
                twinBladesButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                //player.ActivateQuadranisTwinBlades();
            }
            if (isEnergyCoilHighlighted && !isEnergyCoilActive)
            {
                UseUpgradePoints(energyCoilCost);
                isEnergyCoilActive = true;
                energyCoilButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                //player.ActivateQuadranisEnergyCoil();
            }
        }
        if(isDecimator)
        {
            if (isDisruptorCannonHighlighted && !isDisruptorCannonActive)
            {
                UseUpgradePoints(disruptorCannonCost);
                isDisruptorCannonActive = true;
                disruptorCannonButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateDecimatorDisruptorCannon();
            }
            if (isTurboShotHighlighted && !isTurboShotActive && isDisruptorCannonActive)
            {
                UseUpgradePoints(turboShotCost);
                isTurboShotActive = true;
                isPowerShotActive = false;
                turboShotButtonImage.sprite = selectedSprite;
                powerShotButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateDecimatorTurboShot();
            }
            if(isAttackShieldHighlighted && !isAttackShieldActive)
            {
                UseUpgradePoints(attackShieldCost);
                isAttackShieldActive = true;
                attackShieldButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateDecimatorAttackShield();
            }
            if (isPowerShotHighlighted && !isPowerShotActive && isDisruptorCannonActive)
            {
                UseUpgradePoints(powerShotCost);
                isPowerShotActive = true;
                isTurboShotActive = false;
                powerShotButtonImage.sprite = selectedSprite;
                turboShotButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateDecimatorPowerShot();
            }

            if(isMachMissileHighlighted && !isMachMissileActive)
            {
                UseUpgradePoints(machMissileCost);
                isMachMissileActive = true;
                isTorpedoActive = false;
                machMissileButtonImage.sprite = selectedSprite;
                torpedoButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateDecimatorMachMissile();
            }
            if(isTorpedoHighlighted && !isTorpedoActive)
            {
                UseUpgradePoints(torpedoCost);
                isTorpedoActive = true;
                isMachMissileActive = false;
                torpedoButtonImage.sprite = selectedSprite;
                machMissileButtonImage.sprite = deselectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivateDecimatorTorpedo();
            }
        }
        if(isPrototypeX)
        {
            if(isAssaultDroneIHighlighted && !isAssaultDroneIActive)
            {
                UseUpgradePoints(assaultDroneICost);
                isAssaultDroneIActive = true;
                assaultDroneIButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivatePrototypeXAssaultDroneI();
            }
            if (isAssaultDroneIIHighlighted && !isAssaultDroneIIActive)
            {
                UseUpgradePoints(assaultDroneIICost);
                isAssaultDroneIIActive = true;
                assaultDroneIIButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivatePrototypeXAssaultDroneII();
            }
            if(isMatterMatrixHighlighted && !isMatterMatrixActive)
            {
                UseUpgradePoints(matterMatrixCost);
                isMatterMatrixActive = true;
                matterMatrixButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivatePrototypeXMatterMatrix();
            }
            if(isMatterShatterHighlighted && !isMatterShatterActive)
            {
                UseUpgradePoints(matterShatterCost);
                isMatterShatterActive = true;
                matterShatterButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivatePrototypeXMatterShatter();
            }
            if(isAftershockHighlighted && !isAftershockActive)
            {
                UseUpgradePoints(aftershockCost);
                isAftershockActive = true;
                aftershockButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivatePrototypeXAftershock();
            }
            if(isParticleAcceleratorHighlighted && !isParticleAcceleratorActive)
            {
                UseUpgradePoints(particleAcceleratorCost);
                isParticleAcceleratorActive = true;
                particleAcceleratorButtonImage.sprite = selectedSprite;
                CloseHighlightedInformation();
                CloseConfirmAndCancelButtons();
                upgradeUnlockedText.color = whiteColor;
                ShowOkButton();

                player.ActivatePrototypeXParticleAccelerator();
                player.ReloadCurrentWeapon(player.mainWeapon);
            }
        }
    }

    public void Button1Press()
    {
        CloseOkButton();
        CloseCloseButton();
        CloseSkipButton();
        HideTouchToLearnText();
        HideUpgradeUnlockedText();
        CloseHighlightedInformation();
        
        if (isScorch)
        {
            isHeatAmplifierIHighlighted = true;
            highlightedUpgradeDescriptionText.text = heatAmplifierIDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = heatAmplifierIName;
            highlightedUpgradeTitleText.color = heatAmplifierITextColor;
            if(availablePoints >= heatAmplifierICost && !isHeatAmplifierIActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + heatAmplifierICost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(availablePoints < heatAmplifierICost && !isHeatAmplifierIActive)
            {
                var requiredPoints = heatAmplifierICost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isHeatAmplifierIActive)
            {
                ShowOkButton();
            }
        }
        if(isBroadshot)
        {
            isFocusFireHighlighted = true;
            highlightedUpgradeDescriptionText.text = focusFireDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = focusFireName;
            highlightedUpgradeTitleText.color = focusFireTextColor;
            if(availablePoints >= focusFireCost && !isFocusFireActive && !isWideFireActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + focusFireCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints >= focusFireCost && !isFocusFireActive && isWideFireActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = willReplaceString + wideFireColorString + wideFireName + whiteColorString + periodString + unlockForString + focusFireCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(availablePoints < focusFireCost && !isFocusFireActive)
            {
                var requiredPoints = focusFireCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isFocusFireActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = removeString + focusFireColorString + focusFireName + whiteColorString + questionString;
                upgradeTextPromptText.color = whiteColor;
            }
        }
        if(isTheVisitor)
        {
            isBlastReturnHighlighted = true;
            highlightedUpgradeDescriptionText.text = blastReturnDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = blastReturnName;
            highlightedUpgradeTitleText.color = blastReturnTextColor;
            if (availablePoints >= blastReturnCost && !isBlastReturnActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + blastReturnCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < blastReturnCost && !isBlastReturnActive)
            {
                var requiredPoints = blastReturnCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isBlastReturnActive)
            {
                ShowOkButton();
            }
        }
        if(isQuadranis)
        {
            isResupplyHighlighted = true;
            highlightedUpgradeDescriptionText.text = resupplyDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = resupplyName;
            highlightedUpgradeTitleText.color = resupplyTextColor;
            if (availablePoints >= resupplyCost && !isResupplyActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + resupplyCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < resupplyCost && !isResupplyActive)
            {
                var requiredPoints = resupplyCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isResupplyActive)
            {
                ShowOkButton();
            }
        }
        if(isDecimator)
        {
            isDisruptorCannonHighlighted = true;
            highlightedUpgradeDescriptionText.text = disruptorCannonDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = disruptorCannonName;
            highlightedUpgradeTitleText.color = disruptorCannonTextColor;
            if (availablePoints >= disruptorCannonCost && !isDisruptorCannonActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + disruptorCannonCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < disruptorCannonCost && !isDisruptorCannonActive)
            {
                var requiredPoints = disruptorCannonCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isDisruptorCannonActive)
            {
                ShowOkButton();
            }
        }
        if(isPrototypeX)
        {
            isAssaultDroneIHighlighted = true;
            highlightedUpgradeDescriptionText.text = assaultDroneIDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = assaultDroneIName;
            highlightedUpgradeTitleText.color = assaultDroneITextColor;
            if (availablePoints >= assaultDroneICost && !isAssaultDroneIActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + assaultDroneICost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < assaultDroneICost && !isAssaultDroneIActive)
            {
                var requiredPoints = assaultDroneICost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isAssaultDroneIActive)
            {
                ShowOkButton();
            }
        }
    }
    public void Button2Press()
    {
        CloseOkButton();
        CloseCloseButton();
        CloseSkipButton();
        HideTouchToLearnText();
        HideUpgradeUnlockedText();
        CloseHighlightedInformation();

        if (isScorch)
        {
            isHeatAmplifierIIHighlighted = true;
            highlightedUpgradeDescriptionText.text = heatAmplifierIIDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = heatAmplifierIIName;
            highlightedUpgradeTitleText.color = heatAmplifierIITextColor;
            if (availablePoints >= heatAmplifierIICost && isHeatAmplifierIActive && !isHeatAmplifierIIActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + heatAmplifierIICost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(!isHeatAmplifierIActive)
            {
                upgradeTextPromptText.text = heatAmplifierIColorString + heatAmplifierINameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < heatAmplifierIICost && isHeatAmplifierIActive && !isHeatAmplifierIIActive)
            {
                var requiredPoints = heatAmplifierIICost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isHeatAmplifierIIActive)
            {
                ShowOkButton();
            }
        }
        if(isBroadshot)
        {
            isMineEnhancerHighlighted = true;
            highlightedUpgradeDescriptionText.text = mineEnhancerDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = mineEnhancerName;
            highlightedUpgradeTitleText.color = mineEnhancerTextColor;
            if (availablePoints >= mineEnhancerCost && !isMineEnhancerActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + mineEnhancerCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < mineEnhancerCost)
            {
                var requiredPoints = heatAmplifierIICost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMineEnhancerActive)
            {
                ShowOkButton();
            }
        }
        if(isTheVisitor)
        {
            isMagneticResonatorHighlighted = true;
            highlightedUpgradeDescriptionText.text = magneticResonatorDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = magneticResonatorName;
            highlightedUpgradeTitleText.color = magneticResonatorTextColor;
            if (availablePoints >= magneticResonatorCost && !isMagneticResonatorActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + magneticResonatorCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < magneticResonatorCost && !isMagneticResonatorActive)
            {
                var requiredPoints = magneticResonatorCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMagneticResonatorActive)
            {
                ShowOkButton();
            }
        }
        if(isQuadranis)
        {
            isMissileVolleyHighlighted = true;
            highlightedUpgradeDescriptionText.text = missileVolleyDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = missileVolleyName;
            highlightedUpgradeTitleText.color = missileVolleyTextColor;
            if (availablePoints >= missileVolleyCost && !isMissileVolleyActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + missileVolleyCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < missileVolleyCost && !isMissileVolleyActive)
            {
                var requiredPoints = missileVolleyCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMissileVolleyActive)
            {
                ShowOkButton();
            }
        }
        if (isDecimator)
        {
            isTurboShotHighlighted = true;
            highlightedUpgradeDescriptionText.text = turboShotDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = turboShotName;
            highlightedUpgradeTitleText.color = turboShotTextColor;
            if (availablePoints >= turboShotCost && !isTurboShotActive && isDisruptorCannonActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + turboShotCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            if (availablePoints >= turboShotCost && !isTurboShotActive && isDisruptorCannonActive && isPowerShotActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = willReplaceString + powerShotColorString + powerShotName + whiteColorString + periodString + unlockForString + turboShotCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < turboShotCost && !isTurboShotActive && isDisruptorCannonActive)
            {
                var requiredPoints = turboShotCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (!isDisruptorCannonActive)
            {
                upgradeTextPromptText.text = disruptorCannonColorString + disruptorCannonNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isTurboShotActive)
            {
                ShowOkButton();
            }
        }
        if(isPrototypeX)
        {
            isAssaultDroneIIHighlighted = true;
            highlightedUpgradeDescriptionText.text = assaultDroneIIDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = assaultDroneIIName;
            highlightedUpgradeTitleText.color = assaultDroneIITextColor;
            if (availablePoints >= assaultDroneIICost && isAssaultDroneIActive && !isAssaultDroneIIActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + assaultDroneIICost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isAssaultDroneIActive)
            {
                upgradeTextPromptText.text = assaultDroneIColorString + assaultDroneINameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < assaultDroneIICost && isAssaultDroneIActive && !isAssaultDroneIIActive)
            {
                var requiredPoints = assaultDroneIICost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isAssaultDroneIIActive)
            {
                ShowOkButton();
            }
        }
    }
    public void Button3Press()
    {
        CloseOkButton();
        CloseCloseButton();
        CloseSkipButton();
        HideTouchToLearnText();
        HideUpgradeUnlockedText();
        CloseHighlightedInformation();

        if (isScorch)
        {
            isArmorPlatingHighlighted = true;
            highlightedUpgradeDescriptionText.text = armorPlatingDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = armorPlatingName;
            highlightedUpgradeTitleText.color = armorPlatingTextColor;
            if (availablePoints >= armorPlatingCost && !isArmorPlatingActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + armorPlatingCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < armorPlatingCost && !isArmorPlatingActive)
            {
                var requiredPoints = armorPlatingCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isArmorPlatingActive)
            {
                ShowOkButton();
            }
        }
        if(isBroadshot)
        {
            isWideFireHighlighted = true;
            highlightedUpgradeDescriptionText.text = wideFireDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = wideFireName;
            highlightedUpgradeTitleText.color = wideFireTextColor;
            if(availablePoints >= wideFireCost && !isWideFireActive && !isFocusFireActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + wideFireCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            if(availablePoints >= wideFireCost && !isWideFireActive && isFocusFireActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = willReplaceString + focusFireColorString+ focusFireName + whiteColorString + periodString + unlockForString + wideFireCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            if(availablePoints < wideFireCost && !isWideFireActive)
            {
                var requiredPoints = wideFireCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            if(isWideFireActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = removeString + wideFireColorString + wideFireName + whiteColorString + questionString;
                upgradeTextPromptText.color = whiteColor;
            }
        }
        if (isTheVisitor)
        {
            isHealBeamHighlighted = true;
            highlightedUpgradeDescriptionText.text = healBeamDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = healBeamName;
            highlightedUpgradeTitleText.color = healBeamTextColor;
            if (availablePoints >= healBeamCost && !isHealBeamActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + healBeamCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < healBeamCost && !isHealBeamActive)
            {
                var requiredPoints = healBeamCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isHealBeamActive)
            {
                ShowOkButton();
            }
        }
        if(isQuadranis)
        {
            isBladeMatrixHighlighted = true;
            highlightedUpgradeDescriptionText.text = bladeMatrixDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = bladeMatrixName;
            highlightedUpgradeTitleText.color = bladeMatrixTextColor;
            if (availablePoints >= bladeMatrixCost && !isBladeMatrixActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + bladeMatrixCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < bladeMatrixCost && !isBladeMatrixActive)
            {
                var requiredPoints = bladeMatrixCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isBladeMatrixActive)
            {
                ShowOkButton();
            }
        }
        if (isDecimator)
        {
            isAttackShieldHighlighted = true;
            highlightedUpgradeDescriptionText.text = attackShieldDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = attackShieldName;
            highlightedUpgradeTitleText.color = attackShieldTextColor;
            if (availablePoints >= attackShieldCost && !isAttackShieldActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + attackShieldCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < attackShieldCost && !isAttackShieldActive)
            {
                var requiredPoints = attackShieldCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isAttackShieldActive)
            {
                ShowOkButton();
            }
        }
        if (isPrototypeX)
        {
            isMatterMatrixHighlighted = true;
            highlightedUpgradeDescriptionText.text = matterMatrixDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = matterMatrixName;
            highlightedUpgradeTitleText.color = matterMatrixTextColor;
            if (availablePoints >= matterMatrixCost && !isMatterMatrixActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + matterMatrixCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < matterMatrixCost && !isMatterMatrixActive)
            {
                var requiredPoints = matterMatrixCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMatterMatrixActive)
            {
                ShowOkButton();
            }
        }
    }
    public void Button4Press()
    {
        CloseOkButton();
        CloseCloseButton();
        CloseSkipButton();
        HideTouchToLearnText();
        HideUpgradeUnlockedText();
        CloseHighlightedInformation();

        if (isScorch)
        {
            isFlamethrowerHighlighted = true;
            highlightedUpgradeDescriptionText.text = flamethrowerDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = flamethrowerName;
            highlightedUpgradeTitleText.color = flamethrowerTextColor;
            if (availablePoints >= flamethrowerCost && isHeatAmplifierIIActive && !isFlamethrowerActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + flamethrowerCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(!isHeatAmplifierIIActive)
            {
                upgradeTextPromptText.text = heatAmplifierIIColorString + heatAmplifierIINameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < flamethrowerCost && !isFlamethrowerActive)
            {
                var requiredPoints = flamethrowerCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isFlamethrowerActive)
            {
                ShowOkButton();
            }
        }
        if(isBroadshot)
        {
            isMinefieldHighlighted = true;
            highlightedUpgradeDescriptionText.text = minefieldDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = minefieldName;
            highlightedUpgradeTitleText.color = minefieldTextColor;
            if (availablePoints >= minefieldCost && isMineEnhancerActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + minefieldCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(!isMineEnhancerActive)
            {
                upgradeTextPromptText.text = mineEnhancerColorString + mineEnhancerNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(availablePoints < minefieldCost && isMineEnhancerActive && !isMinefieldActive)
            {
                var requiredPoints = minefieldCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isMinefieldActive)
            {
                ShowOkButton();
            }
        }
        if(isTheVisitor)
        {
            isUltraBeamHighlighted = true;
            highlightedUpgradeDescriptionText.text = ultraBeamDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = ultraBeamName;
            highlightedUpgradeTitleText.color = ultraBeamTextColor;
            if (availablePoints >= ultraBeamCost && isHealBeamActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + ultraBeamCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(!isHealBeamActive)
            {
                upgradeTextPromptText.text = healBeamColorString + healBeamNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(availablePoints < ultraBeamCost && isHealBeamActive)
            {
                var requiredPoints = ultraBeamCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isUltraBeamActive)
            {
                ShowOkButton();
            }
        }
        if(isQuadranis)
        {
            isShieldRageHighlighted = true;
            highlightedUpgradeDescriptionText.text = shieldRageDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = shieldRageName;
            highlightedUpgradeTitleText.color = shieldRageTextColor;
            if (availablePoints >= shieldRageCost && !isShieldRageActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + shieldRageCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < shieldRageCost && !isShieldRageActive)
            {
                var requiredPoints = shieldRageCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isShieldRageActive)
            {
                ShowOkButton();
            }
        }
        if (isDecimator)
        {
            isPowerShotHighlighted = true;
            highlightedUpgradeDescriptionText.text = powerShotDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = powerShotName;
            highlightedUpgradeTitleText.color = powerShotTextColor;
            if (availablePoints >= powerShotCost && !isTurboShotActive && !isPowerShotActive && isDisruptorCannonActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + powerShotCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            if (availablePoints >= powerShotCost && !isPowerShotActive && isTurboShotActive && isDisruptorCannonActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = willReplaceString + turboShotColorString + turboShotName + whiteColorString + periodString + unlockForString + powerShotCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isDisruptorCannonActive)
            {
                upgradeTextPromptText.text = disruptorCannonColorString + disruptorCannonNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < powerShotCost && !isPowerShotActive)
            {
                var requiredPoints = turboShotCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isPowerShotActive)
            {
                ShowOkButton();
            }
        }
        if(isPrototypeX)
        {
            isMatterShatterHighlighted = true;
            highlightedUpgradeDescriptionText.text = matterShatterDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = matterShatterName;
            highlightedUpgradeTitleText.color = matterShatterTextColor;
            if (availablePoints >= matterShatterCost && isMatterMatrixActive && !isMatterShatterActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + matterShatterCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isMatterMatrixActive)
            {
                upgradeTextPromptText.text = matterMatrixColorString + matterMatrixNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < matterShatterCost && isMatterMatrixActive && !isMatterShatterActive)
            {
                var requiredPoints = matterShatterCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMatterShatterActive)
            {
                ShowOkButton();
            }
        }
    }
    public void Button5Press()
    {
        CloseOkButton();
        CloseCloseButton();
        CloseSkipButton();
        HideTouchToLearnText();
        HideUpgradeUnlockedText();
        CloseHighlightedInformation();

        if (isScorch)
        {
            isShieldGeneratorHighlighted = true;
            highlightedUpgradeDescriptionText.text = shieldGeneratorDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = shieldGeneratorName;
            highlightedUpgradeTitleText.color = shieldGeneratorTextColor;
            if (availablePoints >= shieldGeneratorCost && isArmorPlatingActive && !isShieldGeneratorActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + shieldGeneratorCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(!isArmorPlatingActive)
            {
                upgradeTextPromptText.text = armorPlatingColorString + armorPlatingNameText.text + whiteColorString + skillRequiredString; 
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < shieldGeneratorCost && !isShieldGeneratorActive && isArmorPlatingActive)
            {
                var requiredPoints = shieldGeneratorCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isShieldGeneratorActive)
            {
                ShowOkButton();
            }
        }
        if (isBroadshot)
        {
            isMultiShotHighlighted = true;
            highlightedUpgradeDescriptionText.text = multiShotDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = multiShotName;
            highlightedUpgradeTitleText.color = multiShotTextColor;
            if (availablePoints >= multiShotCost && !isMultiShotActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + multiShotCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            
            else if (availablePoints < multiShotCost && !isMultiShotActive)
            {
                var requiredPoints = multiShotCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMultiShotActive)
            {
                ShowOkButton();
            }
        }
        if(isTheVisitor)
        {
            isOrbpocolypseHighlighted = true;
            highlightedUpgradeDescriptionText.text = orbpocolypseDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = orbpocolypseName;
            highlightedUpgradeTitleText.color = orbpocolypseTextColor;
            if (availablePoints >= orbpocolypseCost && isBlastReturnActive && !isOrbpocolypseActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + orbpocolypseCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isBlastReturnActive)
            {
                upgradeTextPromptText.text = blastReturnColorString + blastReturnNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(availablePoints < orbpocolypseCost && isBlastReturnActive && !isOrbpocolypseActive)
            {
                var requiredPoints = orbpocolypseCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isOrbpocolypseActive)
            {
                ShowOkButton();
            }
        }
        if(isQuadranis)
        {
            isTwinBladesHighlighted = true;
            highlightedUpgradeDescriptionText.text = twinBladeDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = twinBladeName;
            highlightedUpgradeTitleText.color = twinBladesTextColor;
            if (availablePoints >= twinBladesCost && isBladeMatrixActive && !isTwinBladesActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + twinBladesCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isBladeMatrixActive)
            {
                upgradeTextPromptText.text = bladeMatrixColorString + bladeMatrixNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < twinBladesCost && isBladeMatrixActive && !isTwinBladesActive)
            {
                var requiredPoints = heatAmplifierIICost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isTwinBladesActive)
            {
                ShowOkButton();
            }
        }
        if (isDecimator)
        {
            isMachMissileHighlighted = true;
            highlightedUpgradeDescriptionText.text = machMissileDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = machMissileName;
            highlightedUpgradeTitleText.color = machMissileTextColor;
            if (availablePoints >= machMissileCost && !isMachMissileActive && !isTorpedoActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + machMissileCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints >= machMissileCost && !isMachMissileActive && isTorpedoActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = willReplaceString + torpedoColorString + torpedoName + whiteColorString + periodString + unlockForString + machMissileCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < machMissileCost && !isMachMissileActive)
            {
                var requiredPoints = machMissileCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isMachMissileActive)
            {
                ShowOkButton();
            }
        }
        if (isPrototypeX)
        {
            isAftershockHighlighted = true;
            highlightedUpgradeDescriptionText.text = aftershockDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = aftershockName;
            highlightedUpgradeTitleText.color = aftershockTextColor;
            if (availablePoints >= aftershockCost && isMatterMatrixActive && !isAftershockActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + aftershockCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isMatterMatrixActive)
            {
                upgradeTextPromptText.text = matterMatrixColorString + matterMatrixNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < aftershockCost && isMatterMatrixActive && !isAftershockActive)
            {
                var requiredPoints = aftershockCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isAftershockActive)
            {
                ShowOkButton();
            }
        }
    }
    public void Button6Press()
    {
        CloseOkButton();
        CloseCloseButton();
        CloseSkipButton();
        HideTouchToLearnText();
        HideUpgradeUnlockedText();
        CloseHighlightedInformation();

        if (isScorch)
        {
            isExtraArmamentsHighlighted = true;
            highlightedUpgradeDescriptionText.text = extraArmamentsDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = extraArmamentsName;
            highlightedUpgradeTitleText.color = extraArmamentsColor;
            if (availablePoints >= extraArmamentsCost && !isExtraArmamentsActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + extraArmamentsCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < extraArmamentsCost && !isExtraArmamentsActive)
            {
                var requiredPoints = extraArmamentsCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isExtraArmamentsActive)
            {
                ShowOkButton();
            }
        }
        if(isBroadshot)
        {
            isHeavyDiscountHighlighted = true;
            highlightedUpgradeDescriptionText.text = heavyDiscountDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = heavyDiscountName;
            highlightedUpgradeTitleText.color = heavyDiscountTextColor;
            if (availablePoints >= heavyDiscountCost && !isHeavyDiscountActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + heavyDiscountCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(availablePoints < heavyDiscountCost && !isHeavyDiscountActive)
            {
                var requiredPoints = heavyDiscountCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isHeavyDiscountActive)
            {
                ShowOkButton();
            }
        }
        if(isTheVisitor)
        {
            isDeathDanceHighlighted = true;
            highlightedUpgradeDescriptionText.text = deathDanceDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = deathDanceName;
            highlightedUpgradeTitleText.color = deathDanceTextColor;
            if (availablePoints >= deathDanceCost && !isDeathDanceActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + deathDanceCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < deathDanceCost && !isDeathDanceActive)
            {
                var requiredPoints = deathDanceCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if(isDeathDanceActive)
            {
                ShowOkButton();
            }
        }
        if(isQuadranis)
        {
            isEnergyCoilHighlighted = true;
            highlightedUpgradeDescriptionText.text = energyCoilDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = energyCoilName;
            highlightedUpgradeTitleText.color = energyCoilTextColor;
            if (availablePoints >= energyCoilCost && isShieldRageActive && !isEnergyCoilActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + energyCoilCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (!isShieldRageActive)
            {
                upgradeTextPromptText.text = shieldRageColorString + shieldRageNameText.text + whiteColorString + skillRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (availablePoints < energyCoilCost && isShieldRageActive && !isEnergyCoilActive)
            {
                var requiredPoints = energyCoilCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isEnergyCoilActive)
            {
                ShowOkButton();
            }
        }
        if (isDecimator)
        {
            isTorpedoHighlighted = true;
            highlightedUpgradeDescriptionText.text = torpedoDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = torpedoName;
            highlightedUpgradeTitleText.color = torpedoTextColor;
            if (availablePoints >= torpedoCost && !isTorpedoActive && !isMachMissileActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + torpedoCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if(availablePoints >= torpedoCost && !isTorpedoActive && isMachMissileActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = willReplaceString + machMissileColorString + machMissileName + whiteColorString + periodString + unlockForString + torpedoCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < torpedoCost && !isTorpedoActive)
            {
                var requiredPoints = torpedoCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isTorpedoActive)
            {
                ShowOkButton();
            }
        }
        if (isPrototypeX)
        {
            isParticleAcceleratorHighlighted = true;
            highlightedUpgradeDescriptionText.text = particleAcceleratorDesc;
            highlightedUpgradeDescriptionText.color = whiteColor;
            highlightedUpgradeTitleText.text = particleAcceleratorName;
            highlightedUpgradeTitleText.color = particleAcceleratorTextColor;
            if (availablePoints >= particleAcceleratorCost && !isParticleAcceleratorActive)
            {
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                skipButton.SetActive(false);
                upgradeTextPromptText.text = unlockForString + particleAcceleratorCost + pointsQuestionString;
                upgradeTextPromptText.color = whiteColor;
            }
            else if (availablePoints < particleAcceleratorCost && !isParticleAcceleratorActive)
            {
                var requiredPoints = particleAcceleratorCost - availablePoints;
                upgradeTextPromptText.text = requiredPoints.ToString() + morePointsRequiredString;
                upgradeTextPromptText.color = whiteColor;
                CloseConfirmAndCancelButtons();
                ShowOkButton();
            }
            else if (isParticleAcceleratorActive)
            {
                ShowOkButton();
            }
        }
    }

    // Scorch Scripts
    private void ShowScorchUpgrades()
    {
        ShowScorchLines();
        ShowScorchReqText();
        
        heatAmplifierINameText.text = heatAmplifierIName;
        heatAmplifierINameText.color = heatAmplifierITextColor;
        heatAmplifierICostText.text = costString + heatAmplifierICost.ToString() + pointString;

        heatAmplifierIINameText.text = heatAmplifierIIName;
        heatAmplifierIINameText.color = heatAmplifierIITextColor;
        heatAmplifierIICostText.text = costString + heatAmplifierIICost.ToString() + pointsString;

        armorPlatingNameText.text = armorPlatingName;
        armorPlatingNameText.color = armorPlatingTextColor;
        armorPlatingCostText.text = costString + armorPlatingCost.ToString() + pointsString;

        flamethrowerNameText.text = flamethrowerName;
        flamethrowerNameText.color = flamethrowerTextColor;
        flamethrowerCostText.text = costString + flamethrowerCost.ToString() + pointsString;

        shieldGeneratorNameText.text = shieldGeneratorName;
        shieldGeneratorNameText.color = shieldGeneratorTextColor;
        shieldGeneratorCostText.text = costString + shieldGeneratorCost.ToString() + pointsString;

        extraArmamentsNameText.text = extraArmamentsName;
        extraArmamentsNameText.color = extraArmamentsColor;
        extraArmamentsCostText.text = costString + extraArmamentsCost.ToString() + pointsString;

        if (isHeatAmplifierIActive)
        {
            heatAmplifierIButtonImage.sprite = selectedSprite;
        }
        if(isHeatAmplifierIIActive)
        {
            heatAmplifierIIButtonImage.sprite = selectedSprite;
        }
        if(isArmorPlatingActive)
        {
            armorPlatingButtonImage.sprite = selectedSprite;
        }
        if(isFlamethrowerActive)
        {
            flamethrowerButtonImage.sprite = selectedSprite;
        }
        if(isShieldGeneratorActive)
        {
            shieldGeneratorButtonImage.sprite = selectedSprite;
        }
        if(isExtraArmamentsActive)
        {
            extraArmamentsButtonImage.sprite = selectedSprite;
        }
    }
    private void ShowScorchLines()
    {
        foreach (GameObject line in scorchLines)
        {
            line.SetActive(true);
        }
    }
    private void ShowScorchReqText()
    {
        foreach (GameObject reqText in scorchReqText)
        {
            reqText.SetActive(true);
            reqText.GetComponent<TextMeshProUGUI>().color = whiteColor;
        }
    }
    private void ResetAllScorchHighlightedBools()
    {
        isHeatAmplifierIHighlighted = false;
        isHeatAmplifierIIHighlighted = false;
        isArmorPlatingHighlighted = false;
        isFlamethrowerHighlighted = false;
        isShieldGeneratorHighlighted = false;
        isExtraArmamentsHighlighted = false;
    }
    // BROADSHOT SCRIPTS
    private void ShowBroadshotUpgrades()
    {
        ShowBroadshotLines();
        ShowBroadshotReqText();

        focusFireNameText.text = focusFireName;
        focusFireNameText.color = focusFireTextColor;
        focusFireCostText.text = costString + focusFireCost.ToString() + pointString;

        mineEnhancerNameText.text = mineEnhancerName;
        mineEnhancerNameText.color = mineEnhancerTextColor;
        mineEnhancerCostText.text = costString + mineEnhancerCost.ToString() + pointsString;

        wideFireNameText.text = wideFireName;
        wideFireNameText.color = wideFireTextColor;
        wideFireCostText.text = costString + wideFireCost.ToString() + pointsString;

        minefieldNameText.text = minefieldName;
        minefieldNameText.color = minefieldTextColor;
        minefieldCostText.text = costString + minefieldCost.ToString() + pointsString;

        multiShotNameText.text = multiShotName;
        multiShotNameText.color = multiShotTextColor;
        multiShotCostText.text = costString + multiShotCost.ToString() + pointsString;

        heavyDiscountNameText.text = heavyDiscountName;
        heavyDiscountNameText.color = heavyDiscountTextColor;
        heavyDiscountCostText.text = costString + heavyDiscountCost.ToString() + pointsString;

        if (isFocusFireActive)
        {
            focusFireButtonImage.sprite = selectedSprite;
        }
        if (isMineEnhancerActive)
        {
            mineEnhancerButtonImage.sprite = selectedSprite;
        }
        if (isMultiShotActive)
        {
            multiShotButtonImage.sprite = selectedSprite;
        }
        if (isHeavyDiscountActive)
        {
            heavyDiscountButtonImage.sprite = selectedSprite;
        }
        if (isMineEnhancerActive)
        {
            mineEnhancerButtonImage.sprite = selectedSprite;
        }
        if (isMinefieldActive)
        {
            minefieldButtonImage.sprite = selectedSprite;
        }
    }

    private void ShowBroadshotLines()
    {
        foreach (GameObject line in broadshotLines)
        {
            line.SetActive(true);
        }
    }
    private void ShowBroadshotReqText()
    {
        foreach (GameObject reqText in broadshotReqText)
        {
            reqText.SetActive(true);
            reqText.GetComponent<TextMeshProUGUI>().color = whiteColor;
        }
    }
    private void ResetAllBroadshotHighlightedBools()
    {
        isFocusFireHighlighted = false;
        isMineEnhancerHighlighted = false;
        isWideFireHighlighted = false;
        isMinefieldHighlighted = false;
        isMultiShotHighlighted = false;
        isHeavyDiscountHighlighted = false;
    }
    // THE VISITOR SCRIPTS

    private void ShowTheVisitorUpgrades()
    {
        ShowTheVisitorLines();
        ShowTheVisitorReqText();

        blastReturnNameText.text = blastReturnName;
        blastReturnNameText.color = blastReturnTextColor;
        blastReturnCostText.text = costString + blastReturnCost.ToString() + pointsString;

        magneticResonatorNameText.text = magneticResonatorName;
        magneticResonatorNameText.color = magneticResonatorTextColor;
        magneticResonatorCostText.text = costString + magneticResonatorCost.ToString() + pointsString;

        healBeamNameText.text = healBeamName;
        healBeamNameText.color = healBeamTextColor;
        healBeamCostText.text = costString + healBeamCost.ToString() + pointsString;

        ultraBeamNameText.text = ultraBeamName;
        ultraBeamNameText.color = ultraBeamTextColor;
        ultraBeamCostText.text = costString + ultraBeamCost.ToString() + pointsString;

        orbpocolypsNameText.text = orbpocolypseName;
        orbpocolypsNameText.color = orbpocolypseTextColor;
        orbpocolypseCostText.text = costString + orbpocolypseCost.ToString() + pointsString;

        deathDanceNameText.text = deathDanceName;
        deathDanceNameText.color = deathDanceTextColor;
        deathDanceCostText.text = costString + deathDanceCost.ToString() + pointsString;

        if (isBlastReturnActive)
        {
            blastReturnButtonImage.sprite = selectedSprite;
        }
        if(isMagneticResonatorActive)
        {
            magneticResonatorButtonImage.sprite = selectedSprite;
        }
        if(isHealBeamActive)
        {
            healBeamButtonImage.sprite = selectedSprite;
        }
        if(isUltraBeamActive)
        {
            ultraBeamButtonImage.sprite = selectedSprite;
        }
        if(isOrbpocolypseActive)
        {
            orbpocolypseButtonImage.sprite = selectedSprite;
        }
        if(isDeathDanceActive)
        {
            deathDanceButtonImage.sprite = selectedSprite;
        }
    }
    private void ShowTheVisitorLines()
    {
        foreach(GameObject line in theVisitorLines)
        {
            line.SetActive(true);
        }
    }
    private void ShowTheVisitorReqText()
    {
        foreach (GameObject reqText in theVisitorReqText)
        {
            reqText.SetActive(true);
            reqText.GetComponent<TextMeshProUGUI>().color = whiteColor;
        }
    }
    private void ResetAllTheVisitorHighlightedBools()
    {
        isBlastReturnHighlighted = false;
        isMagneticResonatorHighlighted = false;
        isHealBeamHighlighted = false;
        isUltraBeamHighlighted = false;
        isOrbpocolypseHighlighted = false;
        isDeathDanceHighlighted = false;
    }
    // QUADRANIS SCRIPTS

    private void ShowQuadranisUpgrades()
    {
        ShowQuadranisLines();
        ShowQuadranisReqText();

        resupplyNameText.text = resupplyName;
        resupplyNameText.color = resupplyTextColor;
        resupplyCostText.text = costString + resupplyCost.ToString() + pointString;

        missileVolleyNameText.text = missileVolleyName;
        missileVolleyNameText.color = missileVolleyTextColor;
        missileVolleyCostText.text = costString + missileVolleyCost.ToString() + pointString;

        bladeMatrixNameText.text = bladeMatrixName;
        bladeMatrixNameText.color = bladeMatrixTextColor;
        bladeMatrixCostText.text = costString + bladeMatrixCost.ToString() + pointString;

        shieldRageNameText.text = shieldRageName;
        shieldRageNameText.color = shieldRageTextColor;
        shieldRageCostText.text = costString + shieldRageCost.ToString() + pointString;

        twinBladesNameText.text = twinBladeName;
        twinBladesNameText.color = twinBladesTextColor;
        twinBladesCostText.text = costString + twinBladesCost.ToString() + pointString;

        energyCoilNameText.text = energyCoilName;
        energyCoilNameText.color = energyCoilTextColor;
        energyCoilCostText.text = costString + energyCoilCost.ToString() + pointString;

        if (isResupplyActive)
        {
            resupplyButtonImage.sprite = selectedSprite;
        }
        if(isMissileVolleyActive)
        {
            missileVolleyButtonImage.sprite = selectedSprite;
        }
        if(isBladeMatrixActive)
        {
            bladeMatrixButtonImage.sprite = selectedSprite;
        }
        if(isShieldRageActive)
        {
            shieldRageButtonImage.sprite = selectedSprite;
        }
        if(isTwinBladesActive)
        {
            twinBladesButtonImage.sprite = selectedSprite;
        }
        if(isEnergyCoilActive)
        {
            energyCoilButtonImage.sprite = selectedSprite;
        }
    }
    private void ShowQuadranisLines()
    {
        foreach (GameObject line in quadranisLines)
        {
            line.SetActive(true);
        }
    }
    private void ShowQuadranisReqText()
    {
        foreach (GameObject reqText in quadranisReqText)
        {
            reqText.SetActive(true);
            reqText.GetComponent<TextMeshProUGUI>().color = whiteColor;
        }
    }
    private void ResetAllQuadranisHighlightedBools()
    {
        isResupplyHighlighted = false;
        isMissileVolleyHighlighted = false;
        isBladeMatrixHighlighted = false;
        isShieldRageHighlighted = false;
        isTwinBladesHighlighted = false;
        isEnergyCoilHighlighted = false;
    }

    // DECIMATOR SCRIPTS

    private void ShowDecimatorUpgrades()
    {
        ShowDecimatorLines();
        ShowDecimatorReqText();

        turboShotNameText.text = turboShotName;
        turboShotNameText.color = turboShotTextColor;
        turboShotCostText.text = costString + turboShotCost.ToString() + pointString;

        powerShotNameText.text = powerShotName;
        powerShotNameText.color = powerShotTextColor;
        powerShotCostText.text = costString + powerShotCost.ToString() + pointString;

        attackShieldNameText.text = attackShieldName;
        attackShieldNameText.color = attackShieldTextColor;
        attackShieldCostText.text = costString + attackShieldCost.ToString() + pointsString;

        disruptorCannonNameText.text = disruptorCannonName;
        disruptorCannonNameText.color = disruptorCannonTextColor;
        disruptorCannonCostText.text = costString + disruptorCannonCost.ToString() + pointsString;

        machMissileNameText.text = machMissileName;
        machMissileNameText.color = machMissileTextColor;
        machMissileCostText.text = costString + machMissileCost.ToString() + pointsString;

        torpedoNameText.text = torpedoName;
        torpedoNameText.color = torpedoTextColor;
        torpedoCostText.text = costString + torpedoCost.ToString() + pointsString;

        if(isTurboShotActive)
        {
            turboShotButtonImage.sprite = selectedSprite;
        }
        if(isPowerShotActive)
        {
            powerShotButtonImage.sprite = selectedSprite;
        }
        if(isAttackShieldActive)
        {
            attackShieldButtonImage.sprite = selectedSprite;
        }
        if(isDisruptorCannonActive)
        {
            disruptorCannonButtonImage.sprite = selectedSprite;
        }    
        if(isMachMissileActive)
        {
            machMissileButtonImage.sprite = selectedSprite;
        }
        if (isTorpedoActive)
        {
            torpedoButtonImage.sprite = selectedSprite;
        }
    }
    private void ShowDecimatorLines()
    {
        foreach(GameObject line in decimatorLines)
        {
            line.SetActive(true);
        }
    }
    private void ShowDecimatorReqText()
    {
        foreach(GameObject reqText in decimatorReqText)
        {
            reqText.gameObject.SetActive(true);
            reqText.GetComponent<TextMeshProUGUI>().color = whiteColor;
        }
    }
    private void ResetAllDecimatorHighlightedBools()
    {
        isTurboShotHighlighted = false;
        isPowerShotHighlighted = false;
        isAttackShieldHighlighted = false;
        isDisruptorCannonHighlighted = false;
        isMachMissileHighlighted = false;
        isTorpedoHighlighted = false;
    }

    // PROTOTYPE X SCRIPTS

    private void ShowPrototypeXUpgrades()
    {
        ShowPrototypeXLines();
        ShowPrototypeXReqText();

        assaultDroneINameText.text = assaultDroneIName;
        assaultDroneINameText.color = assaultDroneITextColor;
        assaultDroneICostText.text = costString + assaultDroneICost.ToString() + pointString;

        assaultDroneIINameText.text = assaultDroneIIName;
        assaultDroneIINameText.color = assaultDroneIITextColor;
        assaultDroneIICostText.text = costString + assaultDroneIICost.ToString() + pointString;

        matterMatrixNameText.text = matterMatrixName;
        matterMatrixNameText.color = matterMatrixTextColor;
        matterMatrixCostText.text = costString + matterMatrixCost.ToString() + pointsString;

        matterShatterNameText.text = matterShatterName;
        matterShatterNameText.color = matterShatterTextColor;
        matterShatterCostText.text = costString + matterShatterCost.ToString() + pointsString;

        aftershockNameText.text = aftershockName;
        aftershockNameText.color = aftershockTextColor;
        aftershockCostText.text = costString + aftershockCost.ToString() + pointsString;

        particleAcceleratorNameText.text = particleAcceleratorName;
        particleAcceleratorNameText.color = particleAcceleratorTextColor;
        particleAcceleratorCostText.text = costString + particleAcceleratorCost.ToString() + pointsString;

        if (isAssaultDroneIActive)
        {
            assaultDroneIButtonImage.sprite = selectedSprite;
        }
        if (isAssaultDroneIIActive)
        {
            assaultDroneIIButtonImage.sprite = selectedSprite;
        }
        if (isMatterMatrixActive)
        {
            matterMatrixButtonImage.sprite = selectedSprite;
        }
        if (isMatterShatterActive)
        {
            matterShatterButtonImage.sprite = selectedSprite;
        }
        if (isAftershockActive)
        {
            aftershockButtonImage.sprite = selectedSprite;
        }
        if (isParticleAcceleratorActive)
        {
            particleAcceleratorButtonImage.sprite = selectedSprite;
        }
    }
    private void ShowPrototypeXLines()
    {
        foreach (GameObject line in prototypeXLines)
        {
            line.SetActive(true);
        }
    }
    private void ShowPrototypeXReqText()
    {
        foreach (GameObject reqText in prototypeXReqText)
        {
            reqText.gameObject.SetActive(true);
            reqText.GetComponent<TextMeshProUGUI>().color = whiteColor;
        }
    }
    private void ResetAllPrototypeXHighlightedBools()
    {
        isAssaultDroneIHighlighted = false;
        isAssaultDroneIIHighlighted = false;
        isMatterMatrixHighlighted = false;
        isMatterShatterHighlighted = false;
        isAftershockHighlighted = false;
        isParticleAcceleratorHighlighted = false;
    }
}
