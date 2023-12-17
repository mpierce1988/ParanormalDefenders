using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class Bible : PlayerSpawnable
{
    [SerializeField]
    private Vector2Variable _playerPosition;
    [SerializeField]
    private float _distanceFromPlayer;
    [SerializeField]
    private float _smoothingTime = 0.5f;

    private float _orbitProgress = 0f;
    private bool _isOrbiting = false;
    private Vector2 _currentVelocity;


    protected override void Launch()
    {
        _isOrbiting = true;
        // set random starting orbit progress
        _orbitProgress = Random.Range(0f, 360f);
    }

    protected void FixedUpdate()
    {
        if (!_isOrbiting) return;
        if (_playerPosition == null) return;

        _orbitProgress += _speed * Time.fixedDeltaTime;
        _orbitProgress %= 360f;

        float radians = _orbitProgress * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * _distanceFromPlayer;

        Vector2 desiredPosition = _playerPosition.Value + offset;
        //Vector2 positionDelta = desiredPosition - _rigidbody2D.position;
        //_rigidbody2D.velocity = positionDelta / Time.fixedDeltaTime;
        _rigidbody2D.position = Vector2.SmoothDamp(_rigidbody2D.position,
            desiredPosition, ref _currentVelocity, _smoothingTime,
            Mathf.Infinity, Time.fixedDeltaTime);

        // rotate to face outwards
        Vector2 positionDelta = desiredPosition - _rigidbody2D.position;
        float targetAngle = Mathf.Atan2(positionDelta.y, positionDelta.x) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.LerpAngle(_rigidbody2D.rotation,
            targetAngle, _smoothingTime * Time.fixedDeltaTime);
        _rigidbody2D.MoveRotation(smoothedAngle);

    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage _takeDamage = collision.gameObject.GetComponent<ITakeDamage>();

        if (_takeDamage != null)
        {
            _takeDamage.TakeDamage(_damage);
        }
    }
}
