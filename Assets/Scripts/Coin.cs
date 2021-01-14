using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;

public class Coin : MonoBehaviour
{

    [SerializeField] float turnSpeed = 90f;

    [SerializeField] private AnimationCurve disappearAnimCurve;

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
        FindObjectOfType<GameManager>().IncrementScore();
        GetComponent<AudioSource>()?.Play();

        Tween.LocalScale(transform, Vector3.zero, 0.3f, 0, disappearAnimCurve, completeCallback: () => Destroy(gameObject));
    }

    private void Update()
    {
        transform.Rotate(0,  turnSpeed * Time.deltaTime,0);
    }
}