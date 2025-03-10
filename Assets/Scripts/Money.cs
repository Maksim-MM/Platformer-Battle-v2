using System;
using UnityEngine;

public class Money : MonoBehaviour, ICollectible
{
    public event Action ItemCollected;
    
    public void Collect(Player player)
    {
        ItemCollected?.Invoke(); 
    }
}
