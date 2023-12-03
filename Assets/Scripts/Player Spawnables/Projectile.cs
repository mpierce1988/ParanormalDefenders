using System.Collections.Generic;
using UnityEngine;


public class Projectile : PlayerSpawnable
{
    protected override void Launch()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }
}
