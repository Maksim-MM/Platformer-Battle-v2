using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public event Action MoneyCollected;
    
    private void OnDisable()
    {
        MoneyCollected?.Invoke();
    }
}
