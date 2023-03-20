using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scores {
    public class ScoreManager : Singleton<ScoreManager> {
        [SerializeField] private TextMeshProUGUI scoreText;
        [HideInInspector] public int PlayerScore { get; private set; }

        public void Start() {
            PlayerScore = 0;
        }

        public void AddScore(int addedScore) {
            PlayerScore += addedScore;
            scoreText.text = PlayerScore.ToString();
        }
    }
}

