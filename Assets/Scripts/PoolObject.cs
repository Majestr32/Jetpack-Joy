using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField]
    private string _poolTag;
    public string poolTag { get => _poolTag; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MapGenerator.Instance.GenerateNewFragment();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MapGenerator.Instance.HideFragment(this.gameObject);
        }
    }
}
