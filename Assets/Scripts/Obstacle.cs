using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour
{

    PlayerMovement _playerMovement;
    [SerializeField]
    private GameObject[] possibleMeshes;

    [SerializeField]
    private Vector3 spawnDelta;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        Instantiate(possibleMeshes[Random.Range(0, possibleMeshes.Length - 1)], transform.position+spawnDelta,Quaternion.identity,transform);
        
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