using System;
using UnityEngine;
using UnityEngine.Events;

public interface IPoolable
{
    void Initialize(Action<GameObject> returnToPoolAction);
    void ReturnToPool();
}