﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    bool _alive = true;
    private Animator _animator;
    [SerializeField] Rigidbody rb;
    private float _speed = 5;
    private float _jumpForce;

    float _horizontalInput;
    
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int RunSpeed = Animator.StringToHash("RunSpeed");

    public delegate void PlayerDiedDelegate();

    public event PlayerDiedDelegate PlayerDied;

    private void FixedUpdate()
    {
        if (!_alive) return;

        Vector3 horizontalMove = transform.right * (_horizontalInput * _speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + horizontalMove);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _speed = GameSettings.Instance.gameSettings.horizontalSpeed;
        _jumpForce = GameSettings.Instance.gameSettings.jumpForce;
    }
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        var doJump = Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down,0.1f,~8);
        if (doJump)
        {
            rb.AddForce(Vector3.up*_jumpForce,ForceMode.VelocityChange);
            _animator.SetTrigger(Jump);
        }

        _animator.SetFloat(RunSpeed, _speed/5);
    }

    public void Die()
    {
        _alive = false; 
        _animator.SetTrigger(Fall);
        PlayerDied?.Invoke();
    }

    public void IncreaseDifficulty(float speedDelta)
    {
        _speed += speedDelta;
    }

    
}