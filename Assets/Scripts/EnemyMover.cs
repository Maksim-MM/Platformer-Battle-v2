using UnityEngine;

[RequireComponent(typeof(ChaseArea))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private ChaseArea _chaseArea;
    
    private int _currentWaypoint = 0;

    private Quaternion _turnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion _turnRight = Quaternion.identity;
    private bool _isChase = false;
    private Vector2 _targetPosition;
    
    private void OnEnable()
    {
        if (_chaseArea != null)
        {
            _chaseArea.PlayerEntered += SetTarget;
        }
    }

    private void OnDisable()
    {
        if (_chaseArea != null)
        {
            _chaseArea.PlayerEntered -= SetTarget;
        }
    }

    public void Move()
    {
        if (_isChase == false)
        {
            PatrolArea();
        }
        else
        {
            ChaseTarget(_targetPosition);
        }
    }

    private void Rotate(Vector2 direction)
    {
        float velocityX = direction.x;
        transform.localRotation = velocityX > 0 ? _turnLeft : _turnRight;
    }

    private void SetTarget(Vector2 position)
    {
        _isChase = true;
        _targetPosition = position;
    }

    private void PatrolArea()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint = (++_currentWaypoint) % _waypoints.Length;
            Rotate(_waypoints[_currentWaypoint].position - transform.position);
        }

        transform.position = Vector2.MoveTowards(
            transform.position, _waypoints[_currentWaypoint].position, 
            _speed * Time.deltaTime);
    }

    private void ChaseTarget(Vector2 position)
    {
        Vector2 targetPosition = new Vector2(position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 
            _speed * Time.deltaTime);

        Rotate((targetPosition - (Vector2)transform.position).normalized);

        if (transform.position.x == position.x)
        {
            _isChase = false;
        }
    }
}
