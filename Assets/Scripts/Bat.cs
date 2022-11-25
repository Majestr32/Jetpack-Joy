using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour, IEnemy
{
    [SerializeField, Range(0,100)]
    private float _damageRange;
    [SerializeField]
    private float _lifeTime = 35;
    [SerializeField]
    private float _speed = 2;
    private GameObject _hero;
    public void TrackHero()
    {
        Vector3 targetDirection = (_hero.transform.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = targetRotation;
    }
    public void SeekHero()
    {
        var foundObjects = Physics.OverlapSphere(transform.position, _damageRange);
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
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if(_hero == null)
        {
            SeekHero();
        }
        else
        {
            Debug.Log("Damaged hero");
            Die();
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
        Gizmos.DrawWireSphere(transform.position, _damageRange);
    }
}
