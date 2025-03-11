using UnityEngine;

[RequireComponent ( typeof ( EnemyMover ) )]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Melee _melee;
    [SerializeField] private float _attackCooldown = 1f; 
    
    private EnemyMover _mover;
    private float _nextAttackTime;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }
    
    private void FixedUpdate()
    {
        _mover.Move();
    }

    private void OnTriggerStay2D (Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && Time.time >= _nextAttackTime)
        {
            _melee.Attack();
            
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }
}
