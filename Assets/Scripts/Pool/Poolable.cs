using System;
using UnityEngine;

public class Poolable : MonoBehaviour, IPoolable
{
    private Action<GameObject> _returnToPoolAction;

    public void Initialize(Action<GameObject> returnToPoolAction)
    {
        _returnToPoolAction = returnToPoolAction;
    }

    public void ReturnToPool()
    {
        _returnToPoolAction(this.gameObject);
    }
}
