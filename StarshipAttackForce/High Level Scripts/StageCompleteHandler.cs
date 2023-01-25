using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageCompleteHandler : MonoBehaviour
{
    [SerializeField] BossHealth bossHealth;
    [SerializeField] GameObject stageCompleteUICanvas;
    [SerializeField] bool stageIsComplete = false;
    public bool bossFound = false;
    [SerializeField] float flyAwaySpeed = 6;

    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject menuButton, optionsButton, continueButton, buttonBG;
    [SerializeField] GameObject shipHangerButton;
    [SerializeField] GameObject stageCompleteBoxParent;
    [SerializeField] GameObject starmapUICanvas;
    public string continueButtonSetToSelectStage, continueButtonSetToContinue, continueButtonSetToPlay;
    [SerializeField] TextMeshProUGUI continueButtonText;

    private void Update()
    {
        if (bossFound && bossHealth == null && !stageIsComplete)
        {
            Debug.Log("Stage Complete coroutine started!");
            StartCoroutine(ShowStageCompleteUICanvas());
            stageIsComplete = true;
        }
    }

    public void FindBoss()
    {
        bossHealth = FindObjectOfType<BossHealth>();
        if(bossHealth != null)
        {
            Debug.Log("Boss Found!");
            bossFound = true;
            //FindObjectOfType<BossHealth>().bossHasBeenConnectedToStageCompleteHandler = true;
        }
    }
    private void ActivateButtons()
    {
        menuButton.SetActive(true);
        optionsButton.SetActive(true);
        continueButton.SetActive(true);
        buttonBG.SetActive(true);
    }

    private void DeactivateButtons()
    {
        menuButton.SetActive(false);
        optionsButton.SetActive(false);
        continueButton.SetActive(false);
        buttonBG.SetActive(false);
        DeactivateShipHangerButton();
    }

    public void ActivateShipHangerButton()
    {
        shipHangerButton.SetActive(true);
    }

    private void DeactivateShipHangerButton()
    {
        shipHangerButton.SetActive(false);
    }

    public void SetContinueButtonToSelectStage()
    {
        continueButtonText.text = continueButtonSetToSelectStage;
    }
    public void SetContinueButtonToPlayStageButton()
    {
        continueButtonText.text = continueButtonSetToPlay;
    }

    public void ActivateStarmapUICanvas()
    {
        stageCompleteBoxParent.SetActive(false);
        starmapUICanvas.SetActive(true);
    }

    IEnumerator ShowStageCompleteUICanvas()
    {
        yield return new WaitForSeconds(.5f);
        stageCompleteUICanvas.SetActive(true);
        FindObjectOfType<Player>().CanShoot("false");
        FindObjectOfType<PlayerMovement>().controlIsActive = false;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<Player>().GetComponent<Animator>().SetTrigger("FastFlight");
        FindObjectOfType<Player>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyAwaySpeed);
        FindObjectOfType<ScoreCanvasOffSwitch>().TurnOffScoreCanvas();
        yield return new WaitForSeconds(3f);
        ActivateButtons();
    }
}
