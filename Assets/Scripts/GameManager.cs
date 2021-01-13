using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int _score;
    PlayerMovement _playerMovement;
    [SerializeField] Text scoreText;
    
    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        // Should probably remove if we want a game over screen, and trigger it through the UI. Would be cleaner
        _playerMovement.PlayerDied += () => Invoke("Restart",2);
    }

    public void IncrementScore()
    {
        _score++;
        scoreText.text = "SCORE: " + _score;
        // Increase the player's speed
        _playerMovement.IncreaseDifficulty(GameSettings.Instance.gameSettings.speedIncreasePerPoint);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
