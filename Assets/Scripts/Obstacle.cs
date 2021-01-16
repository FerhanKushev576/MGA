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
        if (!collision.gameObject.CompareTag("Player")) return;
        
        if (!collision.gameObject.GetComponent<PlayerMovement>().isInvulnerable)
        {
            // Kill the player
            _playerMovement.Die();
        }
        else
        {
            GetComponent<ParticleSpawner>().DoSpawn(true);
            GetComponent<AudioSource>().Play();
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<Renderer>().enabled = false;
        }
    }
}