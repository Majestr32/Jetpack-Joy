using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    private float _horizontal = 0;

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(_horizontal,0,1) * Time.deltaTime * _speed);
    }
}
