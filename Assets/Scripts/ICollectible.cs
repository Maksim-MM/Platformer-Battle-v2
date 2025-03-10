using System;

public interface ICollectible
{
    event Action ItemCollected;
    
    public void Collect(Player player);
}