using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scores;

public class GameOverManager : Singleton<GameOverManager> {
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;


    public void GameOver() {
        gameOverPanel.SetActive(true);
        var playerScore = ScoreManager.instance.PlayerScore;
        var highScore = PlayerPrefs.GetFloat("HIGHSCORE");
        if (highScore < playerScore) {
            PlayerPrefs.SetFloat("HIGHSCORE", playerScore);
            scoreText.gameObject.SetActive(true);
            scoreText.text = "NEW HIGHSCORE: " + playerScore;
        }
        else {
            scoreText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
            scoreText.text = "SCORE: " + playerScore;
            highScoreText.text = "HIGHSCORE: " + highScore;
        }
    }

}
