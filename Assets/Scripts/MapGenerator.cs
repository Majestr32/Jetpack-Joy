using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int _xFixedPosition = 1;
    private int _yFixedPosition = 1;

    public static MapGenerator Instance { get; private set; }

    [SerializeField]
    private List<GameObject> _poolPrefabs;

    [SerializeField]
    private List<GameObject> _enemiesPrefabs;

    private List<GameObject> _pool;

    private float _poolCount = 10;
    private float _zFirstFragmentOffset = 8;
    private float _zFragments = 2;
    private float _zSpawnGap = 14;
    private float _nextZSpawnPosition { get => _zFirstFragmentOffset + _zFragments * _zSpawnGap; }
    private Vector3 _nextSpawnPositionForFloor { get => new Vector3(_xFixedPosition, _yFixedPosition, _nextZSpawnPosition); }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        InstaniatePool();
        GenerateFragment();
        GenerateFragment();
        GenerateFragment();
    }
    void InstaniatePool()
    {
        _pool = new List<GameObject>();
        GameObject obj;
        for(int j = 0; j < _poolPrefabs.Count; j++)
        {
            for (int i = 0; i < _poolCount; i++)
            {
                obj = Instantiate(_poolPrefabs[j].gameObject);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }
    }

    public void GenerateFragment()
    {
        GameObject floor = GetPooledObjectFloor();
        PlaceNewObjectToFront(floor);
        SpawnEnemyOnFloor(floor);
    }

    private void SpawnEnemyOnFloor(GameObject floor)
    {
        var randomEnemy = _enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Count)];
        var spawnOn = floor.GetComponent<SpawnArea>().randomSpawnPointGlobalPosition;
        Debug.Log(spawnOn);
        Instantiate(randomEnemy, spawnOn, new Quaternion(0,90,0,1f));
    }

    public void HideFragment(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    GameObject GetPooledObjectFloor()
    {
        int objTypeToChoose = Random.Range(0, _poolPrefabs.Count);
        string poolTagToChoose = _poolPrefabs[objTypeToChoose].GetComponent<PoolObject>().poolTag;
        for(int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy && _pool[i].GetComponent<PoolObject>().poolTag == poolTagToChoose)
            {
                return _pool[i];
            }
        }

        return null;
    }

    void PlaceNewObjectToFront(GameObject obj)
    {
        if(obj != null)
        {
            obj.transform.position = _nextSpawnPositionForFloor;
            obj.SetActive(true);
            _zFragments++;
        }
    }

    void Update()
    {
        
    }
}
