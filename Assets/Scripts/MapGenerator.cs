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
    private List<PoolObject> _poolPrefabs;
    private List<GameObject> _pool;
    private int _poolCount = 5;
    private int _zFirstFragmentOffset = 8;
    private int _zFragments = 5;
    private int _zSpawnGap = 14;
    private int _nextZSpawnPosition { get => _zFirstFragmentOffset + _zFragments * _zSpawnGap; }
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
    }
    void InstaniatePool()
    {
        _pool = new List<GameObject>();
        GameObject grassObj;
        for(int j = 0; j < _poolPrefabs.Count; j++)
        {
            for (int i = 0; i < _poolCount; i++)
            {
                grassObj = Instantiate(_poolPrefabs[j].gameObject);
                grassObj.SetActive(false);
                _pool.Add(grassObj);
            }
        }
    }

    public void GenerateNewFragment()
    {
        GameObject obj = GetPooledObject();
        PlaceNewObjectToFront(obj);
    }

    public void HideFragment(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    GameObject GetPooledObject()
    {
        int objTypeToChoose = Random.Range(0, _poolPrefabs.Count);
        string poolTagToChoose = _poolPrefabs[objTypeToChoose].poolTag;
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
            obj.transform.position = new Vector3(_xFixedPosition,_yFixedPosition,_nextZSpawnPosition);
            obj.SetActive(true);
            _zFragments++;
        }
    }

    void Update()
    {
        
    }
}
