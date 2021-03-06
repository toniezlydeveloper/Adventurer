﻿using UnityEngine;

public class AnimatorCommunicator
{
    private readonly Rigidbody2D _rb2d;
    private readonly Attacker _attacker;
    private readonly Animator _animator;
    private readonly PlayerMover _playerMover;
    private readonly CollisionChecker _collisionChecker;
        
    private static readonly int IsGroundedId = Animator.StringToHash("IsGrounded");
    private static readonly int VerticalSpeedId = Animator.StringToHash("VerticalSpeed");
    private static readonly int HorizontalSpeedId = Animator.StringToHash("HorizontalSpeed");
    private static readonly int HasDoubleJumped = Animator.StringToHash("HasDoubleJumped");
    private static readonly int CurrentAttackId = Animator.StringToHash("CurrentAttack");

    public AnimatorCommunicator(Rigidbody2D rb2d, Animator animator, CollisionChecker collisionChecker, PlayerMover playerMover, Attacker attacker)
    {
        _rb2d = rb2d;
        _animator = animator;
        _attacker = attacker;
        _playerMover = playerMover;
        _collisionChecker = collisionChecker;
    }

    public void UpdateAnimator()
    {
        _animator.SetBool(IsGroundedId, _collisionChecker.IsCollidingWithGround);       
        _animator.SetFloat(HorizontalSpeedId, Mathf.Abs(_rb2d.velocity.x));
        _animator.SetBool(HasDoubleJumped, _playerMover.HasDoubleJumped);
        _animator.SetInteger(CurrentAttackId, _attacker.DesiredAttackCount);
        _animator.SetFloat(VerticalSpeedId, _rb2d.velocity.y);
    }
}