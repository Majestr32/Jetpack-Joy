using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int _xFixedPosition = 1;
    private int _yFixedPosition = 1;

    public static MapGenerator Instance { get; private set; }

    [SerializeField]
    private GameObject _grassPf;
    private List<GameObject> _pool;
    private int _grassPoolCount = 5;
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
        for(int i = 0; i < _grassPoolCount; i++)
        {
            grassObj = Instantiate(_grassPf);
            grassObj.SetActive(false);
            _pool.Add(grassObj);
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
        for(int i = 0; i < _grassPoolCount; i++)
        {
            if (!_pool[i].activeInHierarchy)
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
