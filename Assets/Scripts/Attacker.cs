using UnityEngine;

public class Attacker : IAnimationEndListener
{
    public AnimationId ListenedAnimationId => AnimationId.Attack;

    private float _currentCooldown;
    private int _currentAttackCount;

    private readonly float _attackCooldown;

    private const int MaxComboCount = 3;
    
    public int DesiredAttackCount { get; private set; }

    public Attacker(float attackCooldown)
    {
        _attackCooldown = attackCooldown;
    }

    public void HandleListenedAnimationEnd()
    {
        if (--_currentAttackCount > 0)
        {
            return;
        }

        DesiredAttackCount = 0;
        _currentCooldown = _attackCooldown;
    }
    
    public void TryAttacking(bool attackInput)
    {
        if (_currentCooldown >= 0f)
        {
            _currentCooldown -= Time.deltaTime;
            return;
        }
        
        if (!attackInput)
        {
            return;
        }
        
        if (DesiredAttackCount < MaxComboCount)
        {
            DesiredAttackCount++;
        }
        else if (_currentAttackCount == 0)
        {
            DesiredAttackCount = 1;
        }

        _currentAttackCount = DesiredAttackCount;
    }
}