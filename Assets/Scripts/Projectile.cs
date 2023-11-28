using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _damage;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Debug.Log("Launching projectile...");
        _rigidbody2D.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {

    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }
}
