using System;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private int _healValue = 10;
    
    public event Action AidKitCollected;

    private void OnDisable()
    {
        AidKitCollected?.Invoke();
    }

    public int GetHealValue()
    {
        return _healValue;
    }
}
