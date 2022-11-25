using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    List<Vector3> _spawnPoints;
    private Vector3 randomSpawnPointLocalPosition { get => _spawnPoints[Random.Range(0, _spawnPoints.Count)]; }
    public Vector3 randomSpawnPointGlobalPosition { get => transform.TransformPoint(randomSpawnPointLocalPosition); }

    private void OnDrawGizmosSelected()
    {
        foreach(var p in _spawnPoints)
        {
            Gizmos.DrawSphere(transform.TransformPoint(p), 0.2f);
        }
    }
}
