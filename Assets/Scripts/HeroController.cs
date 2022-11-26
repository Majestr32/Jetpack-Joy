using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    private float _horizontal = 0;
    [SerializeField]
    private Vector3 _attackPoint = Vector3.forward;
    [SerializeField, Range(0,10)]
    private float _attackRadius;
    private Animator _anim;
    [SerializeField]
    private int _hp = 3;
    [SerializeField]
    private GameObject _heartsContainer;
    [SerializeField]
    private Image _heartImage;

    private bool _canReceiveDamage = true;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        MaybeAttack();
    }
    private void Move()
    {
        _horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(_horizontal, 0, 1) * Time.deltaTime * _speed);
    }

    public void ReceiveDamage()
    {
        if (_canReceiveDamage)
        {
            LoseHp();
            StartCoroutine(BecomeInvincible());
        }
    }

    public void LoseHp(bool allHp = false)
    {
        if(allHp || _hp == 1)
        {
            SceneManagment.Instance.GoToGameOver();
        }
        _hp--;
        Destroy(_heartsContainer.transform.GetChild(0).gameObject);
    }
    public void HealHp()
    {
        if (_hp == 3)
        {
            return;
        }
        _hp++;
        Instantiate(_heartImage.gameObject, _heartsContainer.transform);
    }

    IEnumerator BecomeInvincible()
    {
        _canReceiveDamage = false;
        yield return new WaitForSeconds(1f);
        _canReceiveDamage = true;
    }

    private void MaybeAttack()
    {
        var foundObjects = Physics.OverlapSphere(transform.TransformPoint(_attackPoint), _attackRadius);
        foreach(var obj in foundObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                _anim.SetTrigger("Attack");
                obj.GetComponent<IEnemy>().ReceiveHitFromPlayer();
            }else if (obj.CompareTag("Destroyable"))
            {
                _anim.SetTrigger("Attack");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.TransformPoint(_attackPoint), _attackRadius);
    }
}
