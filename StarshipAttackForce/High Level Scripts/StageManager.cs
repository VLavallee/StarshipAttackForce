using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class StageManager : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] GameObject pauseButtonInScoreCanvas;
    [SerializeField] GameObject pauseScreenCanvas;
    [SerializeField] GameObject[] inGameOptionsMenu;
    [SerializeField] bool optionsOpen;
    [SerializeField] GameObject gameSavedCanvas;
    [SerializeField] GameObject soundOptionsCanvas;
    //[SerializeField] Image blackRestartLevelScreen;
    //public static bool isBlackRestartLevelScreenCompleted;
    [SerializeField] List<GameObject> scoreCanvasItemsToDisable;

    // title screen
    [SerializeField] float timeSinceTouched, timeSinceTouchedLimit;
    [SerializeField] bool screenTouched = false;
    [SerializeField] float timeToWait;
    [SerializeField] bool shouldCountDownToEnableTouch;
    [SerializeField] bool canBeTouched = false;
    [SerializeField] Animator touchToPlayTextAnimator;
    [SerializeField] Animator astralTitleAnimator;
    [SerializeField] Animator SDF_mainAnimator, SDF_glowAnimator;
    [SerializeField] Animator sunAnimator;

    [SerializeField] bool shipIsFlyingAway;
    [SerializeField] float levelLoadTime, levelLoadTimeLimit;

    // mode
    [SerializeField] ModeSelect modeSelect;
    [SerializeField] bool isShipFlownAway;
    

    private void Update()
    {
        if(screenTouched)
        {
            timeSinceTouched += Time.deltaTime;
            if(timeSinceTouched > timeSinceTouchedLimit)
            {
                LoadMainMenu();
                screenTouched = false;
            }
        }
        if(shipIsFlyingAway)
        {
            levelLoadTime += Time.deltaTime;
            if(levelLoadTime > levelLoadTimeLimit && !isShipFlownAway)
            {
                isShipFlownAway = true;
                LoadArcadeGame();
                FindObjectOfType<GameSession>().SaveGame();
            }
        }
    }
    private void Start()
    {
        if(shouldCountDownToEnableTouch)
        {
            StartCoroutine(EnableTouchOnTimer());
        }
    }
    IEnumerator EnableTouchOnTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        canBeTouched = true;
        shouldCountDownToEnableTouch = false;
    }
    
    public void TouchScreen()
    {
        if(canBeTouched)
        {
            FindObjectOfType<TitleExitFadeOut>().FadeOut();
            touchToPlayTextAnimator.SetTrigger("TouchToPlayTouched");
            FindObjectOfType<SoundPlayer>().PlayStartSound();
            //astralTitleAnimator.SetTrigger("TitleFade");
            //sunAnimator.SetTrigger("SunFade");
            SDF_mainAnimator.SetTrigger("TitleFade");
            SDF_glowAnimator.SetTrigger("TitleFade");
            canBeTouched = false;
        }
    }
    
    
    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        Debug.Log("gameover triggered");
        FindObjectOfType<SoundPlayer>().PlaySoundEffect("gameOverSound");
        FindObjectOfType<GameOverText>().TurnOnGameOverText();
        //FindObjectOfType<GameSession>().SaveGame();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Game Over");
    }
    public void DisableGameOverScoreCanvasItems()
    {
        foreach(GameObject item in scoreCanvasItemsToDisable)
        {
            item.SetActive(false);
        }
    }
    

    public void LoadArcadeGame()
    {
        SceneManager.LoadScene("Arcade Level 2");
        FindObjectOfType<GameSession>().NewLevel();
        Time.timeScale = 1;
    }
    //public void RestartArcadeLevel()
    //{
    //    StartCoroutine(ReloadArcadeGame());
    //}
    //IEnumerator ReloadArcadeGame()
    //{
    //    blackRestartLevelScreen.enabled = true;
        
    //    yield return new WaitUntil(() => isBlackRestartLevelScreenCompleted);
    //    LoadArcadeGame();
    //}
    //public void SetBlackRestartLevelBool()
    //{
    //    isBlackRestartLevelScreenCompleted = true;
    //}
    public void LoadInfiniteGame()
    {
        SceneManager.LoadScene("Infinite Mode");
        Time.timeScale = 1;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
        isPaused = false;
    }
    public void StopAllPlayerSounds()
    {
        FindObjectOfType<SoundPlayer>().StopAllPlayerSounds();
    }

    public void LoadAsyncMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        Time.timeScale = 1;
        isPaused = false;
    }
    public void LoadAsyncArcadeLevel()
    {
        SceneManager.LoadSceneAsync("Arcade Level 2");
    }

    public void LoadAchievementsScreen()
    {
        SceneManager.LoadScene("Achievements");
        Time.timeScale = 1;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            FindObjectOfType<PlayerMovement>().isPaused = true;
            //pauseButtonInScoreCanvas.SetActive(false);
            pauseScreenCanvas.SetActive(true);

            return;
        }
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            FindObjectOfType<PauseControl>().gameWasJustUnpaused = true;
            //pauseButtonInScoreCanvas.SetActive(true);
            pauseScreenCanvas.SetActive(false);
            CloseOptionsInGame();
            return;
        }
    }
    public void DisablePauseButton()
    {

    }
    public bool GetPausedStatus()
    {
        return isPaused;
    }

    public void OptionsButton()
    {
        if (!optionsOpen)
        {
            OpenOptionsInGame();
        }
        else if (optionsOpen)
        {
            CloseOptionsInGame();
        }
    }

    public void OpenOptionsInGame()
    {
        foreach (GameObject objects in inGameOptionsMenu)
        {
            objects.SetActive(true);
        }
        optionsOpen = true;
    }

    public void CloseOptionsInGame()
    {
        foreach (GameObject objects in inGameOptionsMenu)
        {
            objects.SetActive(false);
        }
        optionsOpen = false;
    }

    public void SaveGameButton()
    {
        gameSavedCanvas.SetActive(true);
    }

    public void SaveGame()
    {
        FindObjectOfType<GameSession>().SaveGame();
    }

    public void OpenSoundOptionsMenuAndClosePauseMenu()
    {
        pauseScreenCanvas.SetActive(false);
        soundOptionsCanvas.SetActive(true);
    }

    public void SaveSoundOptionsAndOpenPauseMenu()
    {
        soundOptionsCanvas.SetActive(false);
        pauseScreenCanvas.SetActive(true);
    }

    public void GameIsInSession()
    {
        FindObjectOfType<GameSession>().GameIsInSession();
    }

    public void LoadSpaceport()
    {
        SceneManager.LoadScene("Spaceport");
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options");
        Time.timeScale = 1;
        isPaused = false;
    }
    public void TriggerUIShipFlyAwaySequence()
    {
        FindObjectOfType<SelectedShipMovement>().TriggerFlyAwaySequence();
        shipIsFlyingAway = true;
        if(FindObjectOfType<SoundPlayer>())
        {
            FindObjectOfType<SoundPlayer>().PlaySoundEffect("shipTakeOffSound");
        }
    }
}
