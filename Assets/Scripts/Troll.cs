using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour, IEnemy
{
    [SerializeField, Range(1,100)]
    private float _visionRange;
    [SerializeField]
    private float _lifeTime = 35;
    [SerializeField]
    private float _knockdownScaleWidth = 50f;
    [SerializeField]
    private float _knockdownScaleHeight = 10f;
    private bool _canHit = true;
    private Rigidbody _rb;
    private GameObject _hero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
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
            MaybeAttack();
        }
    }
    private void MaybeAttack()
    {
        if(_hero.transform.position.z > transform.position.z)
        {
            DealDamage();
            Die();
        }
    }
    public void DealDamage()
    {
        if (_canHit)
        {
            _rb.AddForce(transform.TransformDirection(Vector3.forward) * _knockdownScaleWidth + Vector3.up * 2f, ForceMode.Impulse);
            _hero.GetComponent<HeroController>().ReceiveDamage();
        }
    }

    public void Die()
    {
        Destroy(gameObject, 0.2f);
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRange);
    }

    public void ReceiveHitFromPlayer()
    {
        _rb.AddForce(transform.TransformDirection(Vector3.back) * _knockdownScaleWidth + Vector3.up * _knockdownScaleHeight, ForceMode.Impulse);
        _canHit = false;
        Die();
    }
}
