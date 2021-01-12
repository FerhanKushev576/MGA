using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int _score;
    PlayerMovement _playerMovement;
    [SerializeField] private float speedIncreasePerPoint = 0.1f;
    public float tileSpeed;
    [SerializeField] Text scoreText;
    
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    #endregion

    void Awake()
    {
        #region  Singleton
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(gameObject);
        }
        #endregion
        
        _playerMovement = FindObjectOfType<PlayerMovement>();
        // Should probably remove if we want a game over screen, and trigger it through the UI. Would be cleaner
        _playerMovement.PlayerDied += () => Invoke("Restart",2);
    }
    
    public void IncrementScore()
    {
        _score++;
        scoreText.text = "SCORE: " + _score;
        // Increase the player's speed
        _playerMovement.IncreaseDifficulty(speedIncreasePerPoint);
    }
    
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
