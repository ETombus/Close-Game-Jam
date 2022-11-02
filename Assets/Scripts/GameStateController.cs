using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance;

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public enum GameState
    {
        GamePaused,
        GameOver,
        Playing
    }

    public GameState currentState;

    private void Start()
    {
        if (GameStateController.Instance == null) Instance = this;
        else Destroy(gameObject);

        ChangeGameState(GameState.Playing);

    }

    public void ChangeGameState(GameState newState)
    {
        DiableAllPanels();

        if (newState == GameState.Playing)
        {
            // Return to game

            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            currentState = GameState.Playing;
        }
        else if (newState == GameState.GamePaused)
        {
            // Pause Game (Only if playing)
            if (currentState != GameState.GameOver)
            {

                currentState = GameState.GamePaused;
                pausePanel.SetActive(true);
                StopTimescale();
            }
        }
        else if (newState == GameState.GameOver)
        {
            gameOverPanel.SetActive(true);
            currentState = GameState.GameOver;
            StopTimescale();
            // Dead as hell
        }

    }

    void StopTimescale()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }
    void DiableAllPanels()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    public void TogglePause()
    {
        if (currentState != GameState.GamePaused)
            ChangeGameState(GameState.GamePaused);
        else
            ChangeGameState(GameState.Playing);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

}
