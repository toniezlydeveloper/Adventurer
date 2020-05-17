using UnityEngine;

public class JumpModifier
{
    private readonly Rigidbody2D _rb2d;
    private readonly float _fallMultiplier;
    private readonly float _lowJumpMultiplier;

    public JumpModifier(Rigidbody2D rb2d, float lowJumpMultiplier, float fallMultiplier)
    {
        _rb2d = rb2d;
        _fallMultiplier = fallMultiplier;
        _lowJumpMultiplier = lowJumpMultiplier;
    }

    public void ModifyJump(bool jumpInput)
    {
        if (_rb2d.velocity.y < 0f)
        {
            _rb2d.velocity += Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1f) * Time.deltaTime);
        }
        else if (_rb2d.velocity.y > 0f && jumpInput)
        {
            _rb2d.velocity += Vector2.up * (Physics2D.gravity.y * (_lowJumpMultiplier - 1f) * Time.deltaTime);
        }
    }
}