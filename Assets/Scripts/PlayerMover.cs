using UnityEngine;

public class PlayerMover
{
    private readonly float _jumpPower;
    private readonly float _moveSpeed;
    private readonly Rigidbody2D _rb2d;
    private readonly Transform _transform;
    private readonly CollisionChecker _collisionChecker;

    private const float SingleJumpPowerMultiplier = 1f;
    private const float DoubleJumpPowerMultiplier = 0.75f;
        
    public bool HasDoubleJumped { get; private set; }

    public PlayerMover(Rigidbody2D rb2d, float moveSpeed, Transform transform, float jumpPower, CollisionChecker collisionChecker)
    {
        _rb2d = rb2d;
        _moveSpeed = moveSpeed;
        _transform = transform;
        _jumpPower = jumpPower;
        _collisionChecker = collisionChecker;
    }

    public void MoveHorizontally(float horizontalInput)
    {
        _rb2d.velocity = new Vector2(_moveSpeed * horizontalInput, _rb2d.velocity.y);

        if (horizontalInput == 0f)
        {
            return;
        }
            
        Vector3 currentScale = _transform.localScale;
        float xScale = Mathf.Abs(currentScale.x);
        _transform.localScale = new Vector3(horizontalInput > 0f ? xScale: -xScale, currentScale.y,
            currentScale.z);
    }

    public void TryJumping(bool jumpInput)
    {
        if (!jumpInput)
        {
            return;
        }
            
        if (_collisionChecker.IsCollidingWithGround)
        {
            HasDoubleJumped = false;
            Jump(SingleJumpPowerMultiplier);
        }
        else if (!HasDoubleJumped && _rb2d.velocity.y > 0f)
        {
            HasDoubleJumped = true;
            Jump(DoubleJumpPowerMultiplier);
        }
    }

    private void Jump(float jumpPowerModifier)
    {
        float jumpPower = _jumpPower * jumpPowerModifier;
        _rb2d.velocity *= new Vector2(1f, 0f);
        _rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                
        if (_rb2d.velocity.y > jumpPower)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpPower);
        }
    }
}