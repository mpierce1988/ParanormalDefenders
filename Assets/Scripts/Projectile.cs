using System;
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

    [SerializeField]
    private float _destroyAfterSeconds = -1f;

    private Rigidbody2D _rigidbody2D;
    private Vector2? _launchAngle = null;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

        _launchAngle = transform.right;

        _rigidbody2D.velocity = Vector2.zero;
        Debug.Log("Launching projectile at angle: " + _launchAngle + ", localRotation: " + transform.rotation);
        _rigidbody2D.AddForce(_launchAngle.Value * _speed, ForceMode2D.Impulse);

        if (_destroyAfterSeconds > 0)
        {
            StartCoroutine(DestroyAfter(_destroyAfterSeconds));
        }
    }

    IEnumerator DestroyAfter(float destroyAfterSeconds)
    {
        yield return new WaitForSeconds(destroyAfterSeconds);

        Destroy(this.gameObject);
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

    public void SetLaunchAngle(Vector2 launchAngle)
    {
        Debug.Log("Setting the launch angle to: " + launchAngle);
        _launchAngle = launchAngle;
    }
}
