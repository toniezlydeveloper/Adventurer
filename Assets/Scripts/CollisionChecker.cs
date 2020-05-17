using System.Linq;
using UnityEngine;

public class CollisionChecker
{
    private readonly Transform _groundCheckPoint;
    private readonly Transform[] _wallCheckPoints;
    private readonly LayerMask _wallLayerMask = 1 << LayerMask.NameToLayer("Wall");
    private readonly LayerMask _groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        
    private const float CollisionCheckRadius = 0.05f;

    public bool IsCollidingWithWalls =>
        _wallCheckPoints.Any(
            wallCheckPoint =>
                Physics2D.OverlapCircle(wallCheckPoint.position, CollisionCheckRadius, _wallLayerMask));

    public bool IsCollidingWithGround =>
        Physics2D.OverlapCircle(_groundCheckPoint.position, CollisionCheckRadius, _groundLayerMask);
        
    public CollisionChecker(Transform[] wallCheckPoints, Transform groundCheckPoint)
    {
        _wallCheckPoints = wallCheckPoints;
        _groundCheckPoint = groundCheckPoint;
    }

    public void DebugGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheckPoint.position, CollisionCheckRadius);
            
        foreach (Transform wallCheckPoint in _wallCheckPoints)
        {
            Gizmos.DrawWireSphere(wallCheckPoint.position, CollisionCheckRadius);
        }
    }
}