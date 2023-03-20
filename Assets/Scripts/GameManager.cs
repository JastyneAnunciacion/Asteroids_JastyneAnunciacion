using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onGameOver;

    public bool GameStarted => _gameStarted;
    public bool PlayerCanMove => _playerCanMove;

    private bool _gameStarted = false;
    private bool _playerCanMove = false;

    public void GivePlayerControl() {
        _playerCanMove = true;
    }

    public void StartGame() {
        onStart?.Invoke();
        _gameStarted = true;
    }

    public void GameOver() {
        onGameOver?.Invoke();
    }
    
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start() {
        _gameStarted = false;
        _playerCanMove = false;
    }
}
