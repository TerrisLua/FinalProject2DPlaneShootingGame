using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject endText;
    public GameObject instructionsPanel;
    public GameObject mainMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        if (endText != null) endText.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (levelCompletedPanel != null) levelCompletedPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void InstructionsPanel()
    {
        instructionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public IEnumerator LevelComplete()
    {
        pauseButton.SetActive(false);
        yield return new WaitForSeconds(2f);
        endText.SetActive(true);
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
        levelCompletedPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
     
    }

    public void DelayGameOver()
    {
        StartCoroutine(GameOverWithDelay());
    }

    private IEnumerator GameOverWithDelay()
    {
        yield return new WaitForSeconds(1f);
        GameOver();
    }
}
