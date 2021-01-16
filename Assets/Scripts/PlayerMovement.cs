using System;
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
    
    public Action PlayerDied;

    private AudioSource _source;
    private ParticleSpawner _particleSpawner;

    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip death;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _speed = GameSettings.Instance.gameSettings.horizontalSpeed;
        _jumpForce = GameSettings.Instance.gameSettings.jumpForce;
        _source = GetComponent<AudioSource>();
        _particleSpawner = GetComponentInChildren<ParticleSpawner>();
    }

    private void FixedUpdate()
    {
        if (!_alive) return;

        var horizontalMove = transform.right * (_horizontalInput * _speed * Time.fixedDeltaTime);
        horizontalMove += rb.position;
        horizontalMove.x = Mathf.Clamp(horizontalMove.x, -5, 5);
        rb.MovePosition(horizontalMove);
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        var doJump = Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down,0.1f,~8);
        if (doJump)
        {
            rb.AddForce(Vector3.up*_jumpForce,ForceMode.VelocityChange);
            _animator.SetTrigger(Jump);
            _source.PlayOneShot(jump);
            _particleSpawner.DoSpawn(false,1);
        }

        _animator.SetFloat(RunSpeed, _speed/5);
    }

    public void Die()
    {
        _alive = false; 
        _animator.SetTrigger(Fall);
        _source.PlayOneShot(death);
        _particleSpawner.DoSpawn(false);
        PlayerDied?.Invoke();
    }


}