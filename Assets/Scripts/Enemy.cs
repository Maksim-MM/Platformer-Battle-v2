using UnityEngine;

[RequireComponent ( typeof ( EnemyMover ) )]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Melee _melee;
    
    private EnemyMover _mover;

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
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            _melee.Attack();
        }
    }
}
