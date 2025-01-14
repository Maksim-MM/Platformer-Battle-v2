using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _attackRadius;
    [SerializeField] private int _damage;
    [SerializeField] private float _pushForce;
    
    private Collider2D[] _colliders;

    public void Attack()
    {
        _colliders = Physics2D.OverlapCircleAll(transform.position, _attackRadius, _targetMask);

       if (_colliders.Length == 0)
        {
            return;
        }
       
        foreach (var enemy in _colliders)
        {
            if (enemy.TryGetComponent(out Health health) && enemy.TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                health.TakeDamage(_damage);
                
                Vector2 pushDirection = (enemy.transform.position - transform.position).normalized;
                
                rigidbody2D.AddForce( pushDirection * _pushForce , ForceMode2D.Impulse);
            }
        }
    }
}
