using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected Action<PlayerMovement> PlayerEntered;
    
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

        PlayerEntered?.Invoke(other.GetComponent<PlayerMovement>());

        Tween.LocalScale(transform, Vector3.zero, 0.3f, 0, disappearAnimCurve, completeCallback: () => Destroy(gameObject));
    }
    
    private void Update()
    {
        transform.Rotate(0,  turnSpeed * Time.deltaTime,0);
    }
}
