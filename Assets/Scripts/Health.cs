using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;
    
    private float _max = 100f;
    private float _min = 0f;
    
    public event UnityAction Changed;
    
    public void TakeDamage(int damage)
    {
        _value -= damage;
        
        if (_value <= _min)
        {
            Die();
        }
        
        Changed?.Invoke();
    }

    public void TakeCure(float value)
    {
        _value += value;
        
        if (_value > _max)
        {
            _value = _max;
        }
        
        Changed?.Invoke();
    }
    
    public float GetNormalized() 
    {
        return _value / _max;
    }

    private void Die()
    {
        gameObject.SetActive(false);    
    }
}
