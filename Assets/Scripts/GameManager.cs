using Pixelplacement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int _score;
    PlayerMovement _playerMovement;
    [SerializeField] TMP_Text scoreText;

    private AnimationCurve scoreBreathAnimCurve;
    
    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        // Should probably remove if we want a game over screen, and trigger it through the UI. Would be cleaner
        _playerMovement.PlayerDied += () => Invoke("Restart",2);
        scoreBreathAnimCurve = new AnimationCurve()
        {
            keys = new[]
            {
                new Keyframe(0, 0),
                new Keyframe(0.5f, 1),
                new Keyframe(1, 0)
            }
        };
    }

    public void IncrementScore()
    {
        _score++;
        scoreText.text = _score.ToString();
        Tween.LocalScale(scoreText.transform, Vector3.one, scoreText.transform.localScale * 1.5f, 0.5f, 0, scoreBreathAnimCurve);
        // Increase the player's speed
        _playerMovement.IncreaseDifficulty(GameSettings.Instance.gameSettings.speedIncreasePerPoint);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
