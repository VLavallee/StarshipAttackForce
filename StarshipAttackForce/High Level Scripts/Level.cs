using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    [SerializeField] bool gameWasJustUnpaused = false;
    [SerializeField] GameObject pauseButtonInScoreCanvas;
    [SerializeField] GameObject pauseScreenCanvas;
    [SerializeField] GameObject[] inGameOptionsMenu;
    [SerializeField] bool optionsOpen;
    [SerializeField] GameObject gameSavedUIElement;
    [SerializeField] GameObject soundOptionsUIElement;
    [SerializeField] GameObject whatTheFuckIsHappening;
    [SerializeField] float time;
    [SerializeField] float timeLimit = 0.1f;

    

    
    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void Pause()
    {
        if(!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            FindObjectOfType<PlayerMovement>().isPaused = true;
            pauseButtonInScoreCanvas.SetActive(false);
            pauseScreenCanvas.SetActive(true);
            
            return;
        }
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            gameWasJustUnpaused = true;
            pauseButtonInScoreCanvas.SetActive(true);
            pauseScreenCanvas.SetActive(false);
            CloseOptionsInGame();
            return;
        }
    }

    public void OptionsButton()
    {
        if(!optionsOpen)
        {
            OpenOptionsInGame();
        }
        else if(optionsOpen)
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
        //add game save here
        gameSavedUIElement.SetActive(true);
    }

    public void OpenSoundOptionsMenuAndClosePauseMenu()
    {
        pauseScreenCanvas.SetActive(false);
        soundOptionsUIElement.SetActive(true);
    }

    public void SaveSoundOptionsAndOpenPauseMenu()
    {
        soundOptionsUIElement.SetActive(false);
        pauseScreenCanvas.SetActive(true);
    }

    //public void GameIsInSession()
    //{
    //    FindObjectOfType<GameSession>().GameIsInSession();
    //}
}
