using UnityEngine;

public class Obstacle : MonoBehaviour
{

    PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Kill the player
            _playerMovement.Die();
        }
    }
}