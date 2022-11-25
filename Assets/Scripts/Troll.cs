using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour, IEnemy
{
    [SerializeField, Range(1,100)]
    private float _visionRange;
    [SerializeField]
    private float _lifeTime = 35;
    private GameObject _hero;
    public void TrackHero()
    {
        Vector3 targetDirection = (_hero.transform.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = targetRotation;
    }
    public void SeekHero()
    {
        var foundObjects = Physics.OverlapSphere(transform.position, _visionRange);
        foreach(var obj in foundObjects)
        {
            if (obj.CompareTag("Player"))
            {
                _hero = obj.gameObject;
            }
        }
    }

    private void Update()
    {
        if(_hero == null)
        {
            SeekHero();
        }
        else
        {
            TrackHero();
        }
    }
    public void DealDamage()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRange);
    }

}
