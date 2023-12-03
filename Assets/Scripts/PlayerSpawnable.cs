﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSpawnable : MonoBehaviour
{
    [SerializeField]
    protected float _speed;

    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected float _destroyAfterSeconds = -1f;

    protected Rigidbody2D _rigidbody2D;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        Launch();
        StartDestroyCountdown();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        DestroySpawnable();
    }

    protected virtual void Launch()
    {
        throw new NotImplementedException();
    }

    private void StartDestroyCountdown()
    {
        if (_destroyAfterSeconds > 0)
        {
            StartCoroutine(DestroyAfter(_destroyAfterSeconds));
        }
    }

    protected virtual void SetDamage(int damage)
    {
        _damage = damage;
    }

    protected virtual void SetSpeed(int speed)
    {
        _speed = speed;
    }

    IEnumerator DestroyAfter(float destroyAfterSeconds)
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        DestroySpawnable();
    }

    protected virtual void DestroySpawnable()
    {
        Destroy(this.gameObject);
    }
}
