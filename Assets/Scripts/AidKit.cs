using System;
using UnityEngine;

public class AidKit : MonoBehaviour, ICollectible
{
    [SerializeField] private int _healValue = 10;
    
    public event Action ItemCollected;
    
    public void Collect(Player player)
    {
        player.TakeHeal(_healValue);
        
        ItemCollected?.Invoke();
    }
}
