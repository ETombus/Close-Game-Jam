using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private int[] highscores = new int[5];
    private GameStateController gameState;
    private int currentScore;
    public TextMeshProUGUI scoreCounter;

    void Start()
    {
        gameState = GetComponent<GameStateController>();
        StartCoroutine(Score());
    }

    IEnumerator Score()
    {
        while(gameState.currentState != GameStateController.GameState.GameOver)
        {
            currentScore+=1;
            scoreCounter.text = "Score: " + currentScore.ToString("D4");
            yield return new WaitForSeconds(1f);
        }
    }

}
