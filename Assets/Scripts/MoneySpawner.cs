using System.Collections;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private Money _money;

    private Money _newMoney;
    private float _spawnDelay = 2f;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _newMoney = Instantiate(_money, transform.position , Quaternion.identity);
    }
    
    private void OnEnable()
    {
        _newMoney.MoneyCollected += StartSpawnMoney;
    }
    
    private void OnDisable()
    {
        _newMoney.MoneyCollected -= StartSpawnMoney;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnDelay);
    }
    
    private IEnumerator SpawnMoney()
    {
        yield return _wait;
        _newMoney.gameObject.SetActive(true);
    }
    
    private void StartSpawnMoney()
    {
        StartCoroutine(SpawnMoney());
    }
}
