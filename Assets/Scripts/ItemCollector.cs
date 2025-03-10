using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect(GetComponent<Player>());
        }
    }
}
