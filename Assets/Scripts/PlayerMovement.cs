using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    bool _alive = true;
    private Animator _animator;
    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float _horizontalInput;
    private static readonly int Fall = Animator.StringToHash("Fall");

    public delegate void PlayerDiedDelegate();

    public event PlayerDiedDelegate PlayerDied;

    private void FixedUpdate()
    {
        if (!_alive) return;

        Vector3 horizontalMove = transform.right * (_horizontalInput * speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + horizontalMove);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _animator.SetFloat("RunSpeed", speed/5);
    }

    public void Die()
    {
        _alive = false; 
        _animator.SetTrigger(Fall);
        PlayerDied?.Invoke();
    }

    public void IncreaseDifficulty(float speedDelta)
    {
        speed += speedDelta;
    }

    
}