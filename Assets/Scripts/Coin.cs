using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{

    [SerializeField] float turnSpeed = 90f;

    [SerializeField] private AnimationCurve disappearAnimCurve;

    private int _worth = 1;

    private void Awake()
    {
        if (Random.Range(0f, 1f) <= GameSettings.Instance.gameSettings.chanceToBeGolden)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
            _worth = GameSettings.Instance.gameSettings.worthOfGolden;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {   
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with is the player
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Add to the player's score
        FindObjectOfType<GameManager>().IncrementScore(_worth);
        GetComponent<AudioSource>()?.Play();

        Tween.LocalScale(transform, Vector3.zero, 0.3f, 0, disappearAnimCurve, completeCallback: () => Destroy(gameObject));
    }

    private void Update()
    {
        transform.Rotate(0,  turnSpeed * Time.deltaTime,0);
    }
}