using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Transform[] wallCheckPoints;

    private PlayerMover _playerMover;
    private JumpModifier _jumpModifier;
    private CollisionChecker _collisionChecker;
    private AnimatorCommunicator _animatorCommunicator;
        
    private void Awake()
    {
        _jumpModifier = new JumpModifier(rb2d, 2f, 2.5f);
        _collisionChecker = new CollisionChecker(wallCheckPoints, groundCheckPoint);
        _playerMover = new PlayerMover(rb2d, moveSpeed, transform, jumpPower, _collisionChecker);
        _animatorCommunicator = new AnimatorCommunicator(rb2d, animator, _collisionChecker, _playerMover);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bool jumpInput = Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z);
            
        _playerMover.MoveHorizontally(horizontalInput);
        _playerMover.TryJumping(jumpInput);
        _jumpModifier.ModifyJump(jumpInput);
    }

    private void LateUpdate()
    {
        _animatorCommunicator.UpdateAnimator();
    }

    private void OnDrawGizmos()
    {
        if (_collisionChecker == null)
        {
            _collisionChecker = new CollisionChecker(wallCheckPoints, groundCheckPoint);
        }
            
        _collisionChecker.DebugGizmos();
    }
}