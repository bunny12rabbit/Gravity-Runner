using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject] private GameConfig _config;
    [Inject] private GameController _gameController;

    [SerializeField] LayerMask _mask;

    private Rigidbody2D _myRb;
    private Collider2D _myCol;
    private Transform _gCheck;
    private bool _grounded;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _speed = 8;
    private float _jumpForce = 30;

    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int Speed = Animator.StringToHash("Speed");

    public bool IsMoveNow { get; set; }
    public bool IsDead { get; private set; }
    public bool IsUp { get; private set; }

    private void Start()
    {
        _myRb = GetComponent<Rigidbody2D>();
        _gCheck = transform.Find("groundCheck");
        _myCol = GetComponent<Collider2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = _animator.transform.GetComponent<SpriteRenderer>();

        if (_config)
        {
            _speed = _config.speed;
            _jumpForce = _config.jumpForce;
        }
    }

    private void Update()
    {
        _grounded = Physics2D.Linecast(transform.position, _gCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        _animator.SetBool(IsJumping, !_grounded);

        if (IsDead)
        {
            _gameController.GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (IsMoveNow)
        {
            Move();
            _animator.SetFloat(Speed, Mathf.Abs(1));
        }
    }

    public void Move()
    {
        _myRb.velocity = new Vector2(1 * _speed, _myRb.velocity.y);
    }

    public void Up()
    {
        if (!_grounded) return;
        if (_gameController.IsJumpActivated)
        {
            _myRb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            return;
        }
        _myRb.gravityScale *= -1;
        transform.Rotate(new Vector3(0, 0, 180));
        IsUp = !IsUp;
        _jumpForce *= -1;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    public void Die()
    {
        IsDead = true;
        Destroy(gameObject, 3f);
    }
}
