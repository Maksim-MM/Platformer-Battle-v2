using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ChaseArea : MonoBehaviour
{
    public event Action <Vector2> PlayerEntered;

    private Vector2 _playerPosition;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            _playerPosition = player.transform.position;
            PlayerEntered?.Invoke(_playerPosition);
        }
    }
}
