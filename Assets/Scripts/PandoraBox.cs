using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandoraBox : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetTrigger("Crack");
            float randomNumber = Random.Range(0, 100);
            if(randomNumber < 20)
            {
                return;
            }else if(randomNumber < 50)
            {
                other.GetComponent<HeroController>().HealHp();
            }
            else
            {
                for(int i = 0; i < 10; i++)
                {
                    GameManager.Instance.pickCoin();
                }
            }
        }
    }
}
