using System.Collections;
using UnityEngine;

public class AidKitSpawner : MonoBehaviour
{
    [SerializeField] private  AidKit _aidKit;

    private AidKit _newAidKit;
    private float _spawnDelay = 2f;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _newAidKit = Instantiate(_aidKit, transform.position , Quaternion.identity);
    }
    
    private void OnEnable()
    {
        _newAidKit.AidKitCollected += StartSpawnMoney;
    }
    
    private void OnDisable()
    {
        _newAidKit.AidKitCollected -= StartSpawnMoney;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnDelay);
    }
    
    private IEnumerator SpawnMoney()
    {
        yield return _wait;
        _newAidKit.gameObject.SetActive(true);
    }
    
    private void StartSpawnMoney()
    {
        StartCoroutine(SpawnMoney());
    }
}
