using UnityEngine;

[RequireComponent(typeof(AnimationEndObserver))]
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

    private Attacker _attacker;
    private PlayerMover _playerMover;
    private JumpModifier _jumpModifier;
    private CollisionChecker _collisionChecker;
    private AnimatorCommunicator _animatorCommunicator;
        
    private void Awake()
    {
        _attacker = new Attacker(1f);
        _jumpModifier = new JumpModifier(rb2d, 2f, 2.5f);
        _collisionChecker = new CollisionChecker(wallCheckPoints, groundCheckPoint);
        _playerMover = new PlayerMover(rb2d, moveSpeed, transform, jumpPower, _collisionChecker);
        _animatorCommunicator = new AnimatorCommunicator(rb2d, animator, _collisionChecker, _playerMover, _attacker);
    }

    private void OnEnable()
    {
        GetComponent<AnimationEndObserver>().Subscribe(_attacker);
    }

    private void OnDisable()
    {
        GetComponent<AnimationEndObserver>().Unsubscribe(_attacker);
    }

    private void Update()
    {
        bool attackInput = Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X); 
        bool jumpInput = Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
            
        _playerMover.TryJumping(jumpInput);
        _attacker.TryAttacking(attackInput);
        _jumpModifier.ModifyJump(jumpInput);
        _playerMover.MoveHorizontally(horizontalInput);
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