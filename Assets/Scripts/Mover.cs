using UnityEngine;

[RequireComponent ( typeof ( Rigidbody2D ) )]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpHeight;

    private Rigidbody2D _player;
    
    private Quaternion _turnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion _turnRight = Quaternion.identity;
    
    private void Awake()
    {
        _player = GetComponent<Rigidbody2D>();
    }
    
    public void Move(float direction)
    {
        float distance = direction * _moveSpeed * Time.deltaTime;
        
        switch (distance)
        {
            case > 0: 
                transform.Translate(distance * Vector2.right);
                break;
            case < 0: 
                transform.Translate(distance * Vector2.left);
                break;
        }
    }

    public void Rotate(float velocityX)
    {
        switch (velocityX)
        {
            case > 0:
                transform.localRotation = _turnRight;
                break;
            case < 0:
                transform.localRotation = _turnLeft;
                break;
        }
    }

    public void JumpUp()
    {
        _player.velocity = new Vector2(_player.velocity.x, _jumpHeight);
    }
}
