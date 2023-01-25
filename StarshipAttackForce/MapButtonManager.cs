using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapButtonManager : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI stageSelectInfoText;
    //[SerializeField] string stageLockedText;
    //[SerializeField] string newStageUnlockedText;
    //[TextArea(3, 10)] [SerializeField] string stageOneInfoText;
    //[TextArea(3, 10)] [SerializeField] string stageTwoInfoText;
    //[SerializeField] string defaultStageSelectText;
    //[SerializeField] Animator mapAnimator;
    //[SerializeField] Animator stage2LockedButtonAnimator;

    //[Header("State variables")]
    //[SerializeField] int currentStage;
    //[SerializeField] int stage_01, stage_02, stage_03, stage_04;
    //[SerializeField] bool hasContinueButtonBeenPressedOnStageComplete;
    
    //public void Stage_01_Button()
    //{
    //    StopAllCoroutines();
    //    ShowStage1ButtonIsActive();
    //    stageSelectInfoText.text = stageOneInfoText;
    //}

    //public void Stage_02_Button()
    //{
    //    if(FindObjectOfType<StageManager>().GetStage1CompletionStatus())
    //    {
    //        StopAllCoroutines();
    //        ShowStage2ButtonIsActive();
    //        stageSelectInfoText.text = stageTwoInfoText;
    //    }
    //    else
    //    {
    //        LockedButton();
    //    }
    //}

    //public void SetMapToShowNoStageInfo()
    //{
    //    mapAnimator.SetTrigger("NoStage");
    //}
    //public void ShowStage1ButtonIsActive()
    //{
    //    mapAnimator.SetTrigger("Stage1");
    //}
    //public void ShowStage2ButtonIsActive()
    //{
    //    mapAnimator.SetTrigger("Stage2");
    //}

    //public void LockedButton()
    //{
    //    StopAllCoroutines();
    //    SetMapToShowNoStageInfo();
    //    LockButtonPressedAnim();
    //    StartCoroutine(LockedButtonPress());
    //}

    //private void LockButtonPressedAnim()
    //{
    //    if (stage2LockedButtonAnimator != null)
    //    {
    //        stage2LockedButtonAnimator.SetTrigger("LockedButtonPressed");
    //    }
    //    if (stage2LockedButtonAnimator == null)
    //    {
    //        Debug.LogError("Animator not set on " + gameObject.name);
    //    }
    //}

    //public void UnlockNextStage()
    //{
    //    if(!hasContinueButtonBeenPressedOnStageComplete)
    //    {
    //        StartCoroutine(UnlockSequence());
    //        hasContinueButtonBeenPressedOnStageComplete = true;
    //    }
        
    //}

    //IEnumerator UnlockSequence()
    //{
    //    FindObjectOfType<StageCompleteHandler>().SetContinueButtonToSelectStage();
    //    stageSelectInfoText.text = newStageUnlockedText;
    //    yield return new WaitForSeconds(1f);
    //    stage2LockedButtonAnimator.SetTrigger("LockedButtonUnlockSequence");
    //    yield return new WaitForSeconds(1.5f);
    //    stageSelectInfoText.text = stageTwoInfoText;
    //    ShowStage2ButtonIsActive();
    //    FindObjectOfType<StageCompleteHandler>().ActivateShipHangerButton();
    //    FindObjectOfType<StageCompleteHandler>().SetContinueButtonToPlayStageButton();
    //}

    //private void LockButtonIdle()
    //{
    //    GetComponent<Animator>().SetTrigger("LockedButtonIdle");
    //}

    //IEnumerator LockedButtonPress()
    //{
    //    stageSelectInfoText.text = stageLockedText;
    //    yield return new WaitForSeconds(2);
    //    stageSelectInfoText.text = defaultStageSelectText;
    //}
}
