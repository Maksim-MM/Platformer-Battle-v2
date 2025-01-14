using UnityEngine;

[RequireComponent ( typeof ( Animator ) )]
public class View : MonoBehaviour
{
    private const string Speed = nameof(Speed);
    private const string OnGround = nameof(OnGround);
    private const string AttackTrigger = nameof(AttackTrigger);
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetSpeed(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    public void SetOnGround(bool onGround)
    {
        _animator.SetBool(OnGround, onGround);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(AttackTrigger);
    }
}
