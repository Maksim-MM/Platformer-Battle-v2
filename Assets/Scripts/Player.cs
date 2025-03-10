using System.Collections;
using UnityEngine;

[RequireComponent ( typeof ( Mover ) )]
[RequireComponent ( typeof ( View ) )]
[RequireComponent ( typeof ( Health ) )]
public class Player : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";
    
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private Melee _melee;
    [SerializeField] private float _groundCheckInterval = 0.1f;

    private Mover _mover;
    private View _view;
    private Health _health;
    
    private bool _isTouchingGround;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _view = GetComponent<View>();
        _health = GetComponent<Health>();
    }
    
    private void Start()
    {
        StartCoroutine(CheckGroundedRoutine());
    }
    
    private void Update()
    {
        float direction = Input.GetAxis(Horizontal);
        
        HandleMovement(direction);
        UpdateAnimation(direction);
        HandleJump();
        HandleAttack();
    }
    
    private IEnumerator CheckGroundedRoutine()
    {
        while (true)
        {
            _isTouchingGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayerMask);
            
            yield return new WaitForSeconds(_groundCheckInterval);
        }
    }
    
    public void TakeHeal(int healValue)
    {
        _health.TakeCure(healValue);
    }
    
    private void HandleMovement(float direction)
    {
        _mover.Move(direction);
        _mover.Rotate(direction);
    }
    
    private void UpdateAnimation(float direction)
    {
        _view.SetSpeed(Mathf.Abs(direction));
        _view.SetOnGround(_isTouchingGround);
    }

    private void HandleJump()
    {
        if (Input.GetAxis(Jump) > 0f && _isTouchingGround)
        {
            _mover.JumpUp();
            _view.SetOnGround(false);
        }
    }
    
    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _melee.Attack();
            _view.SetAttackTrigger();
        }
    }
}
