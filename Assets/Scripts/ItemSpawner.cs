using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnDelay = 2f;

    private readonly Dictionary<Transform, GameObject> _spawnedItems = new();
    private readonly Queue<GameObject> _itemPool = new();

    private void Start()
    {
        SpawnInitialItems();
    }
    
    private IEnumerator SpawnItemWithDelay(Transform spawnPoint)
    {
        yield return new WaitForSeconds(_spawnDelay);
        
        SpawnItemAt(spawnPoint);
    }

    private void SpawnInitialItems()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            SpawnItemAt(spawnPoint);
        }
    }

    private void SpawnItemAt(Transform spawnPoint)
    {
        GameObject newItem = GetPooledItem() ?? SpawnNewItem();
        
        newItem.transform.position = spawnPoint.position;
        newItem.SetActive(true);
        _spawnedItems[spawnPoint] = newItem;
        
        SubscribeToItemEvents(newItem, spawnPoint);
    }

    private GameObject GetPooledItem()
    {
        GameObject item;
    
        if (_itemPool.TryDequeue(out item))
        {
            return item;
        }

        return null;
    }

    private GameObject SpawnNewItem()
    {
        GameObject itemPrefab = _itemPrefabs[Random.Range(0, _itemPrefabs.Length)];
        
        return Instantiate(itemPrefab);
    }

    private void SubscribeToItemEvents(GameObject item, Transform spawnPoint)
    {
        if (!item.TryGetComponent(out ICollectible collectible))
        {
            return;
        }

        collectible.ItemCollected += () => CollectItem(collectible, item, spawnPoint);
    }

    private void CollectItem(ICollectible collectible, GameObject item, Transform spawnPoint)
    {
        collectible.ItemCollected -= () => CollectItem(collectible, item, spawnPoint);
        
        item.SetActive(false);
        _itemPool.Enqueue(item);
        
        StartSpawnCoroutine(spawnPoint);
    }

    private void StartSpawnCoroutine(Transform spawnPoint)
    {
        StartCoroutine(SpawnItemWithDelay(spawnPoint));
    }
}
